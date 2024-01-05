using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.AutoFac.Caching;
using Core.Aspects.AutoFac.Logging;
using Core.Aspects.AutoFac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
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
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        IOrderService _orderService;

        public ProductManager(IProductDal productDal, IOrderService orderService)
        {
            _productDal = productDal;
            _orderService = orderService;
        }
        [CacheRemoveAspect("Get")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            var result=BusinessRules.Run(CheckIfProductNameExist(product));
            if(result!=null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult Delete(Product product)
        {
            var result = BusinessRules.Run(CheckIfOrderOfProductExist(product));
            if (result != null)
            {
                return result;
            }
            _productDal.Delete(product);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<List<Product>> GetByCategoryId(int categoryId)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.CategoryId == categoryId).ToList());
        }

        public IDataResult<Product> GetById(int id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.Id == id));

        }

        public IDataResult<List<Product>> GetByUnitsInStock(int unitInStock)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList(p => p.UnitsInStock == unitInStock).ToList());
        }
       // [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList());
        }
       // [CacheAspect]
        [LogAspect(typeof(FileLogger))]
        public IDataResult<List<ProductDetailDto>> GetListProductDetailDto()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        public IResult Update(Product product)
        {
            var result = BusinessRules.Run(CheckIfProductNameExist(product));
            if (result != null)
            {
                return result;
            }
            _productDal.Update(product);
            return new SuccessResult(Messages.UpdatedSuccess);
        }
        private IResult CheckIfProductNameExist(Product product)
        {
            var result = _productDal.GetList(p => p.ProductName == product.ProductName && p.Id!=product.Id).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameExist);
            }
            return new SuccessResult();
        }
        private IResult CheckIfOrderOfProductExist(Product product)
        {
            var result=_orderService.GetByProductName(product.ProductName).Data.Any();
            if (result)
            {
                return new ErrorResult(Messages.OrderOfProductExist);
            }
            return new SuccessResult();
        }
    }
}
