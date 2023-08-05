using EShopper.DataAccess.Abstract;
using EShopper.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.DataAccess.Concrete.EfCore
{
    public class EfCoreCategoryDal : EfCoreGenericRepository<Category, ProjectContext>, ICategoryDal
    {
        public void DeleteFromCategory(int categoryId, int productId)
        {
            using (var context = new ProjectContext())
            {
                //var cmd = @"delete from ProductCategory where categoryId=@p0 and productId=@p1";

                //context.Database.ExecuteSqlRaw(cmd, categoryId, productId);


                var cat = context.ProductCategories.Where(i => i.ProductId == productId && i.CategoryId == categoryId).FirstOrDefault();

                context.ProductCategories.Remove(cat);
                context.SaveChanges();
            }
        }

        public Category GetByIdWithProducts(int id)
        {
            using(var context = new ProjectContext())
            {
                return context.Categories.Where(i => i.Id == id)
                        .Include(i => i.ProductCategories)
                        .ThenInclude(i => i.Product)
                        .ThenInclude(i => i.Images)
                        .FirstOrDefault();
                        
            }
        }
    }
}
