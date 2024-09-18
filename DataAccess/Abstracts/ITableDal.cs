using Core.DataAccess.EntityFramework;
using Entities.Concrete;

namespace DataAccess.Abstracts;

public interface ITableDal:IEntityRepository<Table>
{
}
