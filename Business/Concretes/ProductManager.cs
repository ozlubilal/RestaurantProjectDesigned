using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Logging;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;

public class ProductManager : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductDal _productDal;
    private readonly ICategoryService _categoryService;

    public ProductManager(IProductDal productDal, IMapper mapper, ICategoryService categoryService)
    {
        _productDal = productDal;
        _mapper = mapper;
        _categoryService = categoryService;
    }

    [CacheRemoveAspect("Business.Abstracts.IProductService.GetList()")]
    [ValidationAspect(typeof(ProductValidator))]
    public IResult Add(ProductCreateDto productCreateDto)
    {
        IResult result = BusinessRules.Run(CheckIfProductNameExist(productCreateDto.Name));

        if (result != null)
        {
            return result;
        }

        var product = _mapper.Map<Product>(productCreateDto);
        _productDal.Add(product);

        return new SuccessResult("Product added successfully.");
    }

    [CacheRemoveAspect("Business.Abstracts.IProductService.GetList()")]
    [ValidationAspect(typeof(ProductValidator))]
    public IResult Update(ProductUpdateDto productUpdateDto)
    {
        var existingProduct = _productDal.Get(p => p.Id == productUpdateDto.Id);

        if (existingProduct == null)
        {
            return new ErrorResult("Product not found.");
        }

        _mapper.Map(productUpdateDto, existingProduct);
        _productDal.Update(existingProduct);

        return new SuccessResult("Product updated successfully.");
    }

    [CacheRemoveAspect("Business.Abstracts.IProductService.GetList()")]
    public IResult Delete(Guid id)
    {
        var product = _productDal.Get(p => p.Id == id);

        if (product == null)
        {
            return new ErrorResult("Product not found.");
        }

        _productDal.Delete(product);

        return new SuccessResult("Product deleted successfully.");
    }

    [LogAspect(typeof(MsSqlLogger))]
    public IDataResult<ProductResponseDto> GetById(Guid id)
    {
        var product = _productDal.Get(p => p.Id == id);

        if (product == null)
        {
            return new ErrorDataResult<ProductResponseDto>("Product not found.");
        }

        var productResponseDto = _mapper.Map<ProductResponseDto>(product);
        return new SuccessDataResult<ProductResponseDto>(productResponseDto);
    }

    [LogAspect(typeof(MsSqlLogger))]
    public IDataResult<List<ProductResponseDto>> GetList()
    {
        var products = _productDal.GetList();
        var productResponseDtos = _mapper.Map<List<ProductResponseDto>>(products);

        return new SuccessDataResult<List<ProductResponseDto>>(productResponseDtos);
    }

    [LogAspect(typeof(MsSqlLogger))]
    public IDataResult<List<ProductResponseDto>> GetByCategoryId(Guid categoryId)
    {
        var products = _productDal.GetList(p => p.CategoryId == categoryId);
        var productResponseDtos = _mapper.Map<List<ProductResponseDto>>(products);

        return new SuccessDataResult<List<ProductResponseDto>>(productResponseDtos);
    }

    private IResult CheckIfProductNameExist(string productName)
    {
        var result = _productDal.GetList(p => p.Name == productName).Any();

        if (result)
        {
            return new ErrorResult("Product name already exists.");
        }

        return new SuccessResult();
    }
}
