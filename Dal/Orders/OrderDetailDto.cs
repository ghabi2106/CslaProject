using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Orders
{
    public partial class OrderDetailDto
    {
        #region Primitive Properties

        public virtual Guid OrderId { get; set; }

        public virtual Guid Id { get; set; }

        public virtual int LineNo { get; set; }

        public virtual string Item { get; set; }

        public virtual decimal Price { get; set; }

        public virtual int Qty { get; set; }

        public virtual decimal Discount { get; set; }

        #endregion
    }
}
