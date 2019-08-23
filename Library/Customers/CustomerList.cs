using System;
using System.Linq;
using Csla;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal;
using Dal.Customers;

namespace Library.Customers
{
    [Serializable()]
    public class CustomerList : ReadOnlyListBase<CustomerList, Customer>
    {
        #region Methods

        #endregion

        #region Factory

        public static void GetCustomerList(EventHandler<DataPortalResult<CustomerList>> callback)
        {
            DataPortal.BeginFetch<CustomerList>(callback);
        }

        public static CustomerList GetCustomerList()
        {
            return DataPortal.Fetch<CustomerList>();
        }
        

        //public static async Task<CustomerList> GetListAsync(List<CustomerDto> list)
        //{
        //    var result = DataPortal.Create<CustomerList>();
        //    await result.Fetch(list);
        //    return result;
        //}

        #endregion

        #region Data
        private void DataPortal_Fetch()
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            using (var ctx = DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<ICustomerDal>();
                List<CustomerDto> list = dal.Fetch();

                foreach (var item in list)
                {
                    Add(DataPortal.FetchChild<Customer>(item));
                }
            }
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }
        private async Task Fetch(List<CustomerDto> list)
        {
            var rlce = RaiseListChangedEvents;
            RaiseListChangedEvents = false;
            IsReadOnly = false;
            foreach (var item in list)
                Add(DataPortal.FetchChild<Customer>(item));
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
