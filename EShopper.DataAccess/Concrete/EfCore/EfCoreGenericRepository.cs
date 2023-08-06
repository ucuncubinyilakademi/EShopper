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
    public class EfCoreGenericRepository<T,TContext>:IRepository<T>
        where T:class
        where TContext:DbContext,new()
    {
        public void Create(T entity)
        {
            using(var db = new TContext())
            {
                db.Set<T>().Add(entity);
                db.SaveChanges();
            }          
        }

        public void Delete(T entity)
        {
            using (var db = new TContext())
            {
                db.Set<T>().Remove(entity);
                db.SaveChanges();
            }           
        }

        public virtual IEnumerable<T> GetAll(Expression<Func<T, bool>> filter=null)
        {
            using (var db = new TContext())
            {
                if (filter == null)
                {
                    return db.Set<T>().ToList();
                }
                return db.Set<T>().Where(filter).ToList();
            }           
        }

        public virtual T GetById(int id)
        {
            using (var db = new TContext())
            {
                return db.Set<T>().Find(id);
                //return db.Products.Where(i => i.Id == id).FirstOrDefault();
                //return db.Products.FirstOrDefault(i => i.Id == id);
            }
        }

        public T GetOne(Expression<Func<T, bool>> filter)
        {
            using (var db = new TContext())
            {
                return db.Set<T>().FirstOrDefault(filter);
            }           
        }

        public virtual void Update(T entity)
        {
            using (var db = new TContext())
            {
                db.Entry(entity).State = EntityState.Modified;
                db.SaveChanges();
            }
            
        }
    }
}
