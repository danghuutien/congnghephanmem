using gioithieudaihocvinh.Areas.Admin.Controllers;
using gioithieudaihocvinh.Models;
using Gioithieudaihocvinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    public class BackgroundController : CheckController
    {
        // GET: Admin/Background
        private GioithieuContext db = new GioithieuContext();

        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ajax(string searchnow)
        {
            var recordsTotal = db.Backgrounds.ToList().Count();
            if (searchnow == "")
            {
                var data = (from danhmuc in db.Categorys
                            join Background in db.Backgrounds on danhmuc.Id
                            equals Background.CatId
                            select new
                            {
                                Id = Background.Id,
                                Title = Background.Title,
                                CatId = Background.CatId,
                                Thumbnail = Background.Thumbnail,
                                Danhmuc = danhmuc.Name,
                            }
                           ).ToList();
                var result = new
                {
                    data = data,
                    recordsTotal = recordsTotal
                };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = (from danhmuc in db.Categorys
                            join Background in db.Backgrounds on danhmuc.Id
                            equals Background.CatId
                            select new
                            {
                                Id = Background.Id,
                                Title = Background.Title,
                                CatId = Background.CatId,
                                Thumbnail = Background.Thumbnail,
                                Danhmuc = danhmuc.Name,
                            }
                          ).Where(i => i.Title.Contains(searchnow)).ToList();
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
            Background Menu = db.Backgrounds.Find(id);
            db.Backgrounds.Remove(Menu);
            db.SaveChanges();
            return Json(new { status = true });
        }


        /*public ActionResult ajaxDanhmuc()
        {
             return 
        }*/

        [HttpPost]
        public ActionResult Edit(string Title, string Thumbnail, int CatId, int id)
        {
            try
            {
                string[] testThumbnail = Thumbnail.Split(new char[] { '\\' });
                Background Menu = db.Backgrounds.Find(id);
                Menu.Title = Title;
                Menu.Thumbnail = "/upload/thumbnails/" + testThumbnail[testThumbnail.Length - 1];
                Menu.CatId = CatId;

                Menu.Created_at = DateTime.Now;


                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (InvalidCastException e)
            {
                return Json(new { satus = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(string Title, string Thumbnail, int CatId)
        {
            try
            {
                string[] testThumbnail = Thumbnail.Split(new char[] { '\\' });
                Background myItem = new Background();
                myItem.Title = Title;

                myItem.Thumbnail = "/upload/thumbnails/" + testThumbnail[testThumbnail.Length - 1];

                myItem.CatId = CatId;

                myItem.Created_at = DateTime.Now;
                db.Backgrounds.Add(myItem);
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