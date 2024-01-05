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
    public interface IBillService
    {
        IDataResult<List<Bill>> GetList();
        IDataResult<List<BillDetailDto>> GetListBillDetailDto();
        IDataResult<Bill> GetById(int id);
        IDataResult<int> CalculateBillPrice(int billId);
        IDataResult<List<Bill>> GetByBillStatusId(int billStatusId);
        IDataResult<List<Bill>> GetByStoreBillId(int storeBillId);
        IDataResult<Bill> GetByTableIdAndStatusId(int tableId, int statusId);
        IDataResult<List<Bill>> GetByDate(DateTime date);
        IResult Add(Bill bill);
        IResult Delete(Bill bill);
        IResult Update(Bill bill);
    }
}
