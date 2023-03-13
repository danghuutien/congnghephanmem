
using gioithieudaihocvinh.Models;
using Gioithieudaihocvinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    public class ContactController : CheckController
    {
        private GioithieuContext db = new GioithieuContext();
        // GET: Admin/Contact
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ajax(string searchnow, int start, int length)
        {
            var recordsTotal = db.Contacts.ToList().Count();
            if (searchnow == "")
            {
                var data = db.Contacts.OrderBy(x => x.Id).Skip(start).ToList();
                var result = new
                {
                    data = data,
                    recordsTotal = recordsTotal
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.Contacts.OrderBy(x => x.Id).Skip(start).Where(i => i.Title.Contains(searchnow)).ToList();
                var result = new
                {
                    data = data,
                    recordsTotal = recordsTotal
                };
                return Json(result, JsonRequestBehavior.AllowGet);

            }




        }





        [HttpPost]
        public ActionResult Delete(int id)
        {


                Contact data = db.Contacts.Find(id);
                db.Contacts.Remove(data);
                db.SaveChanges();
                return Json(new { status = true });
            


        }


        /*public ActionResult ajaxDanhmuc()
        {
             return 
        }*/

        [HttpPost]
        public ActionResult Edit(string Title, string Message, string FullName, string Gmail, int id)
        {
            try
            {
                Contact Menu = db.Contacts.Find(id);
                Menu.Title = Title;
                Menu.Message = Message;
                Menu.FullName = FullName;
                Menu.Gmail = Gmail;
                
                Menu.Created_at = DateTime.Now;
                Menu.Update_at = DateTime.Now;


                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (InvalidCastException e)
            {
                return Json(new { satus = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(string Title, string Message, string Slug, string FullName, string Gmail)
        {
            try
            {

                Contact myItem = new Contact();
                myItem.Title = Title;
                myItem.Message = Message;
                myItem.FullName = FullName;
                myItem.Gmail = Gmail;

                myItem.Created_at = DateTime.Now;
                myItem.Update_at = DateTime.Now;

                

                db.Contacts.Add(myItem);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (InvalidCastException e)
            {
                return Json(new { satus = e.Message });
            }
        }
    }
}