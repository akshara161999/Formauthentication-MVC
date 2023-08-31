using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FormAuthentication_MVC_.Models;
using System.Web.Security;
   


namespace FormAuthentication_MVC_.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Membership model)
        {
            using (var context = new OfficeEntities())
            {
               
                bool isValid = context.Users.Any(x => x.UserName == model.UserName && x.Password == model.Password);

                if(isValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, false);
                    return RedirectToAction("Index", "Employees");
                }
                ModelState.AddModelError("", "Invalid username and password");
                return View();
            }
               
        }

       

        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(User model)
        {
            using (var context = new OfficeEntities())
            {
                context.Users.Add(model);
                context.SaveChanges();
            }
                return RedirectToAction("Login");
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

    }
}