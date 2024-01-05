using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITableStatusService
    {
        IDataResult<List<TableStatus>> GetList();
        IDataResult<TableStatus> GetById(int id);
        IResult Add(TableStatus tableStatus);
        IResult Delete(TableStatus tableStatus);
        IResult Update(TableStatus tableStatus);
    }
}
