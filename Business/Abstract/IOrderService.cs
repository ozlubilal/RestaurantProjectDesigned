using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<Order>> GetList();
        IDataResult<Order> GetById(int id);
        IDataResult<List<Order>> GetByBillId(int billId);
        IDataResult<List<Order>> GetByOrderStatusId(int orderStatusId);
        IDataResult<List<OrderDetailDto>> GetOrderDetails();
        IDataResult<List<OrderDetailDto>> GetOrderDetailsOfActive();
        IDataResult<List<OrderDetailDto>> GetByProductName(string productName);
        IDataResult<OrderDetailDto> GetOrderDetailsById(int id);
        IDataResult<List<OrderDetailDto>> RefreshOrderDetilsList(List<OrderDetailDto> list);
        IDataResult<List<OrderDetailDto>> GetOrderDetailsByBillId(int billId);
        IDataResult<List<OrderDetailDto>> GetOrderDetailsByStoreBillId(int storeBillId);

        IResult Add(Order order);
        IResult Delete(Order order);
        IResult Update(Order order);
    }
}
