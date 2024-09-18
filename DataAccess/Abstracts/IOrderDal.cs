using Core.DataAccess.EntityFramework;
using DataAccess.Concretes;
using Entities.Concrete;

namespace DataAccess.Abstracts;

public interface IOrderDal:IEntityRepository<Order>
{
}
