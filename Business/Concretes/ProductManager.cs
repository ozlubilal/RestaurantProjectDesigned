using AutoMapper;
using Business.Abstracts;
using Business.Aspects.Autofac;
using Business.BusinessRule;
using Business.Constants;
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
using DataAccess.Concretes;

public class ProductManager : IProductService
{
    private readonly IMapper _mapper;
    private readonly IProductDal _productDal;
    private readonly ICategoryService _categoryService;
    private readonly ProductBusinessRules _productBusinessRules;

    public ProductManager(IProductDal productDal, IMapper mapper, ICategoryService categoryService, ProductBusinessRules productBusinessRules)
    {
        _productDal = productDal;
        _mapper = mapper;
        _categoryService = categoryService;
        _productBusinessRules = productBusinessRules;
    }

    [CacheRemoveAspect("Business.Abstracts.IProductService.GetList()")]
    [ValidationAspect(typeof(ProductCreateDtoValidator))]
    [SecuredOperation("Admin")]
    public IResult Add(ProductCreateDto productCreateDto)
    {
        IResult result = BusinessRules.Run(_productBusinessRules.CheckIfProductNameExist(productCreateDto.Name,productCreateDto.Id),
            _productBusinessRules.CheckIfCategoryExists(productCreateDto.Id));

        if (result != null)
        {
            return result;
        }

        var product = _mapper.Map<Product>(productCreateDto);
        _productDal.Add(product);

        return new SuccessResult(Messages.ProductAdded);
    }

    [CacheRemoveAspect("Business.Abstracts.IProductService.GetList()")]
    [ValidationAspect(typeof(ProductValidator))]
    [SecuredOperation("Admin")]
    public IResult Update(ProductUpdateDto productUpdateDto)
    {
        var existingProduct = _productDal.Get(p => p.Id == productUpdateDto.Id);

        if (existingProduct == null)
        {
            return new ErrorResult(Messages.ProductNotFound);
        }

        IResult result = BusinessRules.Run(_productBusinessRules.CheckIfProductNameExist(productUpdateDto.Name,productUpdateDto.Id));

        if (result != null)
        {
            return result;
        }

        _mapper.Map(productUpdateDto, existingProduct);
        _productDal.Update(existingProduct);

        return new SuccessResult(Messages.ProductUpdated);
    }

    [CacheRemoveAspect("Business.Abstracts.IProductService.GetList()")]
    [SecuredOperation("Admin")]
    public IResult Delete(Guid id)
    {
        var product = _productDal.Get(t => t.Id == id);

        if (product != null)
        {
            //Ürüne ait sipriş varsa ürün silinemez.
            IResult result = BusinessRules.Run(_productBusinessRules.CheckIfProductHasOrders(id));

            if (result != null)
            {
                return result;
            }
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        return new ErrorResult(Messages.ProductNotFound);
    }

    [LogAspect(typeof(MsSqlLogger))]
    public IDataResult<ProductResponseDto> GetById(Guid id)
    {
        var product = _productDal.Get(p => p.Id == id);

        if (product == null)
        {
            return new ErrorDataResult<ProductResponseDto>(Messages.ProductNotFound);
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

   
}
