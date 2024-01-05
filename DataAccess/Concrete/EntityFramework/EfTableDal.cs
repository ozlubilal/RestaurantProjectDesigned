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
    public class EfTableDal : EfEntityRepositoryBase<Table, Context>, ITableDal
    {
        public List<TableDetailDto> GetTableDetails(Expression<Func<TableDetailDto, bool>> filter = null)
        {
            using (Context context = new Context())
            {
                var result = from t in context.Tables
                             join f in context.Floors
                             on t.FloorId equals f.Id
                             join s in context.SeaterOfTables
                             on t.SeaterOfTableId equals s.Id
                             join ts in context.TableStatuses
                             on t.TableStatusId  equals ts.Id
                             select new TableDetailDto
                             {
                                 Id = t.Id,
                                 FloorName = f.FloorName,
                                 SeaterOfTableName = s.SeaterOfTableName,
                                 TableName = t.TableName,
                                 TableStatusName = ts.TableStatusName,
                             };
                return filter == null
             ? result.ToList()
             : result.Where(filter).ToList();
            }
        }
    }
}
