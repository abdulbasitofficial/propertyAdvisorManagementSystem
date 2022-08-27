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
    public class CompanyController : Controller
    {
        propertyEntities db = new propertyEntities();
        [HttpGet]
        public ActionResult Add_Company()
        {
            return View();

        }
        [HttpPost]
        public ActionResult Add_Company(Company p, HttpPostedFileBase PostedFile)
        {
            if (Session["ID"] != null)
            {
                Company pro = new Company();
                int id = int.Parse(Session["ID"].ToString());
                pro.Insertion(p, PostedFile, id);
                return RedirectToAction("Company");
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        [HttpGet]
        public ActionResult Company()
        {
            if (Session["ID"] != null)
            {
                Company pro = new Company();
                int id = int.Parse(Session["ID"].ToString());
                List<Company> list = pro.CompanyList(id);
                return View(list);
            }
            else
            {
                return RedirectToAction("Login", "Account");

            }
        }
        [HttpGet]
        public ActionResult CompanyUserSide()
        {
            Company pro = new Company();
            List<Company> list = pro.CompanyList();
            ViewBag.StartAverage = StarsAverageNuskha();
            return View(list);
        }
       
        public List<Company> StarsAverageNuskha()
        {
            List<Company> list = new List<Company>();

            var ratingCount = from f in db.ratings.GroupBy(f => f.property_id)
                              select new
                              {
                                  count = f.Sum(c => c.stars),
                                  f.FirstOrDefault().property_id,
                              };

            var userCount = db.ratings.GroupBy(l => l.property_id)
              .Select(g => new
              {
                  g.FirstOrDefault().property_id,
                  Count = g.Select(l => l.account_id).Distinct().Count()
              });
            foreach (var item in ratingCount)
            {
                foreach (var file in userCount)
                {
                    if (item.property_id == file.property_id)
                    {
                        decimal st = Math.Floor((decimal)item.count / file.Count);
                        list.Add(new Company
                        {
                            ID = (int)item.property_id,
                            Stars = st,
                            StarsNumber = (float)item.count,
                            TotalUsers = file.Count
                        });
                    }
                }
            }
            return list;
        }
    }
}