using EShopper.Business.Abstract;
using EShopper.DataAccess.Abstract;
using EShopper.DataAccess.Concrete.EfCore;
using EShopper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        public void Create(Product entity, int[] categoryIds)
        {
            _productDal.Create(entity,categoryIds);
        }

        public void Delete(Product entity)
        {
            _productDal.Delete(entity);
        }

        public IEnumerable<Product> GetAll(Expression<Func<Product, bool>> filter=null)
        {
            return _productDal.GetAll(filter);
        }

        public Product GetById(int id)
        {
            return _productDal.GetById(id);
        }

        public Product GetByIdWithCategories(int id)
        {
            return _productDal.GetByIdWithCategories(id);
        }

        public int GetCountbyCategory(string category)
        {
            return _productDal.GetCountByCategory(category);
        }

        public Product GetProductDetails(int id)
        {
            return _productDal.GetProductDetails(id);
        }

        public List<Product> GetProductsByCategory(string category, int page,int pageSize)
        {
            return _productDal.GetProductsByCategory(category,page,pageSize);
        }

        public void Update(Product entity)
        {
            using(var db = new ProjectContext())
            {
                db.Images.RemoveRange(db.Images.Where(i => i.ProductId == entity.Id).ToList());

                var product = db.Products.Where(i => i.Id == entity.Id).FirstOrDefault();

                product.Description = entity.Description;
                product.Name = entity.Name;
                product.Price = entity.Price;
                product.Images = entity.Images;

                db.SaveChanges();
            }
        }

        public void Update(Product entity, int[] categoryIds)
        {
            _productDal.Update(entity, categoryIds);
        }
    }
}
