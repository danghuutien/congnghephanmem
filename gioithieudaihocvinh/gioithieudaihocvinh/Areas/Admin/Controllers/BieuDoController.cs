using Gioithieudaihocvinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    public class BieuDoController : CheckController
    {
        private GioithieuContext db = new GioithieuContext();
        // GET: Bieudo
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetData()
        {
            var tuyensinh = db.Posts.Where(i=>i.CatId == 2 || i.CatId == 3).ToList().Count();
            var dambaochatluong = db.Posts.Where(i=>i.CatId == 5 || i.CatId == 6).ToList().Count();
            var khoahoccongnghe = db.Posts.Where(i=>i.CatId == 8 || i.CatId == 9).ToList().Count();
            var hoptac = db.Posts.Where(i=>i.CatId == 11 || i.CatId == 12).ToList().Count();
            var nguoihoc = db.Posts.Where(i=>i.CatId == 14 || i.CatId == 15).ToList().Count();

            var list = new List<int>() { tuyensinh, dambaochatluong, khoahoccongnghe, hoptac, nguoihoc };
            return Json(list, JsonRequestBehavior.AllowGet);
        }
    }
}