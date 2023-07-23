using EShopper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter=null);
        Product GetProductDetails(int id);
        void Create(Product entity);
        void Update(Product entity);
        void Delete(Product entity);
    }
}
