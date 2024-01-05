using Business.Abstract;
using Business.Constans;
using Core.Aspects.AutoFac.Logging;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class FloorManager : IFloorService
    {
        IFloorDal _floorDal;
        ITableService _tableService;

        public FloorManager(IFloorDal floorDal, ITableService tableService)
        {
            _floorDal = floorDal;
            _tableService = tableService;
        }

        public IResult Add(Floor floor)
        {
            var result = BusinessRules.Run(CheckIfFloorNameExist(floor));
            if (result != null)
            {
                return result;
            }
            _floorDal.Add(floor);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult Delete(Floor floor)
        {
            var result = BusinessRules.Run(CheckIfTableOfFloorExist(floor));
            if (result != null)
            {
                return result;
            }
            _floorDal.Delete(floor);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<Floor> GetById(int id)
        {
            return new SuccessDataResult<Floor>(_floorDal.Get(f => f.Id == id));
        }
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Floor>> GetAll()
        {
            return new SuccessDataResult<List<Floor>>(_floorDal.GetList());
        }

        public IResult Update(Floor floor)
        {
            var result = BusinessRules.Run(CheckIfFloorNameExist(floor));
            if (result != null)
            {
                return result;
            }
            _floorDal.Update(floor);
            return new SuccessResult(Messages.UpdatedSuccess);
        }
        private IResult CheckIfFloorNameExist(Floor floor)
        {
            var result = _floorDal.GetList(f => f.FloorName == floor.FloorName && f.Id != floor.Id).Any();
            if (result)
            {
                return new ErrorResult(Messages.FloorNameExist);
            }
            return new SuccessResult();
        }
        private IResult CheckIfTableOfFloorExist(Floor floor)
        {
            var result = _tableService.GetByFloorId(floor.Id).Data.Any();
            if (result)
            {
                return new ErrorResult(Messages.TableOfFloorExist);
            }
            return new SuccessResult();
        }
    }
}
