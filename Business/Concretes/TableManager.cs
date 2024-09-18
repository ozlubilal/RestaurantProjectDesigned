using AutoMapper;
using Business.Abstracts;
using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concrete;

namespace Business.Concretes;

public class TableManager : ITableService
{
    private readonly ITableDal _tableDal;
    private readonly IMapper _mapper;

    public TableManager(ITableDal tableDal, IMapper mapper)
    {
        _tableDal = tableDal;
        _mapper = mapper;
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
        var table = _mapper.Map<Table>(tableCreateDto);
        _tableDal.Add(table);
        return new SuccessResult("Table added successfully.");
    }

    public IResult Update(TableUpdateDto tableDto)
    {
        // Varlığı alın ve güncelleme işlemlerini gerçekleştirin
        var existingTable = _tableDal.Get(t=>t.Id==tableDto.Id);
        if (existingTable == null)
        {
            return new ErrorResult("Masa bulunamadı.");
        }

        // AutoMapper kullanarak güncellenmiş özellikleri varlığa uygulayın
        _mapper.Map(tableDto, existingTable);

        _tableDal.Update(existingTable);
        return new SuccessResult("Masa başarıyla güncellendi.");
    }


    public IResult Delete(Guid id)
    {
        var table = _tableDal.Get(t => t.Id == id);
        if (table != null)
        {
            _tableDal.Delete(table);
            return new SuccessResult("Table deleted successfully.");
        }
        return new ErrorResult("Table not found.");
    }
}
