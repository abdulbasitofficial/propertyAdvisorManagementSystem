using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Property.Models
{
    public class Refer
    {
        propertyEntities db = new propertyEntities();
        public void Add_Advisor(int advisorid,int userid)
        {
            refer rf = new refer();
            rf.advisor_id = advisorid;
            rf.account_id = userid;
            rf.property_id = 0;
            db.refers.Add(rf);
            db.SaveChanges();
        }
        public void Remove_Advisor(int advisorid, int userid)
        {
            var query = db.refers.Where(e => e.account_id == userid && e.advisor_id == advisorid).FirstOrDefault();
            db.Entry(query).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
        }
        public List<int> ReferCheck(int id)
        {
            List<int> list = new List<int>();
            var query = db.refers.Where(e => e.account_id == id).ToList();
            foreach (var item in query)
            {
                list.Add((int)item.advisor_id);
            }
            return list;
        }
    }
}