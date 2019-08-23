using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Csla.Data;
using Dal;
using Csla.Data.EF6;
using Dal.Orders;

namespace DalEF
{
    public class OrderDal:IOrderDal
    {
        public OrderDto Fetch(Guid orderId)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var result = ctx.DbContext.Orders.Where(o => (o.OrderId.Equals(orderId) && o.Enable.Equals(true)))
                    .Select(o => new OrderDto
                    {
                        IdCustomer = o.CustomerIdCustomer,
                        Id = o.OrderId,
                        OrderDate = o.OrderDate,
                        Status = o.Status,
                        ShippedDate = o.ShippedDate,
                        ReceivedDate = o.ReceivedDate
                    }).FirstOrDefault();
                return result;
            }
        }

        private OrderDto EntityToDto(Order o)
        {
            return new OrderDto
            {
                IdCustomer = o.CustomerIdCustomer,
                Id = o.OrderId,
                OrderDate = o.OrderDate,
                Status = o.Status,
                ShippedDate = o.ShippedDate,
                ReceivedDate = o.ReceivedDate
            };
        }

        public List<OrderDto> Fetch(int idCustomer)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var result = from o in ctx.DbContext.Orders
                             where o.Enable.Equals(true) && o.CustomerIdCustomer.Equals(idCustomer)
                             orderby o.OrderDate
                             select new OrderDto
                             {
                                 IdCustomer = o.CustomerIdCustomer,
                                 Id = o.OrderId,
                                 OrderDate = o.OrderDate,
                                 Status = o.Status,
                                 ShippedDate = o.ShippedDate,
                                 ReceivedDate = o.ReceivedDate
                             };
                return result.ToList();
            }
        }

        public void Insert(OrderDto item)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var newItem = new Order
                {
                    CustomerIdCustomer = item.IdCustomer,
                    OrderId = item.Id,
                    OrderDate = item.OrderDate,
                    Status = item.Status,
                    ShippedDate = item.ShippedDate,
                    ReceivedDate = item.ReceivedDate,
                    Enable = true
                };

                ctx.DbContext.Orders.Add(newItem);
                ctx.DbContext.SaveChanges();

                item.Id = newItem.OrderId;
            }
        }

        public void Update(OrderDto item)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var data = (from o in ctx.DbContext.Orders
                            where (o.OrderId.Equals(item.Id)) && (o.Enable.Equals(true))
                            select o).FirstOrDefault();

                if (data == null)
                    throw new DataNotFoundException("Customer");
                data.OrderId = item.Id;
                data.OrderDate = item.OrderDate;
                data.Status = item.Status;
                data.ShippedDate = item.ShippedDate;
                data.ReceivedDate = item.ReceivedDate;

                var count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new Exception();
            }
        }

        public void Delete(Guid orderId)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var data = (from o in ctx.DbContext.Orders
                            where (o.OrderId.Equals(orderId)) && (o.Enable.Equals(true))
                            select o).FirstOrDefault();

                if (data == null)
                    throw new DataNotFoundException("Order");

                data.Enable = false;
                ctx.DbContext.SaveChanges();
            }
        }
    }
}
