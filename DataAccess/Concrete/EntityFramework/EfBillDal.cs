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
    public class EfBillDal : EfEntityRepositoryBase<Bill, Context>, IBillDal
    {
        public List<BillDetailDto> GetBillDetails(Expression<Func<BillDetailDto, bool>> filter = null)
        {
            using (Context context = new Context())
            {
                var result = from b in context.Bills
                             join bs in context.BillStatuses
                             on b.BillStatusId equals bs.Id
                             join t in context.Tables
                             on b.TableId equals t.Id
                             select new BillDetailDto
                             {
                                 Id = b.Id,
                                 TableName = t.TableName,
                                 BillStatusName = bs.BillStatusName,
                                 Date = b.Date,

                             };
                return filter == null
             ? result.ToList()
             : result.Where(filter).ToList();
            }
        }
    }
}
