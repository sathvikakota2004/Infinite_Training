using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Northwindapi.Models;
using System.Data.SqlClient;
using System.Web.Http;
namespace Northwindapi.Controllers
    
{
    public class CustordercountryController : ApiController
    {
        [RoutePrefix("api/orders")]
        public class OrdersController : ApiController
        {
            private readonly NorthwindEntities db = new NorthwindEntities();

            
            [HttpGet]
            [Route("employee/{id:int}")]
            public IHttpActionResult GetOrdersByEmployee(int id)
            {
                var orders = db.Orders
                               .Where(o => o.EmployeeID == id)
                               .Select(o => new
                               {
                                   o.OrderID,
                                   o.EmployeeID,
                                   o.OrderDate
                               })
                           
                               .ToList();

               

                return Ok(orders);
            }

            
            [HttpGet]
            [Route("customers/country/{country}")]
            public IHttpActionResult GetCustomersByCountry(string country)
            {
                
                var param = new SqlParameter("@Country", country);
                var customers = db.Database.SqlQuery<Customer>(
                                    "EXEC dbo.GetCustomersByCountry @Country", param
                                ).ToList();

                if (!customers.Any())
                    return NotFound();

                return Ok(customers);
            }

            
        }
    }
}