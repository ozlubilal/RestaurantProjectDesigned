using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;

namespace Business.Concretes;

public class OrderManager : IOrderService   
{
    private readonly IOrderDal _orderDal;
    private readonly IMapper _mapper;

    public OrderManager(IOrderDal orderDal, IMapper mapper)
    {
        _orderDal = orderDal;
        _mapper = mapper;
    }

    public IDataResult<List<OrderResponseDto>> GetList()
    {
        var orders = _orderDal.GetList();
        var orderDtos = _mapper.Map<List<OrderResponseDto>>(orders);
        return new SuccessDataResult<List<OrderResponseDto>>(orderDtos);
    }

    public IDataResult<OrderResponseDto> GetById(Guid id)
    {
        var order = _orderDal.Get(o => o.Id == id);
        var orderDto = _mapper.Map<OrderResponseDto>(order);
        return new SuccessDataResult<OrderResponseDto>(orderDto);
    }

    public IDataResult<List<OrderResponseDto>> GetByBillId(Guid billId)
    {
        var orders = _orderDal.GetList(o => o.BillId == billId);
        var orderDtos = _mapper.Map<List<OrderResponseDto>>(orders);
        return new SuccessDataResult<List<OrderResponseDto>>(orderDtos);
    }

    public IDataResult<List<OrderResponseDto>> GetByProductId(Guid productId)
    {
        var orders = _orderDal.GetList(o => o.ProductId == productId);
        var orderDtos = _mapper.Map<List<OrderResponseDto>>(orders);
        return new SuccessDataResult<List<OrderResponseDto>>(orderDtos);
    }

    public IResult Add(OrderCreateDto orderCreateDto)
    {
        var order = _mapper.Map<Order>(orderCreateDto);
        _orderDal.Add(order);
        return new SuccessResult("Order added successfully.");
    }

    public IResult Update(OrderUpdateDto orderUpdateDto)
    {
        var order = _mapper.Map<Order>(orderUpdateDto);
        _orderDal.Update(order);
        return new SuccessResult("Order updated successfully.");
    }

    public IResult Delete(Guid id)
    {
        var order = _orderDal.Get(o => o.Id == id);
        if (order != null)
        {
            _orderDal.Delete(order);
            return new SuccessResult("Order deleted successfully.");
        }
        return new ErrorResult("Order not found.");
    }
}
