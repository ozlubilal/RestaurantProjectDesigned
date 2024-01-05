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
    public class BillManager : IBillService
    {
        IBillDal _billDal;
        ITableService _tableService;
        IOrderService _orderService;

        public BillManager(IBillDal billDal, ITableService tableService, IOrderService orderService)
        {
            _billDal = billDal;
            _tableService = tableService;
            _orderService = orderService;
        }

        public IResult Add(Bill bill)
        {
            var result = BusinessRules.Run(CheckIfTableEmpty(bill));
            if (result != null)
            {
                return result;
            }

            var table = _tableService.GetById(bill.TableId).Data;
            table.TableStatusId = 2;
            _tableService.Update(table);
            _billDal.Add(bill);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult Delete(Bill bill)
        {
            IResult result = BusinessRules.Run(CheckIfOrderOfBillExist(bill));
            if (result != null)
            {
                return result;
            }

            _billDal.Delete(bill);
            //Masa durumunu boş  yapıyoruz
            _tableService.ChangeTableStatuId(bill.TableId, 1);
            return new SuccessResult(Messages.DeletedSuccess);
        }


        public IDataResult<List<Bill>> GetByBillStatusId(int billStatusId)
        {
            return new SuccessDataResult<List<Bill>>(_billDal.GetList(b => b.BillStatusId == billStatusId).ToList());
        }

        public IDataResult<List<Bill>> GetByDate(DateTime date)
        {
            return new SuccessDataResult<List<Bill>>(_billDal.GetList(b => b.Date == date).ToList());
        }

        public IDataResult<Bill> GetById(int id)
        {
            return new SuccessDataResult<Bill>(_billDal.Get(b => b.Id == id));
        }

        public IDataResult<Bill> GetByTableIdAndStatusId(int tableId, int statusId)
        {
            return new SuccessDataResult<Bill>(_billDal.Get(b => b.TableId == tableId && b.BillStatusId == statusId));
        }

        public IDataResult<List<Bill>> GetList()
        {
            return new SuccessDataResult<List<Bill>>(_billDal.GetList().ToList());
        }

        public IDataResult<List<BillDetailDto>> GetListBillDetailDto()
        {
            //int billPrice=0;
            //List<BillDetailDto> bills = _billDal.GetBillDetails().ToList();
            //bills.ForEach(b => {
            //    var orders = _orderService.GetByBillId(b.Id).Data; 
            //    orders.ForEach(o => { 
            //        billPrice= billPrice+o.Price; });
            //});
            //bills.
            
            return new SuccessDataResult<List<BillDetailDto>>(_billDal.GetBillDetails().ToList());
        }

        public IResult Update(Bill bill)
        {            
           var table= _tableService.GetById(bill.TableId).Data;
            table.TableStatusId = 1;          
            _billDal.Update(bill);
            _tableService.Update(table);
            return new SuccessResult(Messages.UpdatedSuccess);
        }
        
        private IResult CheckIfTableEmpty(Bill bill)
        {
            var table = _tableService.GetById(bill.TableId).Data;
            if(table.TableStatusId == 2)
            {
                return new ErrorResult(Messages.TableNotEmpty);
            }
            return new SuccessResult();
        }

        //silinecek hesaba ait sipariş var mı
        private IResult CheckIfOrderOfBillExist(Bill bill)
        {
            var result = _orderService.GetByBillId(bill.Id).Data.Any();
            if (result)
            {
                return new ErrorResult(Messages.OrderOfBillExist);
            }
            return new SuccessResult();
        }

        public IDataResult<int> CalculateBillPrice(int billId)
        {
            //hesaba ait siprişlerin fiyat ve sayılarının çarpımının toplamını alıyoruz
            var priceOfBill = _orderService.GetByBillId(billId).Data.Select(o => o.Price * o.Quantity).Sum();
           return new SuccessDataResult<int>(priceOfBill);
        }

        public IDataResult<List<Bill>> GetByStoreBillId(int storeBillId)
        {
           return new SuccessDataResult<List<Bill>>(_billDal.GetList(b=>b.StoreBillId==storeBillId));
        }
    }
}
