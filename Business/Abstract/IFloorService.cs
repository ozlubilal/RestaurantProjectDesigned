using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IFloorService
    {
        IDataResult<List<Floor>> GetAll();
        IDataResult<Floor> GetById(int id);
        IResult Add(Floor floor);
        IResult Delete(Floor floor);
        IResult Update(Floor floor);
    }
}
