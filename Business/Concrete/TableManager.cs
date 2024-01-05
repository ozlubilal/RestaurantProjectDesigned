using Business.Abstract;
using Business.Constans;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TableManager : ITableService
    {
        ITableDal _tableDal;

        public TableManager(ITableDal tableDal)
        {
            _tableDal = tableDal;
        }

        public IResult Add(Table table)
        {
            var result = BusinessRules.Run(CheckIfTableNameExist(table));
            if (result != null)
            {
                return result;
            }
            //yeni eklenen masanın masa durumunu boş yapıyoruz
            table.TableStatusId = 1;
            _tableDal.Add(table);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult Delete(Table table)
        {
            _tableDal.Delete(table);
            return new SuccessResult(Messages.DeletedSuccess);
        }
        public IResult ChangeTableStatuId(int tableId, int tableStatuId)
        {
            var table = _tableDal.Get(t => t.Id == tableId);
            table.TableStatusId = tableStatuId;
            _tableDal.Update(table);
            return new SuccessResult();
        }



        public IDataResult<Table> GetById(int id)
        {
            return new SuccessDataResult<Table>(_tableDal.Get(t => t.Id == id));
        }
        public IDataResult<List<Table>> GetByFloorId(int floorId)
        {
            return new SuccessDataResult<List<Table>>(_tableDal.GetList(t => t.FloorId == floorId).ToList());
        }

        public IDataResult<List<Table>> GetList()
        {
            return new SuccessDataResult<List<Table>>(_tableDal.GetList().ToList());
        }

        public IDataResult<List<TableDetailDto>> GetListTableDetailDto()
        {
            return new SuccessDataResult<List<TableDetailDto>>(_tableDal.GetTableDetails().ToList());
        }


        public IResult Update(Table table)
        {
            var result = BusinessRules.Run(CheckIfTableNameExist(table));
            if (result != null)
            {
                return result;
            }
            _tableDal.Update(table);
            return new SuccessResult(Messages.UpdatedSuccess);
        }
        private IResult CheckIfTableNameExist(Table table)
        {
            var result = _tableDal.GetList(t => t.TableName == table.TableName && t.Id!=table.Id).Any();
            if (result)
            {
                return new ErrorResult(Messages.TableNameExist);
            }
            return new SuccessResult();
        }

        public IDataResult<List<Table>> GetByStatuId(int statuId)
        {
            return new SuccessDataResult<List<Table>>(_tableDal.GetList(t=>t.TableStatusId==statuId).ToList());
        }
        //private IResult CheckIfOrderOfProductExist(Product product)
        //{
        //    var result = _orderService.GetByProductName(product.ProductName).Data.Any();
        //    if (result)
        //    {
        //        return new ErrorResult();
        //    }
        //    return new SuccessResult();
        //}
    }
}
