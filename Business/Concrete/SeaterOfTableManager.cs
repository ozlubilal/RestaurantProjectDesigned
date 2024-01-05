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
    public class SeaterOfTableManager : ISeaterOfTableService
    {
        ISeaterOfTableDal _seaterOfTableDal;

        public SeaterOfTableManager(ISeaterOfTableDal seaterOfTableDal)
        {
            _seaterOfTableDal = seaterOfTableDal;
        }

        public IResult Add(SeaterOfTable seaterOfTable)
        {
            _seaterOfTableDal.Add(seaterOfTable);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult Delete(SeaterOfTable seaterOfTable)
        {
            _seaterOfTableDal.Delete(seaterOfTable);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<SeaterOfTable> GetById(int id)
        {
            return new SuccessDataResult<SeaterOfTable>(_seaterOfTableDal.Get(s => s.Id == id));
        }

        public IDataResult<List<SeaterOfTable>> GetList()
        {
            return new SuccessDataResult<List<SeaterOfTable>>(_seaterOfTableDal.GetList().ToList());
        }

        public IResult Update(SeaterOfTable seaterOfTable)
        {
            _seaterOfTableDal.Update(seaterOfTable);
            return new SuccessResult(Messages.UpdatedSuccess);
        }
    }
}
