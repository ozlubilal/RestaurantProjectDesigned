using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRule;

public class StoreBillBusinessRules
{
    private readonly IStoreBillDal _storeBillDal;
    private readonly IBillDal _billDal;

 


    public IResult CheckIfActiveStoreBillExists()
    {
        var ressult = _storeBillDal.GetList(s=>s.Status==StoreBillStatus.Açık).Any();

        if(ressult)
        {
            return new ErrorResult(Messages.ActiveStoreBillAlreadyExists);
        }
        return new SuccessResult();
    }
    public IResult CheckIfStoreBillHasBills(Guid storeBillId)
    {
        var result = _billDal.GetList(b=>b.StoreBillId==storeBillId).Any();

        if(result)
        {
            return new ErrorResult(Messages.StoreBillHasBills);
        }
        return new SuccessResult();
    }
}
