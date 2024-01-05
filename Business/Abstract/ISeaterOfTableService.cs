using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ISeaterOfTableService
    {
        IDataResult<List<SeaterOfTable>> GetList();
        IDataResult<SeaterOfTable> GetById(int id);
        IResult Add(SeaterOfTable seaterOfTable);
        IResult Delete(SeaterOfTable seaterOfTable);
        IResult Update(SeaterOfTable seaterOfTable);
    }
}
