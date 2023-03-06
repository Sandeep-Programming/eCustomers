using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using eCustomersWebApp.Models;
using Microsoft.Extensions.Configuration;

namespace eCustomersWebApp.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IConfiguration _configuration;
             
        HttpClient clientCustomers;
        HttpClient clientCustomerOpenHours;
        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;

            Uri baseAddressCustomers = new Uri(_configuration["CustomersBaseUrl"]);      
            clientCustomers = new HttpClient();
            clientCustomers.BaseAddress = baseAddressCustomers;

            Uri baseAddressOpenHours = new Uri(_configuration["OpenHoursBaseUrl"]);
            clientCustomerOpenHours = new HttpClient();
            clientCustomerOpenHours.BaseAddress = baseAddressOpenHours;
        }

        public IActionResult Index()
        {
            List<Customers> customers = new List<Customers>();
            HttpResponseMessage response = clientCustomers.GetAsync(clientCustomers.BaseAddress + "/Customers").Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customers = JsonConvert.DeserializeObject<List<Customers>>(data);
            }
            return View(customers);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Customers newCustomer)
        {
            string data = JsonConvert.SerializeObject(newCustomer);
            HttpContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clientCustomers.PostAsync(clientCustomers.BaseAddress + "/Customers", stringContent).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Edit(int id)
        {
            Customers customer = new Customers();
            HttpResponseMessage response = clientCustomers.GetAsync(clientCustomers.BaseAddress + "/Customers/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<Customers>(data);
            }
            return View("Edit", customer);
        }

        [HttpPost]
        public IActionResult Edit(Customers newCustomer)
        {
            string data = JsonConvert.SerializeObject(newCustomer);
            HttpContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clientCustomers.PutAsync(clientCustomers.BaseAddress + "/Customers", stringContent).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int id)
        {
            HttpResponseMessage response = clientCustomers.DeleteAsync(clientCustomers.BaseAddress + "/Customers/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Details(int id)
        {           
            TempData["CustomerId"] = id;
            return RedirectToAction("Index", "OpenHours");
        }
    }
}
