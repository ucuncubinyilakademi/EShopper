using EShopper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.Business.Abstract
{
    public interface ICategoryService
    {
        IQueryable<Category> GetAll(Expression<Func<Category, bool>> filter);

        void Create(Category entity);
        void Update(Category entity);
        void Delete(Category entity);
    }
}
