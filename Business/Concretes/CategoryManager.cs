using AutoMapper;
using Business.Abstracts;
using Business.Aspects.Autofac;
using Business.BusinessRule;
using Business.Constants;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace Business.Concretes;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;
    private readonly IMapper _mapper;
    private readonly CategoryBusinessRules _categoryBusinessRules;

    public CategoryManager(ICategoryDal categoryDal, IMapper mapper, CategoryBusinessRules categoryBusinessRules)
    {
        _categoryDal = categoryDal;
        _mapper = mapper;
        _categoryBusinessRules = categoryBusinessRules;
    }
    public IDataResult<List<CategoryResponseDto>> GetList()
    {
        var categories = _categoryDal.GetList();
        var categoryDtos = _mapper.Map<List<CategoryResponseDto>>(categories);
        return new SuccessDataResult<List<CategoryResponseDto>>(categoryDtos);
    }

    public IDataResult<CategoryResponseDto> GetById(Guid id)
    {
        var category = _categoryDal.Get(c => c.Id == id);
        var categoryDto = _mapper.Map<CategoryResponseDto>(category);
        return new SuccessDataResult<CategoryResponseDto>(categoryDto);
    }
    [SecuredOperation("Admin")]
    public IResult Add(CategoryCreateDto categoryCreateDto)
    {
        IResult result = BusinessRules.Run(_categoryBusinessRules.CheckIfCategoryNameExist(categoryCreateDto.Name, categoryCreateDto.Id));

        if (result != null)
        {
            return result;
        }

        var category = _mapper.Map<Category>(categoryCreateDto);
        _categoryDal.Add(category);
        return new SuccessResult(Messages.CategoryAdded);
    }
    [SecuredOperation("Admin")]
    public IResult Update(CategoryUpdateDto categoryUpdateDto)
    {
        // Önce mevcut kategori nesnesini bul
        var existingCategory = _categoryDal.Get(c => c.Id == categoryUpdateDto.Id);

        if (existingCategory == null)
        {
            return new ErrorResult(Messages.CategoryNotFound);
        }

        _mapper.Map(categoryUpdateDto, existingCategory);

        IResult result = BusinessRules.Run(_categoryBusinessRules.CheckIfCategoryNameExist(categoryUpdateDto.Name, categoryUpdateDto.Id));

        if (result != null)
        {
            return result;
        }

        _categoryDal.Update(existingCategory);

        return new SuccessResult(Messages.CategoryUpdated);
    }

    [SecuredOperation("Admin")]
    public IResult Delete(Guid id)
    {     
        var category = _categoryDal.Get(c => c.Id == id);

        if (category != null)
        {
            //Kategoriye ait ürün varsa kategori silinemez.
            IResult result = BusinessRules.Run(_categoryBusinessRules.CheckIfCategoryHasProducts(id));

            if (result != null)
            {
                return result;
            }
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        return new ErrorResult(Messages.CategoryNotFound);
    }
}
