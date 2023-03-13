using gioithieudaihocvinh.Models;
using gioithieudaihocvinh.type;
using Gioithieudaihocvinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace gioithieudaihocvinh.Controllers
{
    public class HomeController : Controller
    {

        private GioithieuContext db = new GioithieuContext();
        public ActionResult Index()
        {
            var Cat = db.Categorys.Where(i => i.Id == 1).First();
            var danhmuc = db.Categorys.Where(i => i.ParentId == Cat.Id.ToString()).OrderBy(s => s.Id).ToList();
            var Posts = db.Posts.ToList();
            List<Post> tuyensinh = new List<Post>();
            foreach(var item in danhmuc)
            {
                foreach(var post in Posts)
                {
                    if(item.Id == post.CatId)
                    {
                        tuyensinh.Add(post);
                    }
                    if(tuyensinh.Count() == 4)
                    {
                        break;
                    }

                }
            }

            var dambaochatluong = db.Posts.Where(i => i.CatId == 5 || i.CatId == 6).OrderBy(s => s.Id).ToList();
            var hocbong = db.Posts.Where(i => i.CatId == 11 ).Take(2).ToList();
            var nguoihoc = db.Posts.Where(i => i.CatId == 14 || i.CatId == 15).OrderBy(s => s.Id).Take(5).ToList();

            /*return Json(tuyensinh[0], JsonRequestBehavior.AllowGet);*/
            ViewBag.nguoihoc = nguoihoc;
            ViewBag.hocbong = hocbong;
            ViewBag.tuyensinh = tuyensinh;
            ViewBag.dambaochatluong = dambaochatluong;
            return View();
        }


        [HttpPost]
        public ActionResult AjaxComment(int CatId)
        {

            var data = db.Comments.Where(s => s.CatId == CatId).OrderBy(x => x.Id).ToList();



            return Json(data, JsonRequestBehavior.AllowGet);

        }

        public ActionResult TinTuc(int id, int page = 1)
        {
            var start = (page - 1) * 8;

            var danhmuc = db.Categorys.Where(s=>s.Id == id).First();
            if(danhmuc != null && danhmuc.ParentId != "")
            {
                var danhmuccha = db.Categorys.Where(j => j.Id.ToString() == danhmuc.ParentId).OrderBy(s => s.Id).First();
                ViewBag.danhmuccha = danhmuccha;
            }
            else
            {
                var danhmuccha = "";
                ViewBag.danhmuccha = danhmuccha;
            }
            var TinTuc = db.Posts.Where(i => i.CatId == id).OrderBy(s => s.Id).Skip(start).Take(8).ToList();
            var background = db.Backgrounds.Where(i => i.CatId == id).OrderBy(s => s.Id).Skip(start).Take(1).ToList();

            double countNew = db.Posts.Where(i => i.CatId == id).OrderBy(s => s.Id).ToList().Count()/8.0;
            var recordsTotal = Math.Ceiling(countNew);
            var count = db.Posts.Where(i => i.CatId == id).OrderBy(s => s.Id).ToList().Count();


            ViewBag.id = id;
            ViewBag.count = count;
            ViewBag.danhmuc = danhmuc;
            ViewBag.TinTuc = TinTuc;
            ViewBag.background = background;


            ViewBag.recordsTotal = recordsTotal;
            ViewBag.page = page;
            return View(background);
        }

        public ActionResult ChiTietTinTuc(int id)
        {
            var ChiTietTinTuc = db.Posts.Where(i => i.Id == id).OrderBy(s => s.Id).First();
            var danhmuc = db.Categorys.Where(s => s.Id == ChiTietTinTuc.CatId).OrderBy(s => s.Id).First();
            var danhmuccha = db.Categorys.Where(s => s.Id.ToString() == danhmuc.ParentId).OrderBy(s => s.Id).First();


            ViewBag.ChiTietTinTuc = ChiTietTinTuc;
            ViewBag.danhmuc = danhmuc;
            ViewBag.danhmuccha = danhmuccha;


            return View();
        }

        public ActionResult LienHe()
        {
            return View();
        }
        public ActionResult GetQuestion(int id)
        {
            var data = db.Questions.Where(i=>i.Status == true && i.PostId == id).OrderBy(s => s.Id).ToList();
            return Json(data, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public ActionResult GetDanhMuc()
        {
            List<InterFace> data = new List<InterFace>();
            var category = db.Categorys.Where(i => i.ParentId == "").ToList();
            foreach (var item in category)
            {
                var dem = db.Categorys.Where(i => i.ParentId.Equals(item.Id.ToString())).ToList().Count();
                InterFace obj = new InterFace();
                obj.Childrens = db.Categorys.Where(i => i.ParentId.Equals(item.Id.ToString())).ToList();
                obj.Kiemtra = 0;
                obj.Category = item;

                if (dem > 0)
                {
                    obj.Childrens = db.Categorys.Where(i => i.ParentId.Equals(item.Id.ToString())).ToList();
                    obj.Kiemtra = 1;
                }
                data.Add(obj);
            }
            
            return Json(data, JsonRequestBehavior.AllowGet);
        }



    }
}