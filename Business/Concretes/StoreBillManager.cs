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
using Entities.Concretes;
using Entities.Enums;

namespace Business.Concretes;

public class StoreBillManager : IStoreBillService
{
    private readonly IStoreBillDal _storeBillDal;
    private readonly StoreBillBusinessRules _storeBillBusinessRules;
    private readonly IMapper _mapper;

    public StoreBillManager(IStoreBillDal storeBillDal, StoreBillBusinessRules storeBillBusinessRules, IMapper mapper)
    {
        _storeBillDal = storeBillDal;
        _storeBillBusinessRules = storeBillBusinessRules;
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

        if (storeBill == null)
        {
            return new ErrorDataResult<StoreBillResponseDto>(Messages.StoreBillNotFound);
        }

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
        IResult result = BusinessRules.Run(_storeBillBusinessRules.CheckIfActiveStoreBillExists());

        if (result != null)
        {
            return result;
        }

        var storeBill = _mapper.Map<StoreBill>(storeBillCreateDto);
        _storeBillDal.Add(storeBill);
        return new SuccessResult(Messages.StoreBillAdded);
    }

    public IResult Update(StoreBillUpdateDto storeBillUpdateDto)
    {
        // Mevcut varlığı al
        var existingStoreBill = _storeBillDal.Get(sb => sb.Id == storeBillUpdateDto.Id);
        if (existingStoreBill == null)
        {
            return new ErrorResult(Messages.StoreBillNotFound);

        }

        _mapper.Map(storeBillUpdateDto, existingStoreBill);

        _storeBillDal.Update(existingStoreBill);
        return new SuccessResult(Messages.StoreBillUpdated);
    }

    public IResult Delete(Guid id)
    {
        var storeBill = _storeBillDal.Get(sb => sb.Id == id);
        if (storeBill != null)
        {
            IResult result = BusinessRules.Run(_storeBillBusinessRules.CheckIfStoreBillHasBills(id));

            if (result != null)
            {
                return result;
            }

            _storeBillDal.Delete(storeBill);
            return new SuccessResult(Messages.StoreBillDeleted);
        }
        return new ErrorResult(Messages.StoreBillNotFound);
    }
}
