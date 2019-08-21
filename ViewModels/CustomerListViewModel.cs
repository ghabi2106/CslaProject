using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels
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
