using System.Collections.Generic;

namespace eCustomersAPI.Models
{
    public interface ICustomersRepository
    {
        List<Customers> GetCustomers();
        Customers GetCustomer(int customersId);
        int InsertCustomer(Customers Customer);
        int UpdateCustomer(Customers Customer);
        int DeleteCustomer(int customersId);
    }
}
