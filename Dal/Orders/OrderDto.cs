using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Orders
{
    public partial class OrderDto
    {
        #region Primitive Properties

        public virtual int IdCustomer { get; set; }

        public virtual Guid Id { get; set; }

        public virtual DateTime OrderDate { get; set; }

        public virtual int Status { get; set; }

        public virtual DateTime? ShippedDate { get; set; }

        public virtual DateTime? ReceivedDate { get; set; }

        public virtual bool Enable { get; set; }

        #endregion
    }
}
