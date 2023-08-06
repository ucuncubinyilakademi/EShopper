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
    public class EfCoreCartDal : EfCoreGenericRepository<Cart, ProjectContext>, ICartDal
    {
        public void AddToCart(string userId, int productId, int quantity)
        {
            var cart = GetCartByUserId(userId);
            if (cart != null)
            {
                var index = cart.CartItems.FindIndex(i => i.ProductId == productId);

                if (index < 0)
                {
                    cart.CartItems.Add(new CartItem()
                    {
                        ProductId = productId,
                        Quantity = quantity,
                        CartId = cart.Id
                    });
                }
                else
                {
                    cart.CartItems[index].Quantity += quantity;
                }

                Update(cart);
            }
        }
        public override void Update(Cart entity)
        {
            using (var context = new ProjectContext())
            {
                context.Carts.Update(entity);
                context.SaveChanges();
            }
        }
        public Cart GetCartByUserId(string userId)
        {
            using(var context = new ProjectContext())
            {
                return context.Carts
                    .Include(i => i.CartItems)
                    .ThenInclude(i => i.Product)
                    .ThenInclude(i=>i.Images)
                    .FirstOrDefault(i=> i.UserId==userId);
            }
        }

        public void DeleteFromCart(int Id, int productId)
        {
            using (var context = new ProjectContext())
            {
                var cmd = @"Delete from CartItem where CartId=@p0 and ProductId=@p1";
                context.Database.ExecuteSqlRaw(cmd, Id, productId);
            }
        }

        public void ClearCart(string cartId)
        {
            using (var context = new ProjectContext())
            {
                var cmd = @"Delete from CartItem where CartId=@p0";
                context.Database.ExecuteSqlRaw(cmd, cartId);
            }
        }
    }
}
