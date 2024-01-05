using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface ITableService
    {
        IDataResult<List<Table>> GetList();
        IDataResult<Table> GetById(int id);
        IDataResult<List<Table>> GetByStatuId(int statuId);
        public IDataResult<List<Table>> GetByFloorId(int floorId);
        IDataResult<List<TableDetailDto>> GetListTableDetailDto();
        IResult ChangeTableStatuId(int tableId, int tableStatuId);
        IResult Add(Table table);
        IResult Delete(Table table);
        IResult Update(Table table);
    }
}
