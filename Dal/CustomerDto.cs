using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public partial class CustomerDto
    {
        #region Primitive Properties

        public virtual int Id { get; set; }

        public virtual string Name { get; set; }

        public virtual string Address { get; set; }

        public virtual int Num1 { get; set; }

        public virtual int Num2 { get; set; }

        public virtual int Sum { get; set; }

        public virtual DateTime StartDate { get; set; }

        public virtual DateTime EndDate { get; set; }

        public virtual bool Enable { get; set; }

        #endregion
    }
}
