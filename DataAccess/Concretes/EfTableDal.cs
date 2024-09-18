using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Concrete;

namespace DataAccess.Concretes;

public class EfTableDal:EfEntityRepositoryBase<Table,Context>,ITableDal
{
    public EfTableDal(Context context):base(context)
    {
        
    }
}
