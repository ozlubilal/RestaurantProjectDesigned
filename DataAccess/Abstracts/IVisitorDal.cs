using Core.DataAccess.EntityFramework;
using Entities.Concretes;

namespace DataAccess.Abstracts;

public interface IVisitorDal: IEntityRepository<Visitor>
{
}
