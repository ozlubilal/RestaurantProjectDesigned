using Business.Dtos.Requests;
using Business.Dtos.Responses;
using Core.Utilities.Results;
using Entities.Enums;

namespace Business.Abstracts;

public interface IStoreBillService
{
    IDataResult<List<StoreBillResponseDto>> GetList();
    IDataResult<StoreBillResponseDto> GetById(Guid id);
    IDataResult<List<StoreBillResponseDto>> GetByStatus(StoreBillStatus status);
    IResult Add(StoreBillCreateDto storeBillCreateDto);
    IResult Update(StoreBillUpdateDto storeBillUpdateDto);
    IResult Delete(Guid id);
}
