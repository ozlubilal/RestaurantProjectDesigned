using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetList();
        IDataResult<List<ProductDetailDto>> GetListProductDetailDto();
        IDataResult<Product> GetById(int id);
        IDataResult<List<Product>> GetByCategoryId(int categoryId);
        IDataResult<List<Product>> GetByUnitsInStock(int unitInStock);
        IResult Add(Product product);
        IResult Delete(Product product);
        IResult Update(Product product);
    }
}
