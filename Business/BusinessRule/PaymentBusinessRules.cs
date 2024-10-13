using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using Entities.Enums;

namespace Business.BusinessRule
{
    public class PaymentBusinessRules
    {
        private readonly IOrderDal _orderDal;

        public PaymentBusinessRules(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public IResult ValidateOrdersStatus(Guid billId)
        {
            var orders = _orderDal.GetList(o => o.BillId == billId && o.Status!=OrderStatus.Servis_Edildi).Any();
            if (orders)
            {
                return new ErrorResult(Messages.OrdersMustBeServed);
            }
            return new SuccessResult();
        }
    }
}
