using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.Enums;

namespace Business.Concretes;

public class StoreBillManager : IStoreBillService
{
    private readonly IStoreBillDal _storeBillDal;
    private readonly IMapper _mapper;

    public StoreBillManager(IStoreBillDal storeBillDal, IMapper mapper)
    {
        _storeBillDal = storeBillDal;
        _mapper = mapper;
    }

    public IDataResult<List<StoreBillResponseDto>> GetList()
    {
        var storeBills = _storeBillDal.GetList();
        var storeBillDtos = _mapper.Map<List<StoreBillResponseDto>>(storeBills);
        return new SuccessDataResult<List<StoreBillResponseDto>>(storeBillDtos);
    }

    public IDataResult<StoreBillResponseDto> GetById(Guid id)
    {
        var storeBill = _storeBillDal.Get(sb => sb.Id == id);
        var storeBillDto = _mapper.Map<StoreBillResponseDto>(storeBill);
        return new SuccessDataResult<StoreBillResponseDto>(storeBillDto);
    }

    public IDataResult<List<StoreBillResponseDto>> GetByStatus(StoreBillStatus status)
    {
        var storeBills = _storeBillDal.GetList(sb => sb.Status == status);
        var storeBillDtos = _mapper.Map<List<StoreBillResponseDto>>(storeBills);
        return new SuccessDataResult<List<StoreBillResponseDto>>(storeBillDtos);
    }

    public IResult Add(StoreBillCreateDto storeBillCreateDto)
    {
        var storeBill = _mapper.Map<StoreBill>(storeBillCreateDto);
        _storeBillDal.Add(storeBill);
        return new SuccessResult("StoreBill added successfully.");
    }

    public IResult Update(StoreBillUpdateDto storeBillUpdateDto)
    {
        // Mevcut varlığı al
        var existingStoreBill = _storeBillDal.Get(sb => sb.Id == storeBillUpdateDto.Id);
        if (existingStoreBill == null)
        {
            return new ErrorResult("StoreBill not found.");
           
        }

        // DTO'yu mevcut varlık üzerine eşle
        _mapper.Map(storeBillUpdateDto, existingStoreBill);

        // Güncelleme işlemi
        _storeBillDal.Update(existingStoreBill);
        return new SuccessResult("StoreBill updated successfully.");
    }

    public IResult Delete(Guid id)
    {
        var storeBill = _storeBillDal.Get(sb => sb.Id == id);
        if (storeBill != null)
        {
            _storeBillDal.Delete(storeBill);
            return new SuccessResult("StoreBill deleted successfully.");
        }
        return new ErrorResult("StoreBill not found.");
    }
}
