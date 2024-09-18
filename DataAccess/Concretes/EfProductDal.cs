using Core.DataAccess.EntityFramework;
using DataAccess.Abstracts;
using Entities.Concretes;

namespace DataAccess.Concretes;
    public class EfProductDal : EfEntityRepositoryBase<Product, Context>, IProductDal
    {
        public EfProductDal(Context context) : base(context)
        {
        }
    }

