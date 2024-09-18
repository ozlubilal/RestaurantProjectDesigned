using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace DataAccess.Concretes;

public class EfStoreBillDal:EfEntityRepositoryBase<StoreBill,Context>,IStoreBillDal
{
    public EfStoreBillDal(Context context):base(context)
    {
        
    }
}
