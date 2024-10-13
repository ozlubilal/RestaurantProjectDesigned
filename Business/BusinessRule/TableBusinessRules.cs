using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;

namespace Business.BusinessRule;

public class TableBusinessRules
{
    private readonly ITableDal _tableDal;
    private readonly IBillDal _billDal;

    public TableBusinessRules(ITableDal tableDal, IBillDal billDal)
    {
        _tableDal = tableDal;
        _billDal = billDal;
    }

    public IResult CheckIfTableNameExist(string tableName, Guid tableId)
    {
        var result = _tableDal.GetList(p => p.TableName == tableName && p.Id != tableId).Any();

        if (result)
        {
            return new ErrorResult(Messages.TableNameAlreadyExists);
        }

        return new SuccessResult();
    }

    public IResult CheckIfTableHasBills(Guid tableId)
    {
       
        var hasOrders = _billDal.GetList(p => p.TableId == tableId).Any();

        if (hasOrders)
        {
            return new ErrorResult(Messages.TableHasOrder);
        }
        return new SuccessResult();
    }
}
