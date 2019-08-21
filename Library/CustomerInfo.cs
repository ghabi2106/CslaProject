using System;
using Csla;
using Csla.Serialization;
using System.Configuration;
using System.Threading.Tasks;
#if !SILVERLIGHT
using Dal;
#endif

namespace Library
{
    [Serializable]
    public class CustomerInfo : ReadOnlyBase<CustomerInfo>
    {
        #region Properties

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
            set { LoadProperty(IdProperty, value); }
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
            set { LoadProperty(AddressProperty, value); }
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
            set { LoadProperty(NameProperty, value); }
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
            set { LoadProperty(Num1Property, value); }
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
            set { LoadProperty(Num2Property, value); }
        }

        /// <summary>
        /// The start date property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> StartDateProperty = RegisterProperty<SmartDate>(c => c.StartDate, null, new SmartDate() { Date = new Func<DateTime>(() => DateTime.Now).Invoke() });
        /// <summary>
        /// Gets or sets StartDate.
        /// </summary>
        public string StartDate
        {
            get { return GetPropertyConvert<SmartDate, string>(StartDateProperty); }
            set { LoadPropertyConvert<SmartDate, string>(StartDateProperty, value); }
        }

        /// <summary>
        /// The end date property.
        /// </summary>
        public static readonly PropertyInfo<SmartDate> EndDateProperty = RegisterProperty<SmartDate>(c => c.EndDate, null, new SmartDate());
        /// <summary>
        /// Gets or sets EndDate.
        /// </summary>
        public string EndDate
        {
            get { return GetPropertyConvert<SmartDate, string>(EndDateProperty); }
            set { LoadPropertyConvert<SmartDate, string>(EndDateProperty, value); }
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
            set { LoadProperty(SumProperty, value); }
        }

        #endregion

        #region Constructor

        public CustomerInfo(CustomerEdit item)
        {
            Address = item.Address;
            Name = item.Name;
            Id = item.Id;
            Num1 = item.Num1;
            Num2 = item.Num2;
            Sum = item.Sum;
            StartDate = item.StartDate;
            EndDate = item.EndDate;
        }

        public CustomerInfo()
        {
        }

        public CustomerInfo(int id)
        {
            Id = id;
        }

        #endregion

        #region Factory

        public static CustomerInfo GetCustomer(int idCustomer)
        {
            var t = DataPortal.Fetch<CustomerInfo>(idCustomer);
            return DataPortal.Fetch<CustomerInfo>(idCustomer);
        }

        public static async Task<CustomerInfo> GetCustomerAsync(int idCustomer)
        {
            //var t = DataPortal.FetchAsync<CustomerInfo>(idCustomer);
            return await DataPortal.FetchAsync<CustomerInfo>(idCustomer);
        }

        #endregion

        #region Data
        private void DataPortal_Fetch(int idCustomer)
        {
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<ICustomerDal>();
                CustomerDto customer = dal.Fetch(idCustomer);

                if (customer != null)
                {
                    Address = customer.Address;
                    Name = customer.Name;
                    Id = customer.Id;
                    Num1 = customer.Num1;
                    Num2 = customer.Num2;
                    Sum = customer.Sum;
                    StartDate = customer.StartDate.ToString("yyyy/MM/dd");
                    EndDate = customer.EndDate.ToString("yyyy/MM/dd");
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
        #endregion
    }
}
