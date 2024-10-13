using AutoMapper;
using Business.Abstracts;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes;

public class VisitorManager : IVisitorService
{
    private readonly IVisitorDal _visitorDal; 

    public VisitorManager(IVisitorDal visitorDal)
    {
        _visitorDal = visitorDal;
    }

    public IDataResult<List<Visitor>> GetAll()
    {
        var visitors = _visitorDal.GetList(); 
        return new SuccessDataResult<List<Visitor>>(visitors); 
    }

    public IDataResult<Visitor> GetVisitorByDate(DateTime date)
    {
        var visitor = _visitorDal.Get(v => v.VisitTime.Date == date.Date); 
        return visitor != null
            ? new SuccessDataResult<Visitor>(visitor)
            : new ErrorDataResult<Visitor>("Visitor not found for the specified date."); 
    }

    public IResult Add(Visitor visitor)
    {
        visitor.VisitTime = DateTime.Now;
        _visitorDal.Add(visitor); 
        return new SuccessResult("Visitor added successfully."); 
    }

    public IResult Delete(Guid id)
    {
        var visitor = _visitorDal.Get(v => v.Id == id); 
        if (visitor != null)
        {
            _visitorDal.Delete(visitor); 
            return new SuccessResult("Visitor deleted successfully.");
        }
        return new ErrorResult("Visitor not found."); 
    }

    public IResult DeleteAll()
    {
        var visitors=_visitorDal.GetList();
        foreach (var visitor in visitors)
        {
            _visitorDal.Delete(visitor);
        }
        return new SuccessResult();
    }
}
