using EShopper.DataAccess.Abstract;
using EShopper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.DataAccess.Concrete.Memory
{
    public class MemoryProductDal : IProductDal
    {
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
            var products = new List<Product>
            {
                new Product{Name="IPhone X16",Price=50000,Images={ new Image {ImageUrl="1.jpg"}} },
                new Product{Name="IPhone X17",Price=60000,Images={ new Image {ImageUrl="2.jpg"}} },
                new Product{Name="IPhone X18",Price=70000,Images={ new Image {ImageUrl="3.jpg"}} }
            };

            return products.ToList();
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Product GetOne(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
