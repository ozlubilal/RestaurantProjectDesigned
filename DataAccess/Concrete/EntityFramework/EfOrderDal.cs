using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, Context>, IOrderDal
    {
        public List<OrderDetailDto> GetOrderDetails(Expression<Func<OrderDetailDto, bool>> filter = null)
        {
            using (Context context = new Context())
            {
                var result = from o in context.Orders
                             join p in context.Products
                             on o.ProductId equals p.Id
                             join b in context.Bills
                             on o.BillId equals b.Id
                             join t in context.Tables
                             on b.TableId equals t.Id
                             join c in context.Categories
                             on p.CategoryId equals c.Id
                             join os in context.OrderStatuses
                             on o.OrderStatusId equals os.Id
                             join s in context.StoreBills
                             on b.StoreBillId equals s.Id


                             select new OrderDetailDto
                             {
                                 Id = o.Id,
                                 CategoryName = c.CategoryName,
                                 StoreBillId=s.Id,
                                 ProductName = p.ProductName,
                                 TableName = t.TableName,
                                 BillId = b.Id,
                                 Price = o.Price,
                                 TableStatuId=t.TableStatusId,
                                 Quantity = o.Quantity,
                                 OrderStatusName = os.OrderStatusName,
                                 BillStatuId=b.BillStatusId,
                             };
                return filter == null
             ? result.ToList()
             : result.Where(filter).ToList();
            }
        }
    }
}
