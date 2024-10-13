using AutoMapper;
using Business.Abstracts;
using Business.BusinessRule;
using Business.Constants;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;

namespace Business.Concretes;

public class TableManager : ITableService
{
    private readonly ITableDal _tableDal;
    private readonly IMapper _mapper;
    private readonly TableBusinessRules _tableBusinessRules;

    public TableManager(ITableDal tableDal, IMapper mapper,TableBusinessRules tableBusinessRules)
    {
        _tableDal = tableDal;
        _mapper = mapper;
        _tableBusinessRules = tableBusinessRules;
    }

    public IDataResult<List<TableResponseDto>> GetList()
    {
        var tables = _tableDal.GetList();
        var tableDtos = _mapper.Map<List<TableResponseDto>>(tables);
        return new SuccessDataResult<List<TableResponseDto>>(tableDtos);
    }

    public IDataResult<TableResponseDto> GetById(Guid id)
    {
        var table = _tableDal.Get(t => t.Id == id);
        var tableDto = _mapper.Map<TableResponseDto>(table);
        return new SuccessDataResult<TableResponseDto>(tableDto);
    }

    public IResult Add(TableCreateDto tableCreateDto)
    {
        IResult result = BusinessRules.Run(_tableBusinessRules.CheckIfTableNameExist(tableCreateDto.TableName,tableCreateDto.Id));

        if (result != null)
        {
            return result;
        }
        var table = _mapper.Map<Table>(tableCreateDto);
        _tableDal.Add(table);
        return new SuccessResult(Messages.TableAdded);
    }

    public IResult Update(TableUpdateDto tableDto)
    {
        // Varlığı alın ve güncelleme işlemlerini gerçekleştirin
        var existingTable = _tableDal.Get(t=>t.Id==tableDto.Id);
        if (existingTable == null)
        {
            return new ErrorResult(Messages.TableNotFound);
        }

        // AutoMapper kullanarak güncellenmiş özellikleri varlığa uygulayın
        _mapper.Map(tableDto, existingTable);

        _tableDal.Update(existingTable);
        return new SuccessResult(Messages.TableUpdated);
    }


    public IResult Delete(Guid id)
    {
        IResult result = BusinessRules.Run(_tableBusinessRules.CheckIfTableHasBills(id));

        if (result != null)
        {
            return result;
        }


        var table = _tableDal.Get(t => t.Id == id);
        if (table != null)
        {
            _tableDal.Delete(table);
            return new SuccessResult(Messages.TableDeleted);
        }
        return new ErrorResult(Messages.TableNotFound);
    }
}
