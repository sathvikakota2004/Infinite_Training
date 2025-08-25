using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CC9northwinddbfirst.Models;

namespace CC9northwinddbfirst.Controllers
{
    public class CodeController : Controller
    {
        private NorthwindEntities db = new NorthwindEntities();
        // GET: Code
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GermanyCustomers()
        {
            var customersInGermany = db.Customers
                .Where(c => c.Country == "Germany")
                .OrderBy(c => c.CompanyName)
                .ToList();

            return View(customersInGermany);
        }
        public ActionResult CustomerByOrder(int orderId = 10248)
        {
            
            var customer = db.Orders
                .Where(o => o.OrderID == orderId)
                .Select(o => o.Customer)
                .FirstOrDefault();

            if (customer == null)
            {
                return HttpNotFound($"No customer found for OrderID {orderId}");
            }

            return View(customer);
        }

        
    }
}