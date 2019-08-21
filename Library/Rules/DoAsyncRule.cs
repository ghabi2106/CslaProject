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
    public class DoAsyncRule : PropertyRule
    {
        public DoAsyncRule(IPropertyInfo primaryProperty)
          : base(primaryProperty)
        {
            InputProperties = new List<IPropertyInfo> { PrimaryProperty };
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

            // uses the async methods in DataPortal to perform data access on a background thread. 
            //NameCustomerCommand.BeginExecute(name, (o, e) =>
            //{
            //    if (e.Error != null)
            //        throw e.Error;
            //    else if(e.Object.IsExist)
            //        context.AddErrorResult("Name already exist");

            //    context.Complete();
            //});
            var IsExist = NameCustomerCommand.Execute(name);
            if (IsExist)
                    context.AddErrorResult("Name already exist");
        }

        //protected override void Execute(IRuleContext context)
        //{
        //    BackgroundWorker worker = new BackgroundWorker();
        //    worker.DoWork += (o, e) => 
        //    {
        //        var value = (string)context.InputPropertyValues[PrimaryProperty];
        //        context.AddOutValue(PrimaryProperty, value.ToUpper());
        //    };
        //    worker.RunWorkerCompleted += (o, e) =>
        //    {
        //        string val = (string)context.InputPropertyValues[PrimaryProperty];
        //        if (val == "Error")
        //        {
        //            context.AddErrorResult("Invalid data!");
        //        }
        //        else if (val == "Warning")
        //        {
        //            context.AddWarningResult("This might not be a great idea!");
        //        }
        //        else if (val == "Information")
        //        {
        //            context.AddInformationResult("Just an FYI!");
        //        }
        //        context.Complete();
        //    };
        //    worker.RunWorkerAsync();
        //}
    }
}
