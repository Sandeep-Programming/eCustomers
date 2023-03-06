using eCustomersAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace eCustomersAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CustomerOpenHoursController : Controller
    {
        private readonly ICustomerOpenHoursRepository _customersOpenHoursRepository;

        public CustomerOpenHoursController(ICustomerOpenHoursRepository customersOpenHoursRepository)
        {
            _customersOpenHoursRepository = customersOpenHoursRepository;
        }

        [HttpGet]
        [Route("{customerId}")]
        public IEnumerable<CustomerOpenHours> GetAllOpenHoursForCustomer(int customerId)
        {
            return _customersOpenHoursRepository.GetCustomerOpenHours(customerId);
        }

        [HttpGet]        
        public CustomerOpenHours GetAllOpenHoursForCustomer(int customerId, int customerHoursId)
        {
            return _customersOpenHoursRepository.GetOneCustomerOpenHour(customerId, customerHoursId);
        }

        [HttpPost]
        public int AddNewOpenHoursForCustomer(CustomerOpenHours customerOpenHours) 
        {
            return _customersOpenHoursRepository.InsertCustomerOpenHours(customerOpenHours);
        }

        [HttpPut]
        public int UpdateOpenHours(CustomerOpenHours customerOpenHours) 
        { 
            return _customersOpenHoursRepository.UpdateCustomerOpenHours(customerOpenHours);
        }

        [HttpDelete]
        public int DeleteOpenHours(int customerId, int customerHoursId) 
        {
            return _customersOpenHoursRepository.DeleteCustomerOpenHours(customerId,customerHoursId); 
        }


    }
}
