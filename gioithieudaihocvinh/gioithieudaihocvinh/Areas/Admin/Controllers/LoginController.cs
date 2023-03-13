using gioithieudaihocvinh.Models;
using Gioithieudaihocvinh.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        private GioithieuContext db = new GioithieuContext();
        public ActionResult Index()
        {
            if(Session["UserId"] == "")
            {
                return View();
            }
            else
            {
                return Redirect("/Admin/HomeAdmin/Index");
            }
            
        }

        [HttpPost]
        
        public ActionResult Check(string Gmail, string password)
        {
            try
            {
                var pw = Utilities.Functions.MD5Password(password);
                var listUser = db.Users.Where(i=>i.Gmail.Equals(Gmail) && i.Password.Equals(pw)).ToList().Count();
                if (listUser > 0) {
                    User User = db.Users.Where(i => i.Gmail.Equals(Gmail) && i.Password.Equals(pw)).First();
                    if(User != null)
                    {
                        Session["UserId"] = User.Id;
                        Session["Fullname"] = User.Fullname;
                        Session["Gmail"] = User.Gmail;
                        Session["Phone"] = User.Phone;
                        return Json(new { status = true });
                    }
                    else
                    {
                        return Json(new { status = false });

                    }

                }
                return Json(new { status = false });
            }
            catch (InvalidCastException e)
            {
                return Json(new { satus = e.Message });
            }
        }

        public ActionResult Logout()
        {
            Session["UserId"] = "";
            Session["Fullname"] = "";
            Session["Gmail"] = "";
            Session["Phone"] = "";
            return RedirectToAction("Index");
        }

    }
}