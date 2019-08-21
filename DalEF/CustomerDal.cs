using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using Csla.Data;
using Dal;
using Csla.Data.EF6;

namespace DalEF
{
    public class CustomerDal:ICustomerDal
    {
        public CustomerDto Fetch(int idCustomer)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {

                //var result = (from c in ctx.DbContext.Customers
                //              where c.IdCustomer.Equals(idCustomer) && c.Enable.Equals(true)
                //              select c).FirstOrDefault();
                var result = ctx.DbContext.Customers.Where(c => (c.IdCustomer.Equals(idCustomer) && c.Enable.Equals(true)))
                    .Select(c => new CustomerDto
                    {
                        Address = c.Address,
                        Name = c.Name,
                        Id = c.IdCustomer,
                        Num1 = c.Num1,
                        Num2 = c.Num2,
                        Sum = c.Sum,
                        StartDate = c.StartDate,
                        EndDate = c.EndDate
                    }).FirstOrDefault();
                //if (result == null)
                //    return null;
                ////return result.ConvertAll(cu => EntityToDto(cu));
                //var res = EntityToDto(result);
                return result;
            }
        }

        private CustomerDto EntityToDto(Customer c)
        {
            return new CustomerDto
            {
                Address = c.Address,
                Name = c.Name,
                Id = c.IdCustomer,
                Num1 = c.Num1,
                Num2 = c.Num2,
                Sum = c.Sum,
                StartDate = c.StartDate,
                EndDate = c.EndDate
            };
        }

        public List<CustomerDto> Fetch()
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var result = from c in ctx.DbContext.Customers
                             where c.Enable.Equals(true)
                             orderby c.Name
                             select new CustomerDto
                             {
                                 Address = c.Address,
                                 Name = c.Name,
                                 Id = c.IdCustomer,
                                 Num1 = c.Num1,
                                 Num2 = c.Num2,
                                 Sum = c.Sum,
                                 StartDate = c.StartDate,
                                 EndDate = c.EndDate
                             };
                return result.ToList();
            }
        }

        public void Insert(CustomerDto item)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var newItem = new Customer
                {
                    Address = item.Address,
                    Name = item.Name,
                    Num1 = item.Num1,
                    Num2 = item.Num2,
                    Sum = item.Sum,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    Enable = true
                };

                ctx.DbContext.Customers.Add(newItem);
                ctx.DbContext.SaveChanges();

                item.Id = newItem.IdCustomer;
            }
        }

        public void Update(CustomerDto item)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var data = (from c in ctx.DbContext.Customers
                            where (c.IdCustomer.Equals(item.Id)) && (c.Enable.Equals(true))
                            select c).FirstOrDefault();

                if (data == null)
                    throw new DataNotFoundException("Customer");

                data.Address = item.Address;
                data.Name = item.Name;
                data.Num1 = item.Num1;
                data.Num2 = item.Num2;
                data.Sum = item.Sum;
                data.StartDate = item.StartDate;
                data.EndDate = item.EndDate;

                var count = ctx.DbContext.SaveChanges();
                if (count == 0)
                    throw new Exception();
            }
        }

        public void Delete(int idCustomer)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var data = (from c in ctx.DbContext.Customers
                            where (c.IdCustomer.Equals(idCustomer)) && (c.Enable.Equals(true))
                            select c).FirstOrDefault();

                if (data == null)
                    throw new DataNotFoundException("Customer");

                data.Enable = false;
                ctx.DbContext.SaveChanges();
            }
        }

        public bool ExistsName(string name)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var result = (from c in ctx.DbContext.Customers
                              where c.Name.Equals(name)
                              select c.IdCustomer).Any();
                return result;
            }
        }

        public bool ExistsAddress(string address)
        {
            using (var ctx = DbContextManager<ModelContainer>.GetManager())
            {
                var result = (from c in ctx.DbContext.Customers
                              where c.Address.Equals(address)
                              select c.IdCustomer).Any();
                return result;
            }
        }
    }
}
