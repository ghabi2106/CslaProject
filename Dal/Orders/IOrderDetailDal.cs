using Dal.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Orders
{
    public interface IOrderDetailDal
    {
        List<OrderDetailDto> Fetch(Guid orderId);
        OrderDetailDto Fetch(Guid orderDetailId, Guid orderId);
        void Insert(OrderDetailDto item);
        void Update(OrderDetailDto item);
        void Delete(Guid orderDetailId);
    }
}
