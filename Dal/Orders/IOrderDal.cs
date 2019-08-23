using Dal.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Orders
{
    public interface IOrderDal
    {
        List<OrderDto> Fetch(int idCustomer);
        OrderDto Fetch(Guid orderId);
        void Insert(OrderDto item);
        void Update(OrderDto item);
        void Delete(Guid orderId);
    }
}
