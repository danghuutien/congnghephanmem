using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    public class CheckController : Controller
    {
        // GET: Admin/Check
        public CheckController()
        {
            if (System.Web.HttpContext.Current.Session["UserId"].Equals(""))
            {
                System.Web.HttpContext.Current.Response.Redirect("~/Admin/Login/Index");
            }
        }
    }
}