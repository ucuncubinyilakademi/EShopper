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
