using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IOrderStatusService
    {
        IDataResult<List<OrderStatus>> GetList();
        IDataResult<OrderStatus> GetById(int id);
        IResult Add(OrderStatus orderStatus);
        IResult Delete(OrderStatus orderStatus);
        IResult Update(OrderStatus orderStatus);
    }
}
