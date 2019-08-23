using System;
using System.Linq;
using Csla;
using Csla.Data;
using Csla.Security;
using Csla.Serialization;
using System.ComponentModel;
using System.Configuration;
using System.Threading.Tasks;
using Csla.Configuration;
using Dal;
using Csla.Rules;
using Csla.Core;
using System.Collections.Generic;
using Csla.Rules.CommonRules;
using Library.Rules;
using Library.Properties;
using Dal.Customers;

namespace Library.Customers
{
    [Serializable]
    public class Customer : BusinessBase<Customer>
    {
        // When a CustomerEdit is created

        #region Business Properties
        /// <summary>
        /// The Id property.
        /// </summary>
        public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
        /// <summary>
        /// Gets or sets Id.
        /// </summary>
        public int Id
        {
            get { return GetProperty(IdProperty); }
            set { SetProperty(IdProperty, value); }
        }

        /// <summary>
        /// The Enable property.
        /// </summary>
        public static readonly PropertyInfo<bool> EnableProperty = RegisterProperty<bool>(c => c.Enable);
        /// <summary>
        /// Gets or sets Enable.
        /// </summary>
        public bool Enable
        {
            get { return GetProperty(EnableProperty); }
            set { SetProperty(EnableProperty, value); }
        }

        /// <summary>
        /// The Address property.
        /// </summary>
        public static readonly PropertyInfo<string> AddressProperty = RegisterProperty<string>(c => c.Address);
        /// <summary>
        /// Gets or sets Address.
        /// </summary>
        public string Address
        {
            get { return GetProperty(AddressProperty); }
            set { SetProperty(AddressProperty, value); }
        }

        /// <summary>
        /// The Name property.
        /// </summary>
        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(c => c.Name);
        /// <summary>
        /// Gets or sets Name.
        /// </summary>
        public string Name
        {
            get { return GetProperty(NameProperty); }
            set { SetProperty(NameProperty, value); }
        }

        /// <summary>
        /// The Num1 property.
        /// </summary>
        public static readonly PropertyInfo<int> Num1Property = RegisterProperty<int>(c => c.Num1);
        /// <summary>
        /// Gets or sets Num1.
        /// </summary>
        public int Num1
        {
            get { return GetProperty(Num1Property); }
            set { SetProperty(Num1Property, value); }
        }

        /// <summary>
        /// The Num2 property.
        /// </summary>
        public static readonly PropertyInfo<int> Num2Property = RegisterProperty<int>(c => c.Num2);
        /// <summary>
        /// Gets or sets Num2.
        /// </summary>
        public int Num2
        {
            get { return GetProperty(Num2Property); }
            set { SetProperty(Num2Property, value); }
        }

        /// <summary>
        /// The start date property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> StartDateProperty 
            = RegisterProperty<SmartDate>(c => c.StartDate, null, new SmartDate()
            { Date = new Func<DateTime>(() => DateTime.Now).Invoke() });
        /// <summary>
        /// Gets or sets StartDate.
        /// </summary>
        public string StartDate
        {
            get { return GetPropertyConvert<SmartDate, string>(StartDateProperty); }
            set { SetPropertyConvert<SmartDate, string>(StartDateProperty, value); }
        }

        /// <summary>
        /// The end date property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> EndDateProperty 
            = RegisterProperty<SmartDate>(c => c.EndDate, null, new SmartDate()
            { Date = new Func<DateTime>(() => DateTime.Now.AddDays(1)).Invoke() });
        /// <summary>
        /// Gets or sets EndDate.
        /// </summary>
        public string EndDate
        {
            get { return GetPropertyConvert<SmartDate, string>(EndDateProperty); }
            set { SetPropertyConvert<SmartDate, string>(EndDateProperty, value); }
        }

        /// <summary>
        /// The Sum property.
        /// </summary>
        public static readonly PropertyInfo<int> SumProperty = RegisterProperty<int>(c => c.Sum);
        /// <summary>
        /// Gets or sets Sum.
        /// </summary>
        public int Sum
        {
            get { return GetProperty(SumProperty); }
            set { SetProperty(SumProperty, value); }
        }
        #endregion

        #region Business rules
        protected override void AddBusinessRules()
        {
            base.AddBusinessRules();
            // add rules to default rule set 

            //-------------------------------------------------------------------------------------------------------------------------
            //---------------------------- Address is required
            //-------------------------------------------------------------------------------------------------------------------------

            BusinessRules.AddRule(new Required(AddressProperty) { MessageText = "Customer must have name" });
            BusinessRules.AddRule(new MaxLength(AddressProperty, 50) { Priority = 1, MessageText = "{0} cannot be longer than {1} chars" });
            BusinessRules.AddRule(new MinLength(AddressProperty, 3) { Priority = 1, MessageText = "{0} cannot be shorter than {1} chars" });

            //-------------------------------------------------------------------------------------------------------------------------
            //---------------------------- Num2 Larger Than Num1
            //-------------------------------------------------------------------------------------------------------------------------

            //BusinessRules.AddRule<CustomerEdit>(Num1Property, o => o.Num1 > 3, "{0} must be larger than 3");
            //BusinessRules.AddRule<CustomerEdit>(Num2Property, o => o.Num2 > Num1, () => Resources.Num2LargerThanNum1, RuleSeverity.Warning);
            //BusinessRules.AddRule(new Dependency(Num1Property, Num2Property));

            //-------------------------------------------------------------------------------------------------------------------------
            //---------------------------- Compare To
            //-------------------------------------------------------------------------------------------------------------------------

            //BusinessRules.AddRule(new GreaterThanOrEqual(Num2Property, Num1Property));
            //BusinessRules.AddRule(new LessThan(StartDateProperty, EndDateProperty));
            //BusinessRules.AddRule(new GreaterThan(EndDateProperty, StartDateProperty));

            //-------------------------------------------------------------------------------------------------------------------------
            //---------------------------- Name is required
            //-------------------------------------------------------------------------------------------------------------------------

            BusinessRules.AddRule(new Required(NameProperty) { MessageDelegate = () => Resources.NameRequired });
            BusinessRules.AddRule(new MaxLength(NameProperty, 50) { Priority = 1, MessageDelegate = () => Resources.NameMaxLength });

            //-------------------------------------------------------------------------------------------------------------------------
            //---------------------------- Calculated Sum
            //-------------------------------------------------------------------------------------------------------------------------

            // set up dependencies to that Sum is automatically recaclulated when PrimaryProperty is changed 

            BusinessRules.AddRule(new Dependency(Num1Property, SumProperty));
            BusinessRules.AddRule(new Dependency(Num2Property, SumProperty));

            // add dependency for LessThanProperty rule on Num1
            BusinessRules.AddRule(new Dependency(Num2Property, Num1Property));

            BusinessRules.AddRule(new MaxValue<int>(Num1Property, 5000) { MessageText = "Num1 must be less than 5000" });
            BusinessRules.AddRule(new LessThan(Num1Property, Num2Property));

            //// calculates sum rule - must alwas run before MinValue with lower priority
            BusinessRules.AddRule(new CalcSum(SumProperty, Num1Property, Num2Property) { Priority = -1 });
            BusinessRules.AddRule(new MinValue<int>(SumProperty, 1));

            //-------------------------------------------------------------------------------------------------------------------------
            //---------------------------- Name Unique
            //-------------------------------------------------------------------------------------------------------------------------

            // ShortCircuit (ie do not run rules) for these properties when object has IsNew = false
            // DataAnnotation rules is always added with priority -1 so by giving ShortCircuiting rules a priority of -1 
            // you can also block DataAnootation rules from being executed. 
            // The same may also be done with StopINotCanWrite to prevent validation of fields that the user is not allowed to edit. 
            //BusinessRules.AddRule(new StopIfIsNotNew(NameProperty) { Priority = 1 });

            //BusinessRules.AddRule(new Required(NameProperty));
            //BusinessRules.AddRule(new MaxLength(NameProperty, 20));
            //BusinessRules.AddRule(new CustomerNameExistsRule(NameProperty));
            //BusinessRules.AddRule(new CustomerNameRequiredRule(NameProperty));

            BusinessRules.AddRule(new IsDuplicateNameAsync(NameProperty, IdProperty));

            BusinessRules.RuleSet = "CustA";
            BusinessRules.AddRule(new DoAsyncRule(NameProperty));

            // use default rule set
            BusinessRules.RuleSet = "default";

        }

        public class CustomerNameExistsRule : BusinessRule
        {
            public CustomerNameExistsRule(IPropertyInfo primaryProperty)
                : base(primaryProperty)
            {
                IsAsync = true;
                InputProperties = new List<IPropertyInfo>();
                InputProperties.Add(primaryProperty);
            }

            protected override async void Execute(IRuleContext context)
            {
                var name = (string)context.InputPropertyValues[NameProperty];

                NameExistsCommand.Execute(name, (result) =>
                {
                    if (result)
                    {
                        context.AddErrorResult(NameProperty, "Name already exists");
                    }
                });
                context.Complete();

                //----------------------------------------------------------------------------------
                //   Second Method
                //----------------------------------------------------------------------------------


                //var cmd = await NameExistsCommand.ExecuteAsync(name);
                //if (cmd.isExist == true)
                //{
                //    context.AddErrorResult("Name already exists");
                //}
                //context.Complete();
            }
        }

        public class CustomerNameRequiredRule : BusinessRule
        {
            public CustomerNameRequiredRule(IPropertyInfo primaryProperty)
                : base(primaryProperty)
            {
                IsAsync = true;
                InputProperties = new List<IPropertyInfo>();
                InputProperties.Add(primaryProperty);
            }

            protected override void Execute(IRuleContext context)
            {
                var name = (string)context.InputPropertyValues[NameProperty];
                if (string.IsNullOrEmpty(name))
                {
                    context.AddErrorResult(NameProperty, "Name required");
                }
                //NameRequiredCommand.Execute(name, (result) =>
                //{
                //    if (result)
                //    {
                //        context.AddErrorResult(NameProperty, "Name required");
                //    }
                //});
                context.Complete();
            }
        }
        #endregion

        #region Factory
        public static Customer NewCustomer()
        {
            return DataPortal.Create<Customer>();
        }

        public static async Task<Customer> NewCustomerAsync()
        {
            return await DataPortal.CreateAsync<Customer>();
        }

        public static Customer GetCustomer(int id)
        {
            return DataPortal.Fetch<Customer>(id);
        }

        public static async Task<Customer> GetCustomerAsync(int id)
        {
            return await DataPortal.FetchAsync<Customer>(id);
        }

        public static async Task<Customer> GetCustomerByNameAsync(string name)
        {
            return await DataPortal.FetchAsync<Customer>(name);
        }

        public static void DeleteCustomer(int id)
        {
            DataPortal.Delete<Customer>(id);
        }

        public static async Task DeleteCustomerAsync(int id)
        {
            await DataPortal.DeleteAsync<Customer>(id);
        }

        public static async Task<Customer> GetExistingCustomerAsync(int customerId)
        {
            return await DataPortal.FetchAsync<Customer>(new Criteria { Id = customerId });
        }

        public static async Task<bool> IsCustomerNameExistAsync(string name)
        {
            var result = await NameExistsCommand.ExecuteAsync(name);
            return result.isExist;
        }

        public static void NameExistsForBusinessRules(string name, Action<bool> result)
        {
            var cmd = new CustomerNameCommand(name);
            DataPortal.BeginExecute<CustomerNameCommand>(cmd, (o, e) =>
            {
                if (e.Error != null)
                    throw e.Error;
                else
                    result(e.Object.isExist);
            });
        }
        #endregion

        #region Base

        //protected override void DataPortal_Create()
        //{
        //    base.DataPortal_Create();
        //}

        private void DataPortal_Fetch(int id)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<ICustomerDal>();
                var data = dal.Fetch(id);
                if (data == null)
                    return;
                using (BypassPropertyChecks)
                {
                    DataMapper.Map(data, this);
                    //Address = data.Address;
                    //Name = data.Name;
                    //Id = data.Id;
                    //Num1 = data.Num1;
                    //Num2 = data.Num2;
                    //Sum = data.Sum;
                    //StartDate = data.StartDate.ToString("yyyy/MM/dd");
                    //EndDate = data.EndDate.ToString("yyyy/MM/dd");
                }
            }
        }

        private void Child_Fetch(CustomerDto item)
        {
            Address = item.Address;
            Name = item.Name;
            Id = item.Id;
            Num1 = item.Num1;
            Num2 = item.Num2;
            Sum = item.Sum;
            StartDate = item.StartDate.ToString("yyyy/MM/dd");
            EndDate = item.EndDate.ToString("yyyy/MM/dd");
        }

        protected override void DataPortal_Insert()
        {
            using (var ctx = DalFactory.GetManager())
            {
                //CheckRules();
                var dal = ctx.GetProvider<ICustomerDal>();
                using (BypassPropertyChecks)
                {
                    //var item = new CustomerDto
                    //{
                    //    Address = this.Address,
                    //    Name = this.Name,
                    //    Num1 = this.Num1,
                    //    Num2 = this.Num2,
                    //    Sum = this.Sum,
                    //    StartDate = Convert.ToDateTime(this.StartDate),
                    //    EndDate = Convert.ToDateTime(this.EndDate)
                    //};
                    var item = new CustomerDto();
                    DataMapper.Map(this, item);

                    dal.Insert(item);

                    Id = item.Id;
                }
                //FieldManager.UpdateChildren(this);
            }
        }

        protected override void DataPortal_Update()
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<ICustomerDal>();
                using (BypassPropertyChecks)
                {
                    //var item = new CustomerDto
                    //{
                    //    Address = this.Address,
                    //    Id = this.Id,
                    //    Name = this.Name,
                    //    Num1 = this.Num1,
                    //    Num2 = this.Num2,
                    //    Sum = this.Sum,
                    //    StartDate = Convert.ToDateTime(this.StartDate),
                    //    EndDate = Convert.ToDateTime(this.EndDate)
                    //};

                    var item = new CustomerDto();
                    DataMapper.Map(this, item);

                    dal.Update(item);
                }

                FieldManager.UpdateChildren(this);
            }
        }

        protected override void DataPortal_DeleteSelf()
        {
            using (BypassPropertyChecks)
            {
                DataPortal_Delete(this.Id);
            }
        }

        private void DataPortal_Delete(int id)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<ICustomerDal>();
                dal.Delete(id);
            }
        }
        #endregion

        #region Criteria

        [Serializable]
        public class Criteria : CriteriaBase<Criteria>
        {
            public static readonly PropertyInfo<int> IdProperty = RegisterProperty<int>(c => c.Id);
            public int Id
            {
                get { return ReadProperty(IdProperty); }
                set { LoadProperty(IdProperty, value); }
            }
        }

        #endregion

        #region ICheckRules Members

        /// <summary>
        /// Invokes all rules for the business type.
        /// </summary>
        public void CheckRules()
        {
            BusinessRules.CheckRules();
        }

        /// <summary>
        /// Invokes all business rules attached at the class level of a business type.
        /// </summary>
        public void CheckObjectRules()
        {
            BusinessRules.CheckObjectRules();
        }

        #endregion
    }
}
