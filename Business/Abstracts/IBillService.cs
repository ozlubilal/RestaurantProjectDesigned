﻿using Core.Utilities.Results;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using System;
using System.Collections.Generic;

namespace Business.Abstract;

public interface IBillService
{
    IDataResult<List<BillResponseDto>> GetAll();
    IDataResult<BillResponseDto> GetById(Guid id);
    IResult Add(BillCreateDto billCreateDto);
    IResult Update(BillUpdateDto billUpdateDto);
    IResult Delete(Guid id);
    IDataResult<List<BillResponseDto>> GetByTableId(Guid tableId);
}