using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.SessionState;

namespace Property.Models
{
    [SessionState(SessionStateBehavior.Default)]
    public class PropertyClass
    {
        propertyEntities db = new propertyEntities();
        public int ID { get; set; }
        public int AdvisorID { get; set; }
        public int OfferBy { get; set; }
        public int Company { get; set; }

        public int ID_Property { get; set; }
        public int ID_Refer { get; set; }
        public string Type { get; set; }

        public string OwnerName { get; set; }
        public string Contact { get; set; }
        public string Location { get; set; }

        public int Price { get; set; }
        public int PriceOffer { get; set; }
        public int ReferPrice { get; set; }
        public int Bed_Room { get; set; }
        public int Wash_Room { get; set; }
        public int Marla { get; set; }

        public byte[] Image { get; set; }

        public string Image_Type { get; set; }

        public void Insertion(PropertyClass p, HttpPostedFileBase image, int account_id)
        {
            byte[] bytes;
            BinaryReader br = new BinaryReader(image.InputStream);
            bytes = br.ReadBytes(image.ContentLength);

            property pro= new property();
            pro.property_type = p.Type;
            pro.company_id = p.Company;
            pro.property_location = p.Location;
            pro.property_price = p.Price;
            pro.property_bed = p.Bed_Room;
            pro.property_washroom = p.Wash_Room;
            pro.property_size = p.Marla;
            pro.property_image = bytes;
            pro.property_image_type = image.ContentType;
            pro.account_id = account_id;
            db.properties.Add(pro);
            db.SaveChanges();
        }
        public List<PropertyClass> Property(int id)
        {
            List<PropertyClass> list = new List<PropertyClass>();
            var query = db.properties.Where(e => e.account_id == id).ToList();
            foreach (var item in query)
            {
                list.Add(new PropertyClass {
                    Type = item.property_type,
                    Location = item.property_location,
                    Bed_Room = (int)item.property_bed,
                    Wash_Room= (int)item.property_washroom,
                    Marla=(int)item.property_size,
                    Price= (int)item.property_price,
                    Image=item.property_image,
                    Image_Type=item.property_image_type
                });
            }
            return list;
        }
        public List<PropertyClass> Property()
        {
            List<PropertyClass> list = new List<PropertyClass>();
            var query = db.properties.Join(db.aacounts,p=>p.account_id,a=>a.account_id,(p,a)=>new {p,a }).ToList();
            foreach (var item in query)
            {
                list.Add(new PropertyClass
                {
                    ID=item.p.property_id,
                    Type = item.p.property_type,
                    Location = item.p.property_location,
                    Bed_Room = (int)item.p.property_bed,
                    Wash_Room = (int)item.p.property_washroom,
                    Marla = (int)item.p.property_size,
                    Price = (int)item.p.property_price,
                    Image = item.p.property_image,
                    Image_Type = item.p.property_image_type,
                    OwnerName = item.a.account_name,
                    Company=(int)item.p.company_id,
                    Contact=item.a.account_contact
                });
            }
            return list;
        }
        public List<PropertyClass> Search(string type, string location, string from, string to)
        {
            int f = int.Parse(from);
            int t = int.Parse(to);
            List<PropertyClass> list = new List<PropertyClass>();
            var query = db.properties.Join(db.aacounts, p => p.account_id, a => a.account_id, (p, a) => new { p, a }).Where(m=>m.p.property_type.Equals(type) && m.p.property_location.Equals(location) && m.p.property_price==f && m.p.property_price == t).ToList();
            foreach (var item in query)
            {
                list.Add(new PropertyClass
                {
                    ID = item.p.property_id,
                    Type = item.p.property_type,
                    Location = item.p.property_location,
                    Bed_Room = (int)item.p.property_bed,
                    Wash_Room = (int)item.p.property_washroom,
                    Marla = (int)item.p.property_size,
                    Price = (int)item.p.property_price,
                    Image = item.p.property_image,
                    Image_Type = item.p.property_image_type,
                    OwnerName = item.a.account_name,
                    Company = (int)item.p.company_id,
                    Contact = item.a.account_contact
                });
            }
            return list;
        }
        public List<PropertyClass> PropertyCompany(int id)
        {
            List<PropertyClass> list = new List<PropertyClass>();
            var query = db.properties.Join(db.aacounts, p => p.account_id, a => a.account_id, (p, a) => new { p, a }).Where(m => m.p.company_id==id).ToList();
            foreach (var item in query)
            {
                list.Add(new PropertyClass
                {
                    ID = item.p.property_id,
                    Type = item.p.property_type,
                    Location = item.p.property_location,
                    Bed_Room = (int)item.p.property_bed,
                    Wash_Room = (int)item.p.property_washroom,
                    Marla = (int)item.p.property_size,
                    Price = (int)item.p.property_price,
                    Image = item.p.property_image,
                    Image_Type = item.p.property_image_type,
                    OwnerName = item.a.account_name,
                    Company = (int)item.p.company_id,
                    Contact = item.a.account_contact
                });
            }
            return list;
        }
        public PropertyClass PropertySingle(int id)
        {
            PropertyClass list = new PropertyClass();
            var item = db.properties.Join(db.aacounts, p => p.account_id, a => a.account_id, (p, a) => new { p, a }).Where(m=>m.p.property_id==id).FirstOrDefault();
            list.ID = item.p.property_id;
            list.Type = item.p.property_type;
            list.Location = item.p.property_location;
            list.Bed_Room = (int)item.p.property_bed;
            list.Wash_Room = (int)item.p.property_washroom;
            list.Marla = (int)item.p.property_size;
            list.Price = (int)item.p.property_price;
            list.Image = item.p.property_image;
            list.Image_Type = item.p.property_image_type;
            list.OwnerName = item.a.account_name;
            list.Contact = item.a.account_contact;
            list.Company = (int)item.p.company_id;
            return list;
        }
        public void Refer(int property, int userid,int price)
        {
            var query = db.refers.Where(e => e.advisor_id == userid).ToList();
            foreach (var item in query)
            {
                var check = db.referproperties.Where(e => e.property_id == property && e.account_id == item.account_id).ToList();
                if(check.Count()>0)
                {
                   //not do any thing
                }
                else
                {
                    referproperty refpro = new referproperty();
                    refpro.account_id = item.account_id;
                    refpro.property_id = property;
                    refpro.referPrice = price;
                    db.referproperties.Add(refpro);
                    db.SaveChanges();
                }
            }
        }
        public void Rating(int companyId, int star,int uid)
        {
            
                var check = db.ratings.Where(e => e.property_id == companyId && e.account_id == uid).FirstOrDefault();
                if (check!=null)
                {
                  check.stars = star;
                  db.SaveChanges();
                    //not do any thing
                }
                else
                {
                    rating refpro = new rating();
                    refpro.account_id = uid;
                    refpro.property_id = companyId;
                    refpro.stars = star;
                    db.ratings.Add(refpro);
                    db.SaveChanges();
                }
            
        }
        public void Offer(int proid, int PriceOff,int uid)
        {
            var check = db.offers.Where(e => e.property_id == proid&&e.account_id==uid).ToList();
            if (check.Count() > 0)
                {
                    //not do any thing
                }
                else
                {
                    offer pro = new offer();
                    pro.account_id = uid;
                    pro.property_id = proid;
                    pro.offer_price = PriceOff;
                    db.offers.Add(pro);
                    db.SaveChanges();
                }
            
        }
        public List<PropertyClass> OfferRequest( int uid)
        {
            List<PropertyClass> list = new List<PropertyClass>();
            var query = db.offers.Join(db.properties,o=>o.property_id,p=>p.property_id,(o,p)=>new {o,p }).Join(db.aacounts,m=>m.p.account_id,a=>a.account_id,(m,a)=>new {m,a }).Where(m => m.m.p.account_id == uid).ToList();
            foreach ( var item in query)
            {
                list.Add(new PropertyClass
                {
                    ID = item.m.p.property_id,
                    Type = item.m.p.property_type,
                    Location = item.m.p.property_location,
                    Bed_Room = (int)item.m.p.property_bed,
                    Wash_Room = (int)item.m.p.property_washroom,
                    Marla = (int)item.m.p.property_size,
                    Price = (int)item.m.p.property_price,
                    Image = item.m.p.property_image,
                    Image_Type = item.m.p.property_image_type,
                    OwnerName = item.a.account_name,
                    Company = (int)item.m.p.company_id,
                    Contact = item.a.account_contact,
                    PriceOffer=(int)item.m.o.offer_price,
                    OfferBy=(int)item.m.o.account_id
                });
            }
            return list;
        }
        public List<PropertyClass> Notification(int id)
        {
            List<PropertyClass> list = new List<PropertyClass>();
            var q1 = db.refers.Join(db.referproperties, r => r.account_id, rf => rf.account_id, (r, rf) => new { r, rf }).Where(m => m.rf.account_id == id).ToList();
            var q2 = db.properties.Join(db.aacounts,p=>p.account_id,a=>a.account_id,(p,a)=>new{p,a }).ToList();
            var query = q1.Join(q2, m1 => m1.rf.property_id, m2 => m2.p.property_id, (m1, m2) => new { m1, m2 }).ToList();
            foreach (var item in query)
            {
                list.Add(new PropertyClass
                {
                    ID=item.m2.p.property_id,
                    ID_Property=(int)item.m2.p.account_id,
                    ID_Refer=(int)item.m1.r.advisor_id,
                    Type = item.m2.p.property_type,
                    Location = item.m2.p.property_location,
                    Bed_Room = (int)item.m2.p.property_bed,
                    Wash_Room = (int)item.m2.p.property_washroom,
                    Marla = (int)item.m2.p.property_size,
                    Price = (int)item.m2.p.property_price,
                    Image = item.m2.p.property_image,
                    Image_Type = item.m2.p.property_image_type,
                    ReferPrice=(int)item.m1.rf.referPrice,
                    AdvisorID=(int)item.m1.r.advisor_id
                });
            }
            return list;
        }
    }
}
public enum type { Home, Office, Land, Appartment };
public enum location { Islamabad, Faisalabad, Rawalpindi, Multan, Peshawar, Lahore, Karachi, Kashmir };