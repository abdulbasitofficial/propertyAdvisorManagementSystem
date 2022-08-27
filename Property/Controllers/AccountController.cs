using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;
using Property.Models;
namespace Property.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class AccountController : Controller
    {
        propertyEntities db = new propertyEntities();
        // GET: Account
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Account acc)
        {
            Account a = new Account();
            Account obj = a.Login(acc);
            if (obj!=null)
            {
                Session["ID"] = obj.ID;
                Session["Type"] = obj.Type;
                Session["Email"] = obj.Email;
                Session["Name"] = obj.Name;
                return RedirectToAction("Index","Home");
            }
            else
            {
                ViewBag.Result = "Invalid Rrecord";
                return View();
            }
            
        }
        [HttpGet]
        public ActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignUp(Account acc ,string location, string type)
        {
            Account a = new Account();
            a.Insertion(acc,location,type);
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult LogOut()
        {
            Session.RemoveAll(); //Clear all session variables
            return RedirectToAction("Index", "Home");
        }
    }
}