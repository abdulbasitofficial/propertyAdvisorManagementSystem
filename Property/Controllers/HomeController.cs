using Property.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Property.Controllers
{
    [SessionState(SessionStateBehavior.Default)]
    public class HomeController : Controller
    {
        propertyEntities db = new propertyEntities();
        public ActionResult Index()
        {
                PropertyClass pro = new PropertyClass();
                List<PropertyClass> list = pro.Property();
                Company com = new Company();
                ViewBag.Company = com.CompanyList();
                return View(list);        }
        public ActionResult Search(string type,string location,string from,string to)
        {
            if (Session["ID"] != null)
            {
                PropertyClass pro = new PropertyClass();
                Company com = new Company();
                List<PropertyClass> list = pro.Search(type, location, from, to);
                ViewBag.Company = com.CompanyList();
                return View("Index", list);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        [HttpGet]
        public ActionResult PropertyCompany(int id)
        {
            if (Session["ID"] != null)
            {
                PropertyClass pro = new PropertyClass();
                Company com = new Company();
                List<PropertyClass> list = pro.PropertyCompany(id);
                ViewBag.Company = com.CompanyList();
                return View("Index", list);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        public ActionResult Advisor()
        {
            if (Session["ID"] != null)
            {
                Account acc = new Account();
                List<Account> list = acc.Advisor();
                int userid = int.Parse(Session["ID"].ToString());
                Refer rf = new Refer();
                ViewBag.MyAdvisor = rf.ReferCheck(userid);

                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult Add_Advisor(int id)
        {
            if (Session["ID"] != null)
            {
                int userid = int.Parse(Session["ID"].ToString());
                Refer rf = new Refer();
                rf.Add_Advisor(id, userid);
                return RedirectToAction("Advisor");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            
        }
        public ActionResult Remove_Advisor(int id)
        {
            if (Session["ID"] != null)
            {
                int userid = int.Parse(Session["ID"].ToString());
                Refer rf = new Refer();
                rf.Remove_Advisor(id, userid);
                return RedirectToAction("Advisor");
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
        public ActionResult Notification()
        {
            if (Session["ID"] != null)
            {
                int userid = int.Parse(Session["ID"].ToString());
                PropertyClass pro = new PropertyClass();
                List<PropertyClass> list = pro.Notification(userid);

                Account acc = new Account();
                ViewBag.Advisor = acc.Advisor();

                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            } 
        }
        public ActionResult Refer()
        {
            if (Session["ID"] != null)
            {
                int userid = int.Parse(Session["ID"].ToString());
                Account pro = new Account();
                List<Account> list = pro.Refer(userid);
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }
    }
}