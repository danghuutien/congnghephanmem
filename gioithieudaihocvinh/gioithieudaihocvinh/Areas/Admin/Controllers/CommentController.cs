using gioithieudaihocvinh.Models;
using Gioithieudaihocvinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    public class CommentController : CheckController
    {
        // GET: Admin/Comment

        private GioithieuContext db = new GioithieuContext();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Ajax(string searchnow, int start, int length)
        {
            var recordsTotal = db.Comments.ToList().Count();
            if (searchnow == "")
            {
                var data = db.Comments.OrderBy(x => x.Id).Skip(start).Take(length).ToList();
                var result = new
                {
                    data = data,
                    recordsTotal = recordsTotal
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.Comments.OrderBy(x => x.Id).Skip(start).Where(i => i.Content.Contains(searchnow)).ToList();
                var result = new
                {
                    data = data,
                    recordsTotal = recordsTotal
                };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

        

        [HttpPost]
        public ActionResult Create(string Created_by, string Content, int CatId)
        {
            try
            {

                Comment myItem = new Comment();
                myItem.Created_by = Created_by;
                myItem.Content = Content;
                myItem.CatId = CatId; 
                myItem.Created_at = DateTime.Now;
                myItem.Update_at = DateTime.Now;

                db.Comments.Add(myItem);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (InvalidCastException e)
            {
                return Json(new { satus = e.Message });
            }
        }
            [HttpPost]
        public ActionResult Delete(int id)
        {
            Comment Menu = db.Comments.Find(id);
            db.Comments.Remove(Menu);
            db.SaveChanges();
            return Json(new { status = true });
        }

    }
}