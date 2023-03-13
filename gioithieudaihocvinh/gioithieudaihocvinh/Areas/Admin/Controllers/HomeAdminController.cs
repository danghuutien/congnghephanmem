
using Gioithieudaihocvinh.Models;

using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    public class HomeAdminController : CheckController
    {
        private GioithieuContext db = new GioithieuContext();
        public ActionResult Index()
        {
           
           
            return View();
        }

      



    }
}