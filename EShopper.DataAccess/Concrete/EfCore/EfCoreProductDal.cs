using EShopper.DataAccess.Abstract;
using EShopper.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.DataAccess.Concrete.EfCore
{
    public class EfCoreProductDal : EfCoreGenericRepository<Product,ProjectContext>, IProductDal
    {
        public override IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using(var context = new ProjectContext())
            {
               var products = context.Products.Include("Images").AsQueryable();

                return filter == null ? products.ToList() : products.Where(filter).ToList();
            }
        }
    }
}
