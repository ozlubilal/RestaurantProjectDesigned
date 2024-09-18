using Core.Utilities.Results;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using System;
using System.Collections.Generic;

namespace Business.Abstracts;

public interface ICategoryService
{
    IDataResult<List<CategoryResponseDto>> GetList();
    IDataResult<CategoryResponseDto> GetById(Guid id);
    IResult Add(CategoryCreateDto categoryCreateDto);
    IResult Update(CategoryUpdateDto categoryUpdateDto);
    IResult Delete(Guid id);
}
