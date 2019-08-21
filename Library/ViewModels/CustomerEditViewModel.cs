using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Csla.Web.Mvc;

namespace Library.ViewModels
{
    public class CustomerEditViewModel : ViewModelBase<CustomerEdit>, IViewModel
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
        public void CustomerViewModel()
        {
            ModelObject = CustomerEdit.NewCustomer();
        }

        //convenience
        public void CustomerViewModel(int id)
        {
            ModelObject = CustomerEdit.GetCustomer(id);
        }
    }
}
