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
    public class EfStoreBillDal : EfEntityRepositoryBase<StoreBill, Context>, IStoreBillDal
    {

        public List<StoreBillDetailDto> GetStoreBillDetails(Expression<Func<StoreBillDetailDto, bool>> filter = null)
        {
            using (Context context = new Context())
            {
                var result = from s in context.StoreBills
                             join ss in context.StoreBillStatuses
                             on s.StoreBillStatusId equals ss.Id
                             select new StoreBillDetailDto
                             {
                                 Id = s.Id,
                                 StartDate = s.StartDate,
                                 FinishDate = s.FinishDate,
                                 StoreBillStatusName = ss.StoreBillStatusName,
                               
                             };
                return filter == null
             ? result.ToList()
             : result.Where(filter).ToList();
            }
        }
    }
}
