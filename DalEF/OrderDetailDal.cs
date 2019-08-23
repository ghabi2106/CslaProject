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
    public class OrderDetailDal:IOrderDetailDal
    {
        public OrderDetailDto Fetch(Guid orderId, Guid orderDetailId)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var result = ctx.DbContext.OrderDetails.Where(o => o.OrderDetailId.Equals(orderDetailId)
                && o.OrderOrderId.Equals(orderId))
                    .Select(o => new OrderDetailDto
                    {
                        OrderId = o.OrderOrderId,
                        Id = o.OrderDetailId,
                        LineNo = o.LineNo,
                        Item = o.Item,
                        Price = o.Price,
                        Qty = o.Qty,
                        Discount = o.Discount
                    }).FirstOrDefault();
                return result;
            }
        }

        private OrderDetailDto EntityToDto(OrderDetail o)
        {
            return new OrderDetailDto
            {
                OrderId = o.OrderOrderId,
                Id = o.OrderDetailId,
                LineNo = o.LineNo,
                Item = o.Item,
                Price = o.Price,
                Qty = o.Qty,
                Discount = o.Discount
            };
        }

        public List<OrderDetailDto> Fetch(Guid orderId)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var result = from o in ctx.DbContext.OrderDetails
                             where o.OrderOrderId.Equals(orderId)
                             orderby o.LineNo
                             select new OrderDetailDto
                             {
                                 OrderId = o.OrderOrderId,
                                 Id = o.OrderDetailId,
                                 LineNo = o.LineNo,
                                 Item = o.Item,
                                 Price = o.Price,
                                 Qty = o.Qty,
                                 Discount = o.Discount
                             };
                return result.ToList();
            }
        }

        public void Insert(OrderDetailDto item)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var newItem = new OrderDetail
                {
                    OrderOrderId = item.OrderId,
                    OrderDetailId = item.Id,
                    LineNo = item.LineNo,
                    Item = item.Item,
                    Price = item.Price,
                    Qty = item.Qty,
                    Discount = item.Discount
                };

                ctx.DbContext.OrderDetails.Add(newItem);
                ctx.DbContext.SaveChanges();

                item.Id = newItem.OrderDetailId;
            }
        }

        public void Update(OrderDetailDto item)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var data = (from o in ctx.DbContext.OrderDetails
                            where (o.OrderDetailId.Equals(item.Id))
                            select o).FirstOrDefault();

                if (data == null)
                    throw new DataNotFoundException("OrderDetail");
                data.OrderOrderId = item.OrderId;
                data.OrderDetailId = item.Id;
                data.LineNo = item.LineNo;
                data.Item = item.Item;
                data.Price = item.Price;
                data.Qty = item.Qty;
                data.Discount = item.Discount;

                var count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new Exception();
            }
        }

        public void Delete(Guid orderDetailId)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var data = (from o in ctx.DbContext.OrderDetails
                            where (o.OrderDetailId.Equals(orderDetailId)) 
                            select o).FirstOrDefault();

                if (data == null)
                    throw new DataNotFoundException("OrderDetail");
                ctx.DbContext.SaveChanges();
            }
        }
    }
}
