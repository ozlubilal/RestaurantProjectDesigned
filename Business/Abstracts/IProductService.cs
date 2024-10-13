using Core.Utilities.Results;
using Business.Dtos.Responses;
using System;
using System.Collections.Generic;
using Business.Dtos.Requests;

public interface IProductService
{
    IDataResult<List<ProductResponseDto>> GetList();
    IDataResult<ProductResponseDto> GetById(Guid id);
    IDataResult<List<ProductResponseDto>> GetByCategoryId(Guid categoryId);
    IResult Add(ProductCreateDto productCreateDto);
    IResult Update(ProductUpdateDto productUpdateDto);
    IResult Delete(Guid id);
}
