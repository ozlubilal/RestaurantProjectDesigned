using Business.Abstract;
using Business.Constans;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StoreBillManager : IStoreBillService
    {
        IStoreBillDal _storeBillDal;
        IBillService _billService;
        IOrderService _orderService;

        public StoreBillManager(IStoreBillDal storeBİllDal,IBillService billService,IOrderService orderService)
        {
            _storeBillDal = storeBİllDal;
            _billService = billService;
            _orderService = orderService;
        }

        public IResult Add(StoreBill storeBill)
        {
            _storeBillDal.Add(storeBill);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult Delete(StoreBill storeBill)
        {
            IResult result = BusinessRules.Run(CheckIfBillExist(storeBill));
            if(result!=null)
            {
                return result;
            }
            _storeBillDal.Delete(storeBill);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<StoreBill> GetById(int id)
        {
            return new SuccessDataResult<StoreBill>(_storeBillDal.Get(s=>s.Id==id));
        }

        public IDataResult<StoreBill> GetByStatusId(int statusId)
        {
            return new SuccessDataResult<StoreBill>(_storeBillDal.Get(s => s.StoreBillStatusId == statusId));
        }

        public IDataResult<List<StoreBill>> GetList()
        {
           return new SuccessDataResult<List<StoreBill>>(_storeBillDal.GetList());
        }

        public IDataResult<List<StoreBillDetailDto>> GetStoreBillDetails()
        {
            var storeBills = _storeBillDal.GetStoreBillDetails();
            foreach (var storeBill in storeBills)
            {
                var bills = _billService.GetByStoreBillId(storeBill.Id).Data;
                storeBill.BillOfPayedCount=bills.FindAll(b=>b.BillStatusId==2).Count();
                //foreach (var bill in bills)
                //{
                //    var order=_orderService.get
                //    storeBill.StoreBillAmount= storeBill.StoreBillAmount+
                //}
            }
           return new SuccessDataResult<List<StoreBillDetailDto>>(storeBills);
        }

        public IResult Update(StoreBill storeBill)
        {
            IResult result = BusinessRules.Run(CheckIfActiveBillExist(storeBill));
            if (result != null)
            {
                return result;
            }
            _storeBillDal.Update(storeBill);
            return new SuccessResult(Messages.UpdatedSuccess);
        }
        private IResult CheckIfBillExist(StoreBill storeBill)
        {
            var result = _billService.GetByStoreBillId(storeBill.Id).Data.Any();
            if (result)
            {
                return new ErrorResult(Messages.BillOfStoreBillExist);
            }
            return new SuccessResult();
        }
        private IResult CheckIfActiveBillExist(StoreBill storeBill)
        {
            var result = _billService.GetByStoreBillId(storeBill.Id).Data.FindAll(b=>b.BillStatusId==1).Any();
            if (result)
            {
                return new ErrorResult(Messages.ActiveBillOfStoreBillExist);
            }
            return new SuccessResult();
        }
    }
}
