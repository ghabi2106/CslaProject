﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GreaterThanOrEqual.cs" company="Marimer LLC">
//   Copyright (c) Marimer LLC. All rights reserved. Website: http://www.lhotka.net/cslanet
// </copyright>
//  <summary>
//   Validates that primary property is freater than or equal compareToProperty
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System;
using System.Collections.Generic;

using Library.Properties;

using Csla.Core;
using Csla.Rules;
using Csla.Rules.CommonRules;

namespace Library.Rules
{

    /// <summary>
    /// Validates that primary property is freater than or equal compareToProperty
    /// </summary>
    public class GreaterThanOrEqual : CommonBusinessRule
    {
        /// <summary>
        /// Gets or sets CompareTo.
        /// </summary>
        private IPropertyInfo CompareTo { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanOrEqual"/> class.
        /// </summary>
        /// <param name="primaryProperty">
        /// The primary property.
        /// </param>
        /// <param name="compareToProperty">
        /// The compare to property.
        /// </param>
        public GreaterThanOrEqual(IPropertyInfo primaryProperty, IPropertyInfo compareToProperty)
          : base(primaryProperty)
        {
            CompareTo = compareToProperty;
            InputProperties = new List<IPropertyInfo>() { primaryProperty, compareToProperty };
            this.RuleUri.AddQueryParameter("compareto", compareToProperty.Name);
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        /// <returns>
        /// The get message.
        /// </returns>
        protected override string GetMessage()
        {
            return HasMessageDelegate ? base.GetMessage() : Resources.GreaterThanOrEqualRule;
        }

        /// <summary>
        /// Does the check for primary propert less than compareTo property
        /// </summary>
        /// <param name="context">
        /// Rule context object.
        /// </param>
        protected override void Execute(IRuleContext context)
        {
            var value1 = (IComparable)context.InputPropertyValues[PrimaryProperty];
            var value2 = (IComparable)context.InputPropertyValues[CompareTo];

            if (value1.CompareTo(value2) < 0)
            {
                context.Results.Add(new RuleResult(this.RuleName, PrimaryProperty, string.Format(GetMessage(), PrimaryProperty.FriendlyName, CompareTo.FriendlyName)) { Severity = this.Severity });
            }
        }
    }
}
