using System;
using System.Collections.Generic;
using System.Linq;
using Csla;
using CslaProject.Library.Orders;
using Dal;
using Dal.Orders;

namespace CslaProject.Library.Orders
{
    [Serializable]
    public class OrderDetailsLists :
      BusinessListBase<OrderDetailsLists, OrderDetail>
    {
        #region Factory Methods

        internal static OrderDetailsLists NewOrderDetailsLists()
        {
            return DataPortal.Create<OrderDetailsLists>();
        }

        internal static OrderDetailsLists GetOrderDetailsLists(Guid OrderId)
        {
            return DataPortal.Fetch<OrderDetailsLists>(
              new SingleCriteria<OrderDetailsLists, Guid>(OrderId));
        }

      public OrderDetailsLists()
        { /* Require use of factory methods */ }

        #endregion

        #region Data Access

        private void DataPortal_Fetch(
          SingleCriteria<OrderDetailsLists, Guid> criteria)
        {
            RaiseListChangedEvents = false;
            //this.AddRange(Data.Connect().OrderDetails
            //.Where(o => o.OrderId == criteria.Value)
            //.Select(o => OrderDetail.GetOrderDetail(o)));
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<IOrderDetailDal>();
                List<OrderDetailDto> list = dal.Fetch(criteria.Value);

                foreach (var item in list)
                {
                    Add(DataPortal.FetchChild<OrderDetail>(item));
                }
            }
            RaiseListChangedEvents = true;
        }

        #endregion
    }
}
