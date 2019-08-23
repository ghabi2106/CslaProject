using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla;

namespace Library
{
    [Serializable]
    public class CustomerNameCommand : NameExistsCommand
    {
        #region Constructors
        public CustomerNameCommand() : base() { }

        public CustomerNameCommand(string name)
            : base(name)
        { }
        #endregion

        #region Data
        protected override void DataPortal_Execute()
        {
                NameExistsCommand cmd = new NameExistsCommand(Name);
                cmd = DataPortal.Execute<NameExistsCommand>(cmd);
                isExist = cmd.isExist;
        }
        #endregion
    }
}
