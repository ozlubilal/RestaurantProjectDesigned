using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstracts;

public interface IPaymentService
{
    IDataResult<List<PaymentResponseDto>> GetList();
    IResult Add(PaymentCreateDto paymentCreateDto);

    IDataResult<PaymentResponseDto> GetById(Guid id);

    IDataResult<PaymentResponseDto> GetByBillPayment(Guid billId);
    IDataResult<List<PaymentResponseDto>> GetByTableId(Guid tableId);

    IDataResult<List<PaymentResponseDto>> GetByStoreBillId(Guid staoreBillId);
}
