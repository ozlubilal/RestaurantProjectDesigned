using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBillStatusService
    {
        IDataResult<List<BillStatus>> GetList();
        IDataResult<BillStatus> GetById(int id);
        IResult Add(BillStatus billStatus);
        IResult Delete(BillStatus billStatus);
        IResult Update(BillStatus billStatus);
    }
}
