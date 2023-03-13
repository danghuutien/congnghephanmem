
using gioithieudaihocvinh.Models;
using Gioithieudaihocvinh.Models;
using Microsoft.Diagnostics.Instrumentation.Extensions.Intercept;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using gioithieudaihocvinh.Utilities;
using OfficeOpenXml;

namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    public class UserController : CheckController
    {
        private GioithieuContext db = new GioithieuContext();
        // GET: Admin/User
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ajax(string searchnow)
        {
            var recordsTotal = db.Users.ToList().Count();
            if (searchnow == "")
            {
                var data = db.Users.ToList();
                var result = new
                {
                    data = data,
                    recordsTotal = recordsTotal
                };
                return Json(result, JsonRequestBehavior.AllowGet);

            }
            else
            {
                var data = db.Users.Where(i => i.Fullname.Contains(searchnow)).ToList();
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
            User Menu = db.Users.Find(id);
            db.Users.Remove(Menu);
            db.SaveChanges();
            return Json(new { status = true });
        }


        /*public ActionResult ajaxDanhmuc()
        {
             return 
        }*/

        [HttpPost]
        public ActionResult Edit(string Fullname, string Gmail, string Password, double Phone, int id)
        {
            try
            {
                User User = db.Users.Find(id);
                User.Fullname = Fullname;
                User.Gmail = Gmail;
                User.Phone = Phone;
                User.Password = Utilities.Functions.MD5Password(Password);
                User.Created_at = DateTime.Now;
                
               

                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (InvalidCastException e)
            {
                return Json(new { satus = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(string Fullname, string Gmail, string Password, double Phone)
        {
            try
            {
                User User = new User();
                User.Fullname = Fullname;
                User.Gmail = Gmail;
                User.Phone = Phone;

                User.Password = Utilities.Functions.MD5Password(Password);
                User.Created_at = DateTime.Now;
                db.Users.Add(User);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (InvalidCastException e)
            {
                return Json(new { satus = e.Message });
            }
        }

        public ActionResult DownloadExcel()
        {
            var users = db.Users.ToList();

            ExcelPackage Ep = new ExcelPackage();
            ExcelWorksheet Sheet = Ep.Workbook.Worksheets.Add("Report");

            Sheet.Cells["A1"].Value = "Họ và tên";
            Sheet.Cells["B1"].Value = "Gmail";
            Sheet.Cells["C1"].Value = "Số điện thoại";

            int row = 2;
            foreach (var item in users)
            {
                Sheet.Cells[string.Format("A{0}", row)].Value = item.Fullname;
                Sheet.Cells[string.Format("B{0}", row)].Value = item.Gmail;
                Sheet.Cells[string.Format("C{0}", row)].Value = item.Phone;
                row++;
            }


            Sheet.Cells["A:AZ"].AutoFitColumns();
            Sheet.SelectedRange["A1:T1"].Style.Font.Bold = true;
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "Report.xlsx");
            Response.BinaryWrite(Ep.GetAsByteArray());
            Response.End();
            return View("Index");
        }

    }
}