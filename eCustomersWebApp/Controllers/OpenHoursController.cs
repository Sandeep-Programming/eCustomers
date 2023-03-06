using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System;
using eCustomersWebApp.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace eCustomersWebApp.Controllers
{
    public class OpenHoursController : Controller
    {
        private readonly IConfiguration _configuration;

        HttpClient clientCustomers;
        HttpClient clientCustomerOpenHours;
        public OpenHoursController(IConfiguration configuration)
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
            int id = Convert.ToInt32(TempData["CustomerId"]);

            Customers customer = new Customers();
            List<CustomerOpenHours> customerOpenHours = new List<CustomerOpenHours>();

            HttpResponseMessage response = clientCustomers.GetAsync(clientCustomers.BaseAddress + "/Customers/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                customer = JsonConvert.DeserializeObject<Customers>(data);

                ViewBag.CustomerData = customer;
            }

            HttpResponseMessage response1 = clientCustomerOpenHours.GetAsync(clientCustomerOpenHours.BaseAddress + "/CustomerOpenHours/" + id).Result;
            if (response.IsSuccessStatusCode)
            {
                string data = response1.Content.ReadAsStringAsync().Result;
                customerOpenHours = JsonConvert.DeserializeObject<List<CustomerOpenHours>>(data);
            }

            return View(customerOpenHours);
        }

        public IActionResult Create(int id)
        {
            ViewBag.CustomerId = Convert.ToInt32(id);
            return View();
        }

        [HttpPost]
        public IActionResult Create(CustomerOpenHours newCustomerOpenHours)
        {
            string data = JsonConvert.SerializeObject(newCustomerOpenHours);
            HttpContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clientCustomers.PostAsync(clientCustomerOpenHours.BaseAddress + "/CustomerOpenHours/", stringContent).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["CustomerId"] = newCustomerOpenHours.CustomerId;
                return RedirectToAction("Index", "OpenHours");
            }
            return View();
        }

        public IActionResult Edit(int id, int id2)
        {
            CustomerOpenHours customerOpenHours = new CustomerOpenHours();

            HttpResponseMessage response = clientCustomers.GetAsync(clientCustomerOpenHours.BaseAddress + "/CustomerOpenHours/?customerId=" + id +
                "&customerHoursId=" + id2).Result;
            if (response.IsSuccessStatusCode)
            {
                string ResponseData = response.Content.ReadAsStringAsync().Result;
                customerOpenHours = JsonConvert.DeserializeObject<CustomerOpenHours>(ResponseData);
            }
            return View(customerOpenHours);
        }

        [HttpPost]
        public IActionResult Edit(CustomerOpenHours customerOpenHours)
        {
            string data = JsonConvert.SerializeObject(customerOpenHours);
            HttpContent stringContent = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = clientCustomers.PutAsync(clientCustomerOpenHours.BaseAddress + "/CustomerOpenHours", stringContent).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["CustomerId"] = customerOpenHours.CustomerId;
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult Delete(int id, int id2)
        {
            HttpResponseMessage response = clientCustomers.DeleteAsync(clientCustomerOpenHours.BaseAddress + "/CustomerOpenHours/?customerId=" + id +
                "&customerHoursId=" + id2).Result;
            if (response.IsSuccessStatusCode)
            {
                TempData["CustomerId"] = id;
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
