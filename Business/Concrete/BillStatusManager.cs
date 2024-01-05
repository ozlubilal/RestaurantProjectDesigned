using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BillStatusManager : IBillStatusService
    {
        IBillStatusDal _billStatusDal;

        public BillStatusManager(IBillStatusDal billStatusDal)
        {
            _billStatusDal = billStatusDal;
        }
        public IResult Add(BillStatus billStatus)
        {
            _billStatusDal.Add(billStatus);
            return new SuccessResult(Messages.AddedSuccess);
        }


        public IResult Delete(BillStatus billStatus)
        {
            _billStatusDal.Delete(billStatus);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<BillStatus> GetById(int id)
        {
            return new SuccessDataResult<BillStatus>(_billStatusDal.Get(b => b.Id == id));
        }

        public IDataResult<List<BillStatus>> GetList()
        {
            return new SuccessDataResult<List<BillStatus>>(_billStatusDal.GetList());
        }

        public IResult Update(BillStatus billStatus)
        {
            _billStatusDal.Update(billStatus);
            return new SuccessResult(Messages.UpdatedSuccess);
        }




    }
}
