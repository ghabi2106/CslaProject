using Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace Web.Controllers
{
    //[HandleError]
    public class HomeController : Csla.Web.Mvc.Controller
    {
        public ActionResult Index()
        {
            //var customerList = CustomerList.GetCustomerList();
            //return View(customerList);
            var customerListViewModel = new CustomerListViewModel();
            return View(customerListViewModel);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        #region Details
        public ActionResult Details(int id)
        {
            var customerEditViewModel = new CustomerEditViewModel(id);
            return View(customerEditViewModel);
            
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            //int ID = id ?? 0;
            //var customerEdit = await CustomerInfo.GetCustomerAsync(ID);
            //return View(customerEdit);
        }
        #endregion

        // Create: Equipments
        #region Create
        public ActionResult Create()
        {
            var customerEditViewModel = new CustomerEditViewModel();
            return View(customerEditViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> Create(CustomerEditViewModel customerEditViewModel)
        {
            try
            {
                if (SaveObject<CustomerEdit>(customerEditViewModel.ModelObject, false))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return View(customerEditViewModel);
                }
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return RedirectToAction("Index");
        }
        #endregion

        //// Create: Equipments
        //#region Create
        //public async Task<ActionResult> Create()
        //{
        //    var customerCreate = new CustomerEdit();
        //    return View(customerCreate);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Create(FormCollection collection)
        //{
        //    var customerCreate = new CustomerEdit(){
        //        Name = collection.Get("Name"),
        //        Address = collection.Get("Address"),
        //        Num1 = Int32.Parse(collection["Num1"]),
        //        Num2 = Int32.Parse(collection["Num2"]),
        //        //Sum = Int32.Parse(collection["Sum"]),
        //        StartDate = collection.Get("StartDate"),
        //        EndDate = collection.Get("EndDate"),
        //        Enable = true
        //    };

        //    var customerEdit = CustomerEdit.NewCustomer();

        //    try
        //    {
        //        //UpdateModel(customerCreate, collection);
        //        //customerCreate.Name = collection.Get("Name");
        //        //customerCreate.Address = collection.Get("Address");
        //        //customerCreate.Num1 = Int32.Parse(collection["Num1"]);
        //        //customerCreate.Num2 = Int32.Parse(collection["Num2"]);
        //        //customerCreate.Sum = Int32.Parse(collection["Sum"]);
        //        //customerCreate.StartDate = collection.Get("StartDate");
        //        //customerCreate.EndDate = collection.Get("EndDate");
        //        //customerCreate.Enable = true;

        //        customerCreate = await customerCreate.SaveAsync();
        //        if (customerCreate.IsSavable)
        //        {
        //            return RedirectToAction("Edit", new { id = customerCreate.Id });
        //        }
        //        //if (ModelState.IsValid)
        //        //{
        //        //    //customerCreate.Name = collection.Get("Name");
        //        //    //customerCreate.Address = collection.Get("Address");

        //        //}
        //    }
        //    catch (DataException /* dex */)
        //    {
        //        //Log the error (uncomment dex variable name and add a line here to write a log.
        //        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
        //    }
        //    return View(customerCreate);
        //}
        //#endregion

        // Edit: Equipments
        #region Edit      
        //[ValidateAntiForgeryToken]
        public ActionResult Edit(int id)
        {
            var customerEditViewModel = new CustomerEditViewModel(id);
            return View(customerEditViewModel);
        }

        [HttpPost]
        public ActionResult Edit(int id, CustomerEditViewModel customerEditViewModel)
        {
            customerEditViewModel.CustomerId = id;

            if (SaveObject<CustomerEdit>(customerEditViewModel.ModelObject, true))
            {
                return RedirectToAction("Index");
            }
            else
            {
                return View(customerEditViewModel);
            }
        }
        #endregion Edit

        // Delete: Equipments
        #region Delete
        public async Task<ActionResult> Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete failed. Try again, and if the problem persists see your system administrator.";
            }
            var ID = id ?? 0;
            var customer = CustomerEdit.GetCustomer(id ?? 0);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var customerEditViewModel = new CustomerEditViewModel();
            customerEditViewModel.CustomerId = id; //not set by binder
            if (SaveObject<CustomerEdit>(customerEditViewModel.ModelObject, c => c.Delete(), true))
                return RedirectToAction("Index");
            else
            {
                return View();
            }
            //try
            //{
            //    var customer = await CustomerEdit.GetCustomerAsync(id);
            //    if (customer.Id != id)
            //    {
            //        return HttpNotFound();
            //    }
            //    await CustomerEdit.DeleteCustomerAsync(id);
            //}
            //catch (DataException/* dex */)
            //{
            //    //Log the error (uncomment dex variable name and add a line here to write a log.
            //    return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            //}
            //return RedirectToAction("Index");
        }
        #endregion Delete
    }
}