// CustomersController.cs
using Microsoft.AspNetCore.Mvc;
using Callum_Tech_Test_TG.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Callum_Tech_Test_TG.Controllers
{
    public class CustomersController : Controller
    {
        private static List<Customer> _customers = new List<Customer>();

        public IActionResult Index()
        {
            return View(_customers);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddEditCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var existingCustomer = _customers.FirstOrDefault(c => c.Id == customer.Id);
                if (existingCustomer == null)
                {
                    // Adding a new customer
                    customer.Id = _customers.Any() ? _customers.Max(c => c.Id) + 1 : 1;
                    _customers.Add(customer);
                }
                else
                {
                    // Editing an existing customer
                    existingCustomer.Name = customer.Name;
                    existingCustomer.Age = customer.Age;
                    existingCustomer.PostCode = customer.PostCode;
                    existingCustomer.Height = customer.Height;
                }

                // For AJAX requests, return partial view to update the grid
                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                {
                    return PartialView("_CustomerGrid", _customers);
                }

                // For non-AJAX requests, redirect or return a full view
                return RedirectToAction(nameof(Index));
            }

            // Handle validation errors
            // For AJAX requests, you might return a validation summary or specific error messages
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return PartialView("_AddEditCustomerModal", customer);
            }

            // Return view with validation errors for non-AJAX requests
            return View(customer);
        }

        public IActionResult GetCustomerById(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return Json(customer);
        }
    }
}