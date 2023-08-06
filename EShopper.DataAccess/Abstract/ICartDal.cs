using EShopper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.DataAccess.Abstract
{
    public interface ICartDal : IRepository<Cart>
    {
        void AddToCart(string userId, int productId, int quantity);
        void ClearCart(string cartId);
        void DeleteFromCart(int Id, int productId);
        Cart GetCartByUserId(string userId);
    }
}
