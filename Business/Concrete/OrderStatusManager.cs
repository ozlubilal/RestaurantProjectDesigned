using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class OrderStatusManager : IOrderStatusService
    {
        IOrderStatusDal _orderStatusDal;

        public OrderStatusManager(IOrderStatusDal orderStatusDal)
        {
            _orderStatusDal = orderStatusDal;
        }

        public IResult Add(OrderStatus orderStatus)
        {
            _orderStatusDal.Add(orderStatus);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult Delete(OrderStatus orderStatus)
        {
            _orderStatusDal.Delete(orderStatus);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<OrderStatus> GetById(int id)
        {
            return new SuccessDataResult<OrderStatus>(_orderStatusDal.Get(x => x.Id == id));
        }

        public IDataResult<List<OrderStatus>> GetList()
        {
            return new SuccessDataResult<List<OrderStatus>>(_orderStatusDal.GetList().ToList());

        }

        public IResult Update(OrderStatus orderStatus)
        {
            _orderStatusDal.Update(orderStatus);
            return new SuccessResult(Messages.DeletedSuccess);
        }
    }
}
