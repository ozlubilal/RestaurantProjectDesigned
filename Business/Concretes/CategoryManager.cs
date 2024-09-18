using AutoMapper;
using Business.Abstracts;
using Business.Aspects.Autofac;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concretes;

public class CategoryManager : ICategoryService
{
    private readonly ICategoryDal _categoryDal;
    private readonly IMapper _mapper;

    public CategoryManager(ICategoryDal categoryDal, IMapper mapper)
    {
        _categoryDal = categoryDal;
        _mapper = mapper;
    }
   // [SecuredOperation("Admin")]
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
        var category = _mapper.Map<Category>(categoryCreateDto);
        _categoryDal.Add(category);
        return new SuccessResult("Category added successfully.");
    }

    public IResult Update(CategoryUpdateDto categoryUpdateDto)
    {
        // Önce mevcut kategori nesnesini bul
        var existingCategory = _categoryDal.Get(c => c.Id == categoryUpdateDto.Id);

        if (existingCategory == null)
        {
            return new ErrorResult("Category not found.");
        }

        // Güncellenmiş verileri mevcut nesneye uygulayın
        existingCategory.Name = categoryUpdateDto.Name;

        // Güncellenmiş nesneyi kaydedin
        _categoryDal.Update(existingCategory);

        return new SuccessResult("Category updated successfully.");
    }


    public IResult Delete(Guid id)
    {
        var category = _categoryDal.Get(c => c.Id == id);
        if (category != null)
        {
            _categoryDal.Delete(category);
            return new SuccessResult("Category deleted successfully.");
        }
        return new ErrorResult("Category not found.");
    }
}
