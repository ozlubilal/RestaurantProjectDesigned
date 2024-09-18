using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes;

public class EfBillDal:EfEntityRepositoryBase<Bill,Context>,IBillDal
{
    public EfBillDal(Context context):base(context)
    {
        
    }
}
