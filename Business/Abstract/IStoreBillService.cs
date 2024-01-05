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
    public interface IStoreBillService
    {
        IDataResult<List<StoreBill>> GetList();
        IDataResult<StoreBill> GetById(int id);
        IDataResult<StoreBill> GetByStatusId(int statusId);
        IDataResult<List<StoreBillDetailDto>> GetStoreBillDetails();
        IResult Add(StoreBill storeBill);
        IResult Delete(StoreBill storeBill);
        IResult Update(StoreBill storeBill);
    }
}
