using Business.Abstracts;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Enums;

namespace Business.BusinessRule;

public class BillBusinessRules
{
    private readonly IOrderDal _orderDal;
    private readonly ITableService _tableService;

    public BillBusinessRules(IOrderDal orderDal, ITableService tableService)
    {
        _orderDal = orderDal;
        _tableService = tableService;
    }

    public IResult CheckIfBillHasOrders(Guid billId)
    {
        // Ürünleri kontrol etmek için IProductDal kullanılıyor
        var hasOrders = _orderDal.GetList(o => o.BillId == billId).Any();

        if (hasOrders)
        {
            return new ErrorResult(Messages.ProductHasOrder);
        }
        return new SuccessResult();
    }
    public IResult CheckIfTableEmpty(Guid tableId)
    {
        var table=_tableService.GetById(tableId).Data;

        if(table!=null &&  table.Status==TableStatus.Dolu)
        {
            return new ErrorResult(Messages.TableNotEmpty);
        }
        return new SuccessResult();
    }
}

