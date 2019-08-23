using Csla.Web.Mvc;
using Library;
using Library.Customers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ViewModels.Customers
{
    public class CustomerEditViewModel : ViewModelBase<Customer>, IViewModel
    {
        //setter for internal id prop
        public int CustomerId
        {
            get { return ModelObject.Id; }
            set { ModelObject.Id = value; }
        }

        //value list (use private backing field to load on-demand, not every view needs list
        private List<string> states;
        public List<string> StateList
        {
            get
            {
                if (states == null)
                {
                    //can replace with CSLA ROL, NVL or other collection
                    states = new List<string>();
                    states.Add("IL");
                    states.Add("MN");
                    states.Add("WA");
                    states.Add("XX");
                    states.Add("YY");
                    states.Add("ZZ");
                }
                return states;
            }
        }

        //default
        public CustomerEditViewModel()
        {
            ModelObject = Customer.NewCustomer();
            //ModelObject.ValidationComplete += (o, e) =>
            // {
            //     var obj = (CustomerEdit)o;
            //     Console.WriteLine("Rules completed, Name=\"{0}\"", obj.Name);
            // };
        }

        //convenience
        public CustomerEditViewModel(int id)
        {
            ModelObject = Customer.GetCustomer(id);
        }
    }
}
