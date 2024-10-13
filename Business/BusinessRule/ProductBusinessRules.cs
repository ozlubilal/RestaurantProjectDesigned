using Business.Abstracts;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.BusinessRule
{
    public class ProductBusinessRules
    {
        private readonly IProductDal _productDal;
        private readonly ICategoryService _categoryService;
        private readonly IOrderDal _orderDal;

        public ProductBusinessRules(IProductDal productDal,ICategoryService categoryService, IOrderDal orderDal)
        {
            _productDal = productDal;
            _categoryService = categoryService;
            _orderDal = orderDal;
        }

        public IResult CheckIfProductNameExist(string productName, Guid productId)
        {
            var result = _productDal.GetList(p => p.Name == productName && p.Id!=productId).Any();

            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }

            return new SuccessResult();
        }
        public IResult CheckIfCategoryExists(Guid categoryId)
        {
            var category = _categoryService.GetById(categoryId).Data;
            if (category == null)
            {
                return new ErrorResult(Messages.CategoryNotFound);
            }
            return new SuccessResult();
        }

        public IResult CheckIfProductHasOrders(Guid productId)
        {
            var hasOrders = _orderDal.GetList(p => p.ProductId == productId).Any();

            if (hasOrders)
            {
                return new ErrorResult(Messages.ProductHasOrder);
            }
            return new SuccessResult();
        }
    }
}