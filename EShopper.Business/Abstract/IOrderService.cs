using EShopper.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShopper.Business.Abstract
{
    public interface IOrderService
    {
        void Create(Order entity);
    }
}
