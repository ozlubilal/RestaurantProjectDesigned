using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes;

public class EfPaymentDal:EfEntityRepositoryBase<Payment,Context>,IPaymentDal
{
    public EfPaymentDal(Context context) : base(context)
    {

    }
}
