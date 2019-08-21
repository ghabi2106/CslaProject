using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class NameExistsCommand : CommandBase<NameExistsCommand>
    {
        #region Properties
        public static readonly PropertyInfo<bool> isExistProperty = RegisterProperty<bool>(c => c.isExist);
        public bool isExist
        {
            get { return ReadProperty(isExistProperty); }
            protected set { LoadProperty(isExistProperty, value); }
        }
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        protected string Name
        {
            get { return ReadProperty(NameProperty); }
            set { LoadProperty(NameProperty, value); }
        }
        #endregion

        #region Constructors
        public NameExistsCommand()
        {
            Name = string.Empty;
        }

        public NameExistsCommand(string name)
        {
            Name = name;
        }

        public NameExistsCommand(Criteria criteria)
        {
            Name = criteria.Name;
        }
        #endregion

        #region Factories
        public async static Task<NameExistsCommand> ExecuteAsync(string name)
        {
            var cmd = new NameExistsCommand(new Criteria() { Name = name });
            cmd = await DataPortal.ExecuteAsync(cmd);
            return cmd;
        }

        public static void Execute(string name, Action<bool> result)
        {
            var cmd = new NameExistsCommand(new Criteria() { Name = name });
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
                isExist = dal.ExistsName(Name);
            }
        }
        #endregion

        #region Criteria
        [Serializable]
        public class Criteria : CriteriaBase<Criteria>
        {
            public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
            public string Name
            {
                get { return ReadProperty(NameProperty); }
                set { LoadProperty(NameProperty, value); }
            }
        }
        #endregion
    }
}
