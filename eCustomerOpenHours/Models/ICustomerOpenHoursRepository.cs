using System.Collections.Generic;

namespace eCustomersAPI.Models
{
    public interface ICustomerOpenHoursRepository
    {
        List<CustomerOpenHours> GetCustomerOpenHours(int customerId);

        CustomerOpenHours GetOneCustomerOpenHour(int customerId, int customerHourId);

        int InsertCustomerOpenHours(CustomerOpenHours customerHours);

        int UpdateCustomerOpenHours(CustomerOpenHours customerOpenHours);

        int DeleteCustomerOpenHours(int customerId, int customerHourId);

    }
}
