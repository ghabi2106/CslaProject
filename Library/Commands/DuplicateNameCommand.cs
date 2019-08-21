// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DuplicateNameCommand.cs" company="ITGWANA">
//   Copyright (c) ITGWANA . All rights reserved. Website: http://www.itgwana.com
// </copyright>
//  <summary>
//   The Exist Name customer command.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


using System;

using Csla;

namespace Library.Commands
{
    /// <summary>
    /// The Exist Name customer command.
    /// </summary>
    [Serializable]
    public class DuplicateNameCommand : CommandBase<DuplicateNameCommand>
    {
        /// <summary>
        /// The customer IsExist property.
        /// </summary>
        public static readonly PropertyInfo<bool> IsExistProperty = RegisterProperty<bool>(c => c.IsExist);

        /// <summary>
        /// Gets or sets IsExist.
        /// </summary>
        public bool IsExist
        {
            get { return ReadProperty(IsExistProperty); }
            set { LoadProperty(IsExistProperty, value); }
        }

        /// <summary>
        /// The name property.
        /// </summary>
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name
        {
            get { return ReadProperty(NameProperty); }
            set { LoadProperty(NameProperty, value); }
        }

        /// <summary>
        /// The name property.
        /// </summary>
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);

        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public int Id
        {
            get { return ReadProperty(IdProperty); }
            set { LoadProperty(IdProperty, value); }
        }

        #region Factory Methods

        /// <summary>
        /// Sync Exist of customer name
        /// </summary>
        /// <param name="Name">
        /// The customer Name.
        /// </param>
        /// <returns>
        /// </returns>
        public static bool Execute(string name, int id)
        {
            var cmd = new DuplicateNameCommand();
            cmd.Name = name;
            cmd.Id = id;
            cmd = DataPortal.Execute<DuplicateNameCommand>(cmd);
            return cmd.IsExist;
        }

        /// <summary>
        /// Async Exist of customer name
        /// </summary>
        /// <param name="customerName">
        /// The customer Name.
        /// </param>
        /// <param name="callback">
        /// The callback function to execute when async call is completed.
        /// </param>
        public static void BeginExecute(string name, int id, EventHandler<DataPortalResult<DuplicateNameCommand>> callback)
        {
            var cmd = new DuplicateNameCommand();
            cmd.Name = name;
            cmd.Id = id;
            DataPortal.BeginExecute<DuplicateNameCommand>(cmd, callback);
        }

        #endregion

        #region Server-side Code

        /// <summary>
        /// The data portal_ execute.
        /// </summary>
        protected override void DataPortal_Execute()
        {
            using (var ctx = Dal.DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<Dal.ICustomerDal>();
                IsExist = dal.ExistsNameId(Name, Id);
            }
        }

        #endregion
    }
}

