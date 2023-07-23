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
    public class EfCoreProductDal : EfCoreGenericRepository<Product, ProjectContext>, IProductDal
    {
        public override IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (var context = new ProjectContext())
            {
                var products = context.Products.Include("Images").AsQueryable();

                return filter == null ? products.ToList() : products.Where(filter).ToList();
            }
        }
        public Product GetProductDetails(int id)
        {
            using (var db = new ProjectContext())
            {
                return db.Products.Where(i => i.Id == id)
                            .Include("Images")
                            .Include(i => i.ProductCategories)
                            .ThenInclude(i => i.Category)
                            .FirstOrDefault();
            }
        }

        public List<Product> GetProductsByCategory(string category,int page, int pageSize)
        {
            using(var context = new ProjectContext())
            {
                var products = context.Products.Include("Images").AsQueryable();

                if (category != null)
                {
                    products = products.Include(i => i.ProductCategories).ThenInclude(i => i.Category)
                        .Where(i => i.ProductCategories.Any(a => a.Category.Name.ToLower() == category.ToLower()));
                }

                return products.Skip((page-1)*pageSize).Take(pageSize).ToList();
            }
        }
    }
}

