using Core.Utilities.Results;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using System;
using System.Collections.Generic;
using Entities.Concrete;

namespace Business.Abstract;

public interface IBillService
{
    IDataResult<List<BillResponseDto>> GetAll();
    IDataResult<List<Bill>> GetAllBills();
    IDataResult<BillResponseDto> GetById(Guid id);
    IDataResult<Bill> GetBillById(Guid id);

    IDataResult<List<BillResponseDto>> GetByStoreBillId(Guid id);
    IResult Add(BillCreateDto billCreateDto);
    IResult Update(BillUpdateDto billUpdateDto);
    IResult Delete(Guid id);
    IDataResult<List<BillResponseDto>> GetByTableId(Guid tableId);
}
