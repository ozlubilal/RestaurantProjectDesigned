using Core.Utilities.Results;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using System;
using System.Collections.Generic;

namespace Business.Abstracts;

public interface IOrderService
{
    IDataResult<List<OrderResponseDto>> GetList();
    IDataResult<OrderResponseDto> GetById(Guid id);
    IDataResult<List<OrderResponseDto>> GetByBillId(Guid billId);
    IDataResult<List<OrderResponseDto>> GetByProductId(Guid productId);
    IDataResult<List<OrderResponseDto>> GetByTableId(Guid tableId);
    IResult Add(OrderCreateDto orderCreateDto);
    IResult Update(OrderUpdateDto orderUpdateDto);
    IResult Delete(Guid id);
}
