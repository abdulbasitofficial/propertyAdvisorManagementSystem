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
    public class PropertyController : Controller
    {
        propertyEntities db = new propertyEntities();
        // GET: Property
        [HttpGet]
        public ActionResult Add_Property()
        {
            ViewBag.Company = CompanyDropDown();
            return View();
            
        }
        [HttpPost]
        public ActionResult Add_Property(PropertyClass p, HttpPostedFileBase PostedFile)
        {
            if (Session["ID"] != null)
            {
                PropertyClass pro = new PropertyClass();
                int id = int.Parse(Session["ID"].ToString());
                pro.Insertion(p, PostedFile, id);
                return RedirectToAction("Property");
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
           
        }
        public List<SelectListItem> CompanyDropDown()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var query = db.companies.ToList();
            list.Add(new SelectListItem { Value = "0", Text = "Not a Company" });
            foreach (var item in query)
            {
                list.Add(new SelectListItem { Value=item.company_id.ToString(),Text=item.company_name });
            }
            return list;

        }
        [HttpGet]
        public ActionResult Property()
        {
            if (Session["ID"]!=null)
            {
                int id = int.Parse(Session["ID"].ToString());
                PropertyClass pro = new PropertyClass();
                List<PropertyClass> list = pro.Property(id);
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
           
        }
        [HttpGet]
        public ActionResult Single(int id)
        {
            Company com = new Company();
            PropertyClass pro = new PropertyClass();
            PropertyClass list = pro.PropertySingle(id);
            if (Session["ID"] !=null)
            {
                int stars;
                    int accid = (int)Session["id"];
                    var query = db.ratings.Join(db.properties,r=>r.property_id,p=>p.company_id,(r,p)=>new {r,p }).Where(m=> m.r.account_id == accid && m.p.property_id == id).FirstOrDefault();
                    if (query != null)
                    {
                        stars = (int)query.r.stars;

                    }
                    else
                    {
                        stars = 0;
                    }
                ViewBag.Rated = stars;
            }
          ViewBag.Company = com.CompanyList();

            return View(list);
        }
        [HttpPost]
        public ActionResult Refer(string id,string price)
        {
            if (Session["ID"] != null)
            {
                int uid = int.Parse(Session["ID"].ToString());
                int pid = int.Parse(id);
                int referprice = int.Parse(price);
                PropertyClass pro = new PropertyClass();
                pro.Refer(pid, uid,referprice);
                return RedirectToAction("Single", new { id = pid });
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
            
        }
        [HttpPost]
        public ActionResult MakeOffer(string id,string offer)
        {
            if (Session["ID"] != null)
            {
                int uid = int.Parse(Session["ID"].ToString());
                int pid = int.Parse(id);
                int PriceOffer = int.Parse(offer);
                PropertyClass pro = new PropertyClass();
                pro.Offer(pid, PriceOffer, uid);
                ViewBag.Message = "Successfull Submitted Offer";
                return RedirectToAction("Single", new { id = pid });
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
            
        }
        public ActionResult Offer()
        {
            if (Session["ID"] != null)
            {
                int uid = int.Parse(Session["ID"].ToString());
                PropertyClass pro = new PropertyClass();
                Company com = new Company();
                Account acc = new Account();

                ViewBag.Account = acc.AllAccount();
                ViewBag.Company = com.CompanyList();
                List<PropertyClass> list = pro.OfferRequest(uid);
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
            
        }
        public ActionResult Rating(int id,int star,int pid)
        {
            if (Session["ID"] != null)
            {
                int uid = int.Parse(Session["ID"].ToString());
                PropertyClass pro = new PropertyClass();
                pro.Rating(id, star, uid);
                return RedirectToAction("Single", new { id = pid });
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
           
        }
    }
}