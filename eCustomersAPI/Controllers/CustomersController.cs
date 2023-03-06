using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using eCustomersAPI.Models;

namespace eCustomersAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private readonly ICustomersRepository _customersRepository;

        public CustomersController(ICustomersRepository customersRepository)
        { 
            _customersRepository = customersRepository;
        }

        [HttpGet]
        public IEnumerable<Customers> GetAllCustomers()
        {
          return _customersRepository.GetCustomers();
        }

        [HttpGet]
        [Route("{CustomerId}")]
        public Customers GetSingleCustomer(int CustomerId)
        {
            return _customersRepository.GetCustomer(CustomerId);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public int CreateNewCustomer(Customers Customer)
        {
            return _customersRepository.InsertCustomer(Customer);   
        }

        [HttpPut]
        public int UpdateCutomer(Customers Customer)
        {
            return _customersRepository.UpdateCustomer(Customer);
        }

        [HttpDelete]
        [Route("{CustomerId}")]
        public int DeleteCustomer(int CustomerId)
        {
            return _customersRepository.DeleteCustomer(CustomerId);
        }

    }
}
