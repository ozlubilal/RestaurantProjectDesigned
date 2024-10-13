using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.BusinessRule;
using Business.Constants;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class BillManager : IBillService
    {
        private readonly IBillDal _billDal;
        private readonly ITableService _tableService;
        private readonly IMapper _mapper;
        private readonly IStoreBillService _storeBillService;
        private readonly BillBusinessRules _billBusinessRules;

        public BillManager(IBillDal billDal, IMapper mapper, IStoreBillService storeBillService, ITableService tableService, BillBusinessRules billBusinessRules)
        {
            _billDal = billDal;
            _mapper = mapper;
            _storeBillService = storeBillService;
            _tableService = tableService;
            _billBusinessRules = billBusinessRules;
        }

        public IDataResult<List<BillResponseDto>> GetAll()
        {

            var bills = _billDal.GetList(
                include: query => query.Include(b => b.Table) // Table ilişkisini yükle
            );

            // DTO listesi oluştur
            var billDtos = _mapper.Map<List<BillResponseDto>>(bills);

            return new SuccessDataResult<List<BillResponseDto>>(billDtos);
        }

        public IDataResult<List<Bill>> GetAllBills()
        {
            // Table ilişkisini de yüklemek için Include kullanın
            var bills = _billDal.GetList(
                include: query => query.Include(b => b.Table) // Table ilişkisini yükle
            );


            return new SuccessDataResult<List<Bill>>(bills);
        }
        public IDataResult<BillResponseDto> GetById(Guid id)
        {
            var bill = _billDal.Get(b => b.Id == id);
            var billDto = _mapper.Map<BillResponseDto>(bill);
            return new SuccessDataResult<BillResponseDto>(billDto);
        }

        public IDataResult<Bill> GetBillById(Guid id)
        {
            var bill = _billDal.Get(b => b.Id == id);
            return new SuccessDataResult<Bill>(bill);
        }
       
        public IResult Add(BillCreateDto billCreateDto)
        {
            IResult result = BusinessRules.Run(_billBusinessRules.CheckIfTableEmpty(billCreateDto.TableId));

            if(result!=null)
            {
                return result;
            }

            var bill = _mapper.Map<Bill>(billCreateDto);

            // StoreBillId'yi ayarla
            bill.StoreBillId = _storeBillService.GetByStatus(StoreBillStatus.Açık).Data.FirstOrDefault().Id;

            // Bill ekle
            _billDal.Add(bill);
            // Masa durumunu 'Dolu' olarak güncelle
            UpdateTableStatus(bill.TableId, TableStatus.Dolu);

            return new SuccessResult(Messages.BillAdded);
        }

       
        public IResult Update(BillUpdateDto billUpdateDto)
        {

            var existingBill = _billDal.Get(b => b.Id == billUpdateDto.Id);

            if (existingBill == null)
            {
                return new ErrorResult(Messages.BillNotFound);
            }

            _mapper.Map(billUpdateDto, existingBill);

            if ((BillStatus)billUpdateDto.Status == BillStatus.Tahsil_Edildi)
            {
                existingBill.ClosedDate = DateTime.Now;  // Kapama tarihini şu anki zamana ayarla
            }

            _billDal.Update(existingBill);
            return new SuccessResult(Messages.BillUpdated);
        }
        
        public IResult Delete(Guid id)
        {
            var bill = _billDal.Get(b => b.Id == id);

            if (bill != null)
            {
                IResult result = BusinessRules.Run(_billBusinessRules.CheckIfBillHasOrders(id));

                if (result != null)
                {
                    return result;
                }

                _billDal.Delete(bill);
                //Masa durumunu boş olarak güncelle
                UpdateTableStatus(bill.TableId, TableStatus.Boş);

                return new SuccessResult(Messages.BillDeleted);
            }
            return new ErrorResult(Messages.BillNotFound);
        }

        public IDataResult<List<BillResponseDto>> GetByTableId(Guid tableId)
        {
            var bills = _billDal.GetList(b => b.TableId == tableId);
            var billDtos = _mapper.Map<List<BillResponseDto>>(bills);
            return new SuccessDataResult<List<BillResponseDto>>(billDtos);
        }

        public IDataResult<List<BillResponseDto>> GetByStoreBillId(Guid id)
        {
            var bills = _billDal.GetList(
            predicate: b => b.StoreBillId == id,
            include: b => b.Include(b => b.Table)
        );
            var billResponseDto = _mapper.Map<List<BillResponseDto>>(bills);
            return new SuccessDataResult<List<BillResponseDto>>(billResponseDto);
        }

        private void UpdateTableStatus(Guid tableId, TableStatus status)
        {
            var table = _tableService.GetById(tableId).Data;
            if (table != null)
            {
                var tableUpdateDto = _mapper.Map<TableUpdateDto>(table);
                tableUpdateDto.Status = status;
                _tableService.Update(tableUpdateDto);
            }
        }
    }
}
