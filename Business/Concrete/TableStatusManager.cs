using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TableStatusManager : ITableStatusService
    {
        ITableStatusDal _tableStatusDal;

        public TableStatusManager(ITableStatusDal tableStatusDal)
        {
            _tableStatusDal = tableStatusDal;
        }

        public IResult Add(TableStatus tableStatus)
        {
            _tableStatusDal.Add(tableStatus);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult Delete(TableStatus tableStatus)
        {
            _tableStatusDal.Delete(tableStatus);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<TableStatus> GetById(int id)
        {
            return new SuccessDataResult<TableStatus>(_tableStatusDal.Get(t => t.Id == id));
        }

        public IDataResult<List<TableStatus>> GetList()
        {
            return new SuccessDataResult<List<TableStatus>>(_tableStatusDal.GetList().ToList());
        }

        public IResult Update(TableStatus tableStatus)
        {
            _tableStatusDal.Update(tableStatus);
            return new SuccessResult(Messages.UpdatedSuccess);
        }
    }
}

