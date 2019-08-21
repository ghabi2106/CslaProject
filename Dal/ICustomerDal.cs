using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    public interface ICustomerDal
    {
        List<CustomerDto> Fetch();
        //List<CustomerDto> Fetch(string search, PagerDto pager, out int counterItem);
        //List<CustomerDto> Fetch(string companyNameFilter);
        //List<CustomerDto> Fetch(string companyNameFilter, int adminId);
        //List<CustomerDto> FetchByNameOrCodeForAdmin(int adminId, string valueSearched);
        //List<CustomerDto> FetchByNameOrCode(string valueSearched);
        //List<CustomerDto> FetchForAdmin(int adminId);
        CustomerDto Fetch(int idCustomer);
        //CustomerDto FetchOnByName(string Name);
        //bool Exists(int idCustomer);
        //bool Exists(int idCustomer, int adminId);
        bool ExistsName(string Name);
        bool ExistsNameId(string Name, int Id);
        bool ExistsAddress(string Address);
        void Insert(CustomerDto item);
        void Update(CustomerDto item);
        void Delete(int idCustomer);

        // For BDD Saving

        //void DeleteForRealAllWithOtherAdminId(int adminId);
    }
}
