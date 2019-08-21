using Csla.Core;
using Csla.Rules;
using Csla.Threading;
using Library.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Library.Rules
{
    public class IsDuplicateNameAsync : PropertyRule
    {
        private IPropertyInfo SecondaryProperty { get; set; }
        public IsDuplicateNameAsync(IPropertyInfo primaryProperty, IPropertyInfo secondaryProperty)
          : base(primaryProperty)
        {
            SecondaryProperty = secondaryProperty;

            InputProperties.Add(primaryProperty);
            InputProperties.Add(secondaryProperty);
            IsAsync = false;

            // setting all to false will only allow the rule to run when the property is set - typically by the user from the UI.
            CanRunAsAffectedProperty = false;
            CanRunInCheckRules = false;
            CanRunOnServer = false;
        }

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Execute(IRuleContext context)
        {
            var name = (string)context.InputPropertyValues[PrimaryProperty];
            var id = (int)context.InputPropertyValues[SecondaryProperty];

            // uses the async methods in DataPortal to perform data access on a background thread. 
            //NameCustomerCommand.BeginExecute(name, (o, e) =>
            //{
            //    if (e.Error != null)
            //        throw e.Error;
            //    else if(e.Object.IsExist)
            //        context.AddErrorResult("Name already exist");

            //    context.Complete();
            //});
            var IsExist =  DuplicateNameCommand.Execute(name, id);
            if (IsExist)
                context.AddErrorResult("Name already exist");
        }
    }
}
