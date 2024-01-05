using Business.Abstract;
using Business.BusinessAspect.Autofac;
using Business.Constans;
using Core.Utilities.Business;
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
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;
        IProductService _productService;

        public CategoryManager(ICategoryDal categoryDal, IProductService productService)
        {
            _categoryDal = categoryDal;
            _productService = productService;
        }

        public IResult Add(Category category)
        {
            var result=BusinessRules.Run(CheckIfCategoryNameExist(category));
            if(result!=null)
            {
                return result;
            }
            _categoryDal.Add(category);
            return new SuccessResult(Messages.AddedSuccess);
        }

        public IResult Delete(Category category)
        {
            var result=BusinessRules.Run(CheckIfProductOfTheCategoryExist(category));
            if(result!=null)
            {
                return result;
            }
           
            _categoryDal.Delete(category);
            return new SuccessResult(Messages.DeletedSuccess);
        }

        public IDataResult<Category> GetById(int id)
        {
            return new SuccessDataResult<Category>(_categoryDal.Get(c => c.Id == id));
        }

        public IDataResult<List<Category>> GetList()
        {
            return new SuccessDataResult<List<Category>>(_categoryDal.GetList().ToList());
        }

        public IResult Update(Category category)
        {
            var result = BusinessRules.Run(CheckIfCategoryNameExist(category));
            if (result != null)
            {
                return result;
            }
            _categoryDal.Update(category);
            return new SuccessResult(Messages.UpdatedSuccess);
        }
        private IResult CheckIfCategoryNameExist(Category category)
        {
            var result=_categoryDal.GetList(c=>c.CategoryName== category.CategoryName&&c.Id!=category.Id).Any();
            if (result)
            {
                return new ErrorResult(Messages.CategoryNameExist);
            }
            return new SuccessResult();
        }
        //Katgoriye ait ürün varmı
        private IResult CheckIfProductOfTheCategoryExist(Category category)
        {

            var result = _productService.GetByCategoryId(category.Id).Data.Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductOfTheCategoryExist);
            }
            return new SuccessResult();
        }


    }
}
