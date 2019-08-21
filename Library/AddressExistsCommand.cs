using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{

    [Serializable]
    public class AddressExistsCommand : CommandBase<AddressExistsCommand>
    {
        #region Properties
        public static readonly PropertyInfo<bool> isExistProperty = RegisterProperty<bool>(c => c.isExist);
        public bool isExist
        {
            get { return ReadProperty(isExistProperty); }
            protected set { LoadProperty(isExistProperty, value); }
        }

        public static readonly PropertyInfo<string> AddressProperty = RegisterProperty<string>(c => c.Address);
        protected string Address
        {
            get { return ReadProperty(AddressProperty); }
            set { LoadProperty(AddressProperty, value); }
        }
        #endregion

        #region Constructors
        public AddressExistsCommand()
        {
            Address = string.Empty;
        }

        public AddressExistsCommand(string name, string address)
        {
            Address = address;
        }

        public AddressExistsCommand(Criteria criteria)
        {
            Address = criteria.Address;
        }
        #endregion

        #region Factories
        public async static Task<AddressExistsCommand> ExecuteAsync(string address)
        {
            var cmd = new AddressExistsCommand(new Criteria() { Address = address });
            cmd = await DataPortal.ExecuteAsync(cmd);
            return cmd;
        }

        public static void Execute(string address, Action<bool> result)
        {
            var cmd = new AddressExistsCommand(new Criteria() { Address = address });
            DataPortal.BeginExecute(cmd, (o, e) =>
            {
                if (e.Error != null)
                    throw e.Error;
                else
                    result(e.Object.isExist);
            });
        }
        #endregion Factories

        #region Data
        protected override void DataPortal_Execute()
        {
            using (var ctx = Dal.DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<Dal.ICustomerDal>();
                isExist = dal.ExistsAddress(Address);
            }
        }
        #endregion

        #region Criteria
        [Serializable]
        public class Criteria : CriteriaBase<Criteria>
        {
            public static readonly PropertyInfo<string> AddressProperty = RegisterProperty<string>(c => c.Address);
            public string Address
            {
                get { return ReadProperty(AddressProperty); }
                set { LoadProperty(AddressProperty, value); }
            }
        }
        #endregion
    }
}
