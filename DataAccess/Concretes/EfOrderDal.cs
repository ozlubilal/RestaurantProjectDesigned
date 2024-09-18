using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes;

public class EfOrderDal: EfEntityRepositoryBase<Order,Context>,IOrderDal
{
    public EfOrderDal(Context context):base(context)
    {
        
    }
}
