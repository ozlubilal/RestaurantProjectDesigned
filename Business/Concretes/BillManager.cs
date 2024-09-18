using AutoMapper;
using Business.Abstract;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;
using Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class BillManager : IBillService
    {
        private readonly IBillDal _billDal;
        private readonly IMapper _mapper;
        private readonly IStoreBillService _storeBillService;

        public BillManager(IBillDal billDal, IMapper mapper,IStoreBillService storeBillService)
        {
            _billDal = billDal;
            _mapper = mapper;
            _storeBillService = storeBillService;
        }

        public IDataResult<List<BillResponseDto>> GetAll()
        {
            // Table ilişkisini de yüklemek için Include kullanın
            var bills = _billDal.GetList(
                include: query => query.Include(b => b.Table) // Table ilişkisini yükle
            );

            // DTO listesi oluştur
            var billDtos = _mapper.Map<List<BillResponseDto>>(bills);

            return new SuccessDataResult<List<BillResponseDto>>(billDtos);
        }


        public IDataResult<BillResponseDto> GetById(Guid id)
        {
            var bill = _billDal.Get(b => b.Id == id);
            var billDto = _mapper.Map<BillResponseDto>(bill);
            return new SuccessDataResult<BillResponseDto>(billDto);
        }

        public IResult Add(BillCreateDto billCreateDto)
        {
            var bill = _mapper.Map<Bill>(billCreateDto);
            bill.StoreBillId = _storeBillService.GetByStatus(StoreBillStatus.Open).Data.FirstOrDefault().Id;
            _billDal.Add(bill);
            return new SuccessResult("Bill added successfully.");
        }

        public IResult Update(BillUpdateDto billUpdateDto)
        {
            var bill = _mapper.Map<Bill>(billUpdateDto);
            _billDal.Update(bill);
            return new SuccessResult("Bill updated successfully.");
        }

        public IResult Delete(Guid id)
        {
            var bill = _billDal.Get(b => b.Id == id);
            if (bill != null)
            {
                _billDal.Delete(bill);
                return new SuccessResult("Bill deleted successfully.");
            }
            return new ErrorResult("Bill not found.");
        }

        public IDataResult<List<BillResponseDto>> GetByTableId(Guid tableId)
        {
            var bills = _billDal.GetList(b => b.TableId == tableId);
            var billDtos = _mapper.Map<List<BillResponseDto>>(bills);
            return new SuccessDataResult<List<BillResponseDto>>(billDtos);
        }
    }
}
