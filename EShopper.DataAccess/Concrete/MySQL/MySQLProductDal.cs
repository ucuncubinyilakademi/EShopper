using EShopper.DataAccess.Abstract;
using EShopper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.DataAccess.Concrete.MySQL
{
    public class MySQLProductDal : IProductDal
    {
        public void Create(Product entity, int[] categoryIds)
        {
            throw new NotImplementedException();
        }

        public void Create(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetByIdWithCategories(int id)
        {
            throw new NotImplementedException();
        }

        public int GetCountByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public Product GetOne(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public Product GetProductDetails(int id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByCategory(string category)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetProductsByCategory(string category, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity, int[] categoryIds)
        {
            throw new NotImplementedException();
        }
    }
}
