﻿using Csla.Core;
using Csla.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Rules
{
    /// <summary>
    /// simple rule that modifies a string property to all upper case
    /// </summary>
    public class UpperCaseRule : BusinessRule
    {
        public UpperCaseRule(IPropertyInfo primaryProperty)
            : base(primaryProperty)
        {
            InputProperties = new List<IPropertyInfo> { primaryProperty };
        }

        protected override void Execute(IRuleContext context)
        {
            //modify property value, to upper
            var val1 = (string)context.InputPropertyValues[PrimaryProperty];
            if (!string.IsNullOrEmpty(val1))
                context.AddOutValue(PrimaryProperty, val1.ToUpper());
        }
    }
}
