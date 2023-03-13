using gioithieudaihocvinh.Models;
using Gioithieudaihocvinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    public class QuestionController : CheckController
    {
        private GioithieuContext db = new GioithieuContext();
        // GET: Admin/Question
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ajax(string searchnow, int start, int length)
        {
            var recordsTotal = db.Questions.ToList().Count();
            if (searchnow == "")
            {
                var data = db.Questions.OrderBy(x => x.Id).Skip(start).ToList();
                var result = new
                {
                    data = data,
                    recordsTotal = recordsTotal
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = db.Questions.OrderBy(x => x.Id).Skip(start).Where(i => i.Content.Contains(searchnow)).ToList();
                var result = new
                {
                    data = data,
                    recordsTotal = recordsTotal
                };
                return Json(result, JsonRequestBehavior.AllowGet);

            }






        }
        [HttpPost]
        public ActionResult Create(bool Status, string Slug, string Title, string Content, int PostId)
        {
            try
            {

                Question myItem = new Question();
                myItem.Title = Title;
                myItem.Content = Content;
                myItem.PostId = PostId;
                myItem.Slug = Slug;
                myItem.Status = Status;


                db.Questions.Add(myItem);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (InvalidCastException e)
            {
                return Json(new { satus = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Edit(string Answer, bool Status, string Slug, string Title, string Content, int PostId, int id)
        {
            try
            {
                Question myItem = db.Questions.Find(id);
               
                myItem.Title = Title;
                myItem.Content = Content;
                myItem.PostId = PostId;
                myItem.Slug = Slug;
                myItem.Status = Status;
                myItem.Answer = Answer;

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