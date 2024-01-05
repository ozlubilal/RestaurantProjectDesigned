using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constans;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }


        public IResult Add(Order order)
        {
           
            IResult result = BusinessRules.Run(CheckIfOrderExist(order));
            if (result != null)
            {
                return result;
            }
          
                _orderDal.Add(order);
                return new SuccessResult(Messages.AddedSuccess);
            
        }
      
        public IResult Delete(Order order)
        {
            IResult result = BusinessRules.Run(CheckIfOrderQuantityMoreZero(order));
            if (result != null)
            {
                return result;
            }

            _orderDal.Delete(order);
            return new SuccessResult(Messages.DeletedSuccess);
        }
       
        private IResult CheckIfOrderStatusAndOrderStatus(Order order)
        {
            var result = _orderDal.Get(o => o.OrderStatusId != 1 && o.UserId == 1);
            if(result != null)
            {
                return new ErrorResult();
            }
            return new SuccessResult();
        }
        //bu hesaba ait aynı üründen sipariş varsa sipariş sayısını 1 artırıyoruz.
        private IResult CheckIfOrderExist(Order order)
        {
            var result = _orderDal.Get(x => x.ProductId == order.ProductId && x.BillId == order.BillId && x.OrderStatusId==order.OrderStatusId );
            if (result != null)
            {
                result.Quantity = result.Quantity + order.Quantity;
                _orderDal.Update(result);
                return new ErrorResult();
            }
            return new SuccessResult();
        }

    
        //aynı ürüne ait sipariş adedi 1'den büyükse silme işlemi için sipariş adedini 1 eksiltiyoruz 
        private IResult CheckIfOrderQuantityMoreZero(Order order)
        {
            var data = _orderDal.Get(o=>o.ProductId==order.ProductId&&o.BillId==order.BillId&&o.OrderStatusId==order.OrderStatusId);
            if(data != null && data.Quantity > 1)
            {
                data.Quantity = data.Quantity - order.Quantity;
                _orderDal.Update(data);
                return new ErrorResult();
            }
            return new SuccessResult();
        }

        //Get Method(order)
        public IDataResult<List<Order>> GetByBillId(int billId)
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetList(o => o.BillId == billId).ToList());
        }


        public IDataResult<Order> GetById(int id)
        {
            return new SuccessDataResult<Order>(_orderDal.Get(o => o.Id == id));
        }

        public IDataResult<List<Order>> GetByOrderStatusId(int orderStatusId)
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetList(o => o.OrderStatusId == orderStatusId).ToList());
        }

       
        public IDataResult<List<Order>> GetList()
        {
            return new SuccessDataResult<List<Order>>(_orderDal.GetList().ToList());
        }
      
        
        //Get Method(OrderDetailDto)
        public IDataResult<List<OrderDetailDto>> GetOrderDetails()
        {
            return new SuccessDataResult<List<OrderDetailDto>>(_orderDal.GetOrderDetails());
        }
        public IDataResult<List<OrderDetailDto>> GetOrderDetailsByBillId(int billId)
        {
            return new SuccessDataResult<List<OrderDetailDto>>(_orderDal.GetOrderDetails(b => b.BillId == billId));
        }
        public IDataResult<List<OrderDetailDto>> GetByProductName(string productName)
        {
            return new SuccessDataResult<List<OrderDetailDto>>(_orderDal.GetOrderDetails(o => o.ProductName == productName).ToList());
        }
        public IDataResult<List<OrderDetailDto>> GetOrderDetailsOfActive()
        {
            return new SuccessDataResult<List<OrderDetailDto>>(_orderDal.GetOrderDetails(o => o.BillStatuId ==1).ToList());
        }
        public IDataResult<OrderDetailDto> GetOrderDetailsById(int id)
        {
            return new SuccessDataResult<OrderDetailDto>(_orderDal.GetOrderDetails(o=>o.Id==id).FirstOrDefault());
        }
        public IDataResult<List<OrderDetailDto>> RefreshOrderDetilsList(List<OrderDetailDto> list)
        {

            var orderDetailsList= new List<OrderDetailDto>();
            foreach (var item in list)
            {
                var orderDetailDto=_orderDal.GetOrderDetails(o => o.Id == item.Id).FirstOrDefault();
                orderDetailsList.Add(orderDetailDto);

            }
            return new SuccessDataResult<List<OrderDetailDto>>(orderDetailsList);
        }

        [SecuredOperation("Chef,Cashier,Admin")]     
        public IResult Update(Order order)
        {
            _orderDal.Update(order);
            return new SuccessResult(Messages.UpdatedSuccess);
        }

        public IDataResult<List<OrderDetailDto>> GetOrderDetailsByStoreBillId(int storeBillId)
        {
            return new SuccessDataResult<List<OrderDetailDto>>(_orderDal.GetOrderDetails(o=>o.StoreBillId==storeBillId));
        }
    }
}
