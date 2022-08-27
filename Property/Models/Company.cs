using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Property.Models
{
    public class Company
    {
        propertyEntities db = new propertyEntities();
        public int ID { get; set; }
        public decimal Stars { get; set; }

        public float StarsNumber { get; set; }
        public int TotalUsers { get; set; }
        public string Name { get; set; }

        public byte[] Image { get; set; }

        public string Image_Type { get; set; }

        public void Insertion(Company p, HttpPostedFileBase image, int account_id)
        {
            byte[] bytes;
            BinaryReader br = new BinaryReader(image.InputStream);
            bytes = br.ReadBytes(image.ContentLength);
            company pro = new company();
            pro.company_name = p.Name;
            pro.company_image = bytes;
            pro.account_id = account_id;
            pro.company_image_type = image.ContentType;
            db.companies.Add(pro);
            db.SaveChanges();
        }
        public List<Company> CompanyList(int id)
        {
            List<Company> list = new List<Company>();
            var query = db.companies.Where(e=>e.account_id==id).ToList();
            foreach (var item in query)
            {
                list.Add(new Company
                {
                    ID = item.company_id,
                    Name=item.company_name,
                    Image = item.company_image,
                    Image_Type = item.company_image_type
                });
            }
            return list;
        }
        public List<Company> CompanyList()
        {
            List<Company> list = new List<Company>();
            var query = db.companies.Where(e=>e.company_id!=0).ToList();
            foreach (var item in query)
            {
                list.Add(new Company
                {
                    ID = item.company_id,
                    Name = item.company_name,
                    Image = item.company_image,
                    Image_Type = item.company_image_type
                });
            }
            return list;
        }
        public List<Company> CompanyRate(int uid)
        {
            List<Company> list = new List<Company>();
            var query = db.companies.Join(db.ratings,c=>c.company_id,r=>r.property_id,(c,r)=>new {c,r }).Where(e => e.c.company_id != 0 && e.r.account_id==uid).ToList();
            foreach (var item in query)
            {
                list.Add(new Company
                {
                    ID = (int)item.r.property_id,
                    Stars= (int)item.r.stars
                });
            }
            return list;
        }
    }
}