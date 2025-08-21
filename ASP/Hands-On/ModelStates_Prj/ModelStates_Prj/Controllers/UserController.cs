using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ModelStates_Prj.Models;

namespace ModelStates_Prj.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //1. If Validation Succeeds
        public ActionResult UserStatus()
        {
            ViewBag.Status = "Validation Successful";
            return View();
        }

        //2. Add user
        [HttpGet]
        public ActionResult AddUser()
        {
            User user = new User();
            return View(user);
        }

        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (string.IsNullOrEmpty(user.Lname))
            {
                ModelState.AddModelError("Lname", "Please Enter Last Name also..");
            }
            if (user.age < 21 || user.age > 45)
            {
                ModelState.AddModelError("age", "Age only between 21 and 45");
            }
            if (string.IsNullOrEmpty(user.Address))
            {
                ModelState.AddModelError("Address", "Address is required");
            }

           
            if (string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("Password", "Password is required");
            }
            
            else if (user.Password.Length < 6)
            {
                ModelState.AddModelError("Password", "Password must be at least 6 characters long");
            }
            if (!ModelState.IsValid)
            {
                return View(user);
            }

            else
            {
                TempData["lastname"] = user.Lname;
                TempData["age"] = user.age;
                TempData["address"] = user.Address;
                TempData.Keep();
                return RedirectToAction("UserStatus");

            }
        }
    }
}