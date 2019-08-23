using Library;
using Library.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Customers
{
    public class CustomerListViewModel
    {
        //business object
        public CustomerList Customers;

        //default
        public CustomerListViewModel()
        {
            Customers = CustomerList.GetCustomerList();
        }
    }
}
