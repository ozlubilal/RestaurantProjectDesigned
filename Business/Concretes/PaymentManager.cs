using AutoMapper;
using Business.Abstracts;
using Business.BusinessRule;
using Business.Constants;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using Microsoft.EntityFrameworkCore;

public class PaymentManager : IPaymentService
{
    private readonly IPaymentDal _paymentDal;
    private readonly IMapper _mapper;
    private readonly PaymentBusinessRules _paymentBusinessRule;

    public PaymentManager(IPaymentDal paymentDal, IMapper mapper,PaymentBusinessRules paymentBusinessRule)
    {
        _paymentDal = paymentDal;
        _mapper = mapper;
        _paymentBusinessRule = paymentBusinessRule;
    }

    public IDataResult<List<PaymentResponseDto>> GetList()
    {
        var payments = _paymentDal.GetList(
            include: p => p
                .Include(p => p.Bill)                
                .ThenInclude(b => b.Table)           
                .Include(p => p.Bill)                
                .ThenInclude(b => b.StoreBill)     
            ).ToList();

        if (payments.Any())
        {
            var paymentDtos = _mapper.Map<List<PaymentResponseDto>>(payments);
            return new SuccessDataResult<List<PaymentResponseDto>>(paymentDtos);
        }

        return new ErrorDataResult<List<PaymentResponseDto>>("No payments found.");
    }



    public IDataResult<PaymentResponseDto> GetById(Guid id)
    {
        var payment = _paymentDal.Get(p => p.Id == id);

        if (payment != null)
        {
            var paymentDto = _mapper.Map<PaymentResponseDto>(payment);
            return new SuccessDataResult<PaymentResponseDto>(paymentDto);
        }

        return new ErrorDataResult<PaymentResponseDto>("Payment not found.");
    }

    public IDataResult<PaymentResponseDto> GetByBillPayment(Guid billId)
    {
        var payment = _paymentDal.Get(p => p.BillId == billId);

        if (payment != null)
        {
            var paymentDto = _mapper.Map<PaymentResponseDto>(payment);
            return new SuccessDataResult<PaymentResponseDto>(paymentDto);
        }

        return new ErrorDataResult<PaymentResponseDto>("Payment not found.");
    }

    public IDataResult<List<PaymentResponseDto>> GetByStoreBillId(Guid storeBillId)
    {
        var payments = _paymentDal.GetList(p => p.Bill.StoreBillId == storeBillId).ToList();

        if (payments.Any())
        {
            var paymentDtos = _mapper.Map<List<PaymentResponseDto>>(payments);
            return new SuccessDataResult<List<PaymentResponseDto>>(paymentDtos);
        }

        return new ErrorDataResult<List<PaymentResponseDto>>("No payments found for the given Store Bill ID.");
    }

    public IDataResult<List<PaymentResponseDto>> GetByTableId(Guid tableId)
    {
        var payments = _paymentDal.GetList(p => p.Bill.TableId == tableId).ToList();

        if (payments.Any())
        {
            var paymentDtos = _mapper.Map<List<PaymentResponseDto>>(payments);
            return new SuccessDataResult<List<PaymentResponseDto>>(paymentDtos);
        }

        return new ErrorDataResult<List<PaymentResponseDto>>("No payments found for the given Table ID.");
    }

    public IResult Add(PaymentCreateDto paymentCreateDto)
    {
        IResult result = BusinessRules.Run(_paymentBusinessRule.ValidateOrdersStatus(paymentCreateDto.BillId));
        if (result != null)
        {
            return result;
        }
        var payment = _mapper.Map<Payment>(paymentCreateDto);
        _paymentDal.Add(payment);
        return new SuccessResult(Messages.PaymentSuccess);
    }
}
