using System;
using System.Linq;
using Csla;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dal;

namespace Library
{
    [Serializable()]
    public class CustomerList : ReadOnlyListBase<CustomerList, CustomerInfo>
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
            using (var ctx = Dal.DalFactory.GetManager())
            {
                var dal = ctx.GetProvider<Dal.ICustomerDal>();
                List<Dal.CustomerDto> list = dal.Fetch();

                foreach (var item in list)
                {
                    Add(DataPortal.FetchChild<CustomerInfo>(item));
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
                Add(DataPortal.FetchChild<CustomerInfo>(item));
            IsReadOnly = true;
            RaiseListChangedEvents = rlce;
        }

        #endregion
    }
}
