using Csla;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    [Serializable]
    public class NameRequiredCommand : CommandBase<NameRequiredCommand>
    {
        #region Properties
        public static readonly PropertyInfo<bool> RequiredProperty = RegisterProperty<bool>(c => c.Required);
        public bool Required
        {
            get { return ReadProperty(RequiredProperty); }
            private set { LoadProperty(RequiredProperty, value); }
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        private string Name
        {
            get { return ReadProperty(NameProperty); }
            set { LoadProperty(NameProperty, value); }
        }
        #endregion

        #region Constructors
        public NameRequiredCommand()
        {
            Name = string.Empty;
            Required = false;
        }

        public NameRequiredCommand(string name) : this()
        {
            Name = name;
        }
        #endregion

        #region Factories
        public static async Task<NameRequiredCommand> ExecuteAsync(string name)
        {
            var cmd = new NameRequiredCommand(name);
            cmd = await DataPortal.ExecuteAsync(cmd);
            return cmd;
        }

        public static void Execute(string name, Action<bool> result)
        {
            var cmd = new NameRequiredCommand(name);
            DataPortal.BeginExecute(cmd, (o, e) =>
            {
                if (e.Error != null)
                    throw e.Error;
                else
                    result(e.Object.Required);
            });
        }
        #endregion Factories

        #region Data
        protected override void DataPortal_Execute()
        {
            Required = string.IsNullOrEmpty(Name);
        }
        #endregion
    }
}
