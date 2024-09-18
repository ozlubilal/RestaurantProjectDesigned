using Core.Utilities.Results;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using System;
using System.Collections.Generic;

namespace Business.Abstracts;

public interface ITableService
{
    IDataResult<List<TableResponseDto>> GetList();
    IDataResult<TableResponseDto> GetById(Guid id);
    IResult Add(TableCreateDto tableCreateDto);
    IResult Update(TableUpdateDto tableUpdateDto);
    IResult Delete(Guid id);
}
