using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstracts;

public class CategoryBusinessRules
{
    private readonly ICategoryDal _categoryDal;
    private readonly IProductDal _productDal; 

    public CategoryBusinessRules(ICategoryDal categoryDal, IProductDal productDal) 
    {
        _categoryDal = categoryDal;
        _productDal = productDal;
    }

    public IResult CheckIfCategoryNameExist(string categoryName, Guid categoryId)
    {
        var result = _categoryDal.GetList(c => c.Name == categoryName && c.Id != categoryId).Any();
        if (result)
        {
            return new ErrorResult(Messages.CategoryNameAlreadyExists);
        }
        return new SuccessResult();
    }

    public IResult CheckIfCategoryHasProducts(Guid categoryId)
    {
        // Ürünleri kontrol etmek için IProductDal kullanılıyor
        var hasProducts = _productDal.GetList(p => p.CategoryId == categoryId).Any();

        if (hasProducts)
        {
            return new ErrorResult(Messages.CategoryHasProduct);
        }
        return new SuccessResult();
    }
}
