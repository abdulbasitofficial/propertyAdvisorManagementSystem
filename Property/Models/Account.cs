using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Property.Models
{
    [SessionState(SessionStateBehavior.Default)]
    public class Account
    {
        propertyEntities db = new propertyEntities();
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }

        public void Insertion( Account a, string location,string type)
        {
            aacount acc = new aacount();
            acc.account_name = a.Name;
            acc.account_email = a.Email;
            acc.account_password = a.Password;
            acc.account_type = type;
            acc.account_contact = a.Contact;
            acc.account_location = location;
            db.aacounts.Add(acc);
            db.SaveChanges();
        }
        public Account Login(Account a)
        {
            Account obj = new Account();
            var query = db.aacounts.Where(e => e.account_email.Equals(a.Email) && e.account_password.Equals(a.Password)).FirstOrDefault();
            if (query !=null)
            {
                obj.Name = query.account_name;
                obj.ID = query.account_id;
                obj.Email = query.account_email;
                obj.Type = query.account_type;
            }
           
            return obj;
        }
        public List<Account> Advisor()
        {
            List<Account> list = new List<Account>();

            var query = db.aacounts.Where(e => e.account_type.Equals("Advisor")).ToList();
            foreach (var item in query)
            {
                list.Add(new Account {
                    ID=item.account_id,
                    Name=item.account_name,
                    Email=item.account_email,
                    Contact=item.account_contact,
                    Location=item.account_location
                });

            }
            return list;

        }
        public List<Account> AllAccount()
        {
            List<Account> list = new List<Account>();

            var query = db.aacounts.ToList();
            foreach (var item in query)
            {
                list.Add(new Account
                {
                    ID = item.account_id,
                    Name = item.account_name,
                    Email = item.account_email,
                    Type=item.account_type,
                    Contact = item.account_contact,
                    Location = item.account_location
                });

            }
            return list;

        }
        public List<Account> Refer(int id)
        {
            List<Account> list = new List<Account>();

            var query = db.aacounts.Join(db.refers,a=>a.account_id,r=>r.account_id,(a,r)=>new {a,r }).Where(m => m.r.advisor_id==id).ToList();
            foreach (var item in query)
            {
                list.Add(new Account
                {
                    ID = item.a.account_id,
                    Name = item.a.account_name,
                    Email = item.a.account_email,
                    Contact = item.a.account_contact,
                    Type=item.a.account_type,
                    Location = item.a.account_location
                });

            }
            return list;

        }
    }
}