

using gioithieudaihocvinh.Models;
using gioithieudaihocvinh.type;
using Gioithieudaihocvinh.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    
    public class MenuController : CheckController
    {
        private GioithieuContext db = new GioithieuContext();

        // GET: Admin/Menu
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AjaxDanhmuc(string searchnow, int start, int length)
        {
            var recordsTotal = db.Categorys.ToList().Count();
            if (searchnow == "")
            {
                var data = (from Category in db.Categorys
                            join loai in db.Typecategorys on Category.Typecategory_id equals loai.Id
                            select new
                            {
                                Id = Category.Id,
                                Name = Category.Name,
                                Typecategory_id = Category.Typecategory_id,
                                Slug = Category.Slug,
                                ParentId = Category.ParentId,
                                TypecategoryName = loai.Name,
                                
                            }).OrderBy(x => x.Id).Skip(start).Take(length).ToList();
                var result = new
                {
                    data = data,
                    recordsTotal = recordsTotal
                };

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var data = (from TypecategryItem in db.Typecategorys
                            join Category in db.Categorys on TypecategryItem.Id equals Category.Typecategory_id
                            where Category.Name.Contains(searchnow)
                            select new
                            {
                                Id = Category.Id,
                                Name = Category.Name,
                                Typecategory_id = Category.Typecategory_id,
                                Slug = Category.Slug,
                                ParentId = Category.ParentId,
                                TypecategoryName = TypecategryItem.Name,

                            }).OrderBy(x => x.Id).Skip(start).Take(length).ToList();
                var result = new
                {
                    data = data,
                    recordsTotal = recordsTotal
                };
                return Json(result, JsonRequestBehavior.AllowGet);
                
            }


            

        }

        public ActionResult GetDanhmuc1(string txtSearch, int? page)
        {
            var data = db.Categorys.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);

        }

        [HttpGet]
        public ActionResult GetDanhMuc()
        {
            List<Treeselect> data = new List<Treeselect>();
            var category = db.Categorys.Where(i => i.ParentId == "").ToList();
            foreach (var item in category)
            {
                var dem = db.Categorys.Where(i => i.ParentId.Equals(item.Id.ToString())).ToList().Count();
                Treeselect obj = new Treeselect();
                
                obj.Id = item.Id;
                obj.Name = item.Name;

                if (dem > 0)
                {
                    obj.Childrens = db.Categorys.Where(i => i.ParentId.Equals(item.Id.ToString())).ToList();
                    
                }
                data.Add(obj);
            }

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetTypeCategory(string txtSearch, int? page)
        {
            var data = db.Typecategorys.ToList();

            return Json(data, JsonRequestBehavior.AllowGet);

        }



        [HttpPost]
        public ActionResult DeleteDanhmuc(int id)
        {
            Category Menu = db.Categorys.Find(id);
            db.Categorys.Remove(Menu);
            db.SaveChanges();
            return Json(new { status = true });
        }


        /*public ActionResult ajaxDanhmuc()
        {
             return 
        }*/

        [HttpPost]
        public ActionResult EditDanhmuc(string name, string slug, int Typecategory_id, string ParentId, int id)
        {
            try
            {
                Category Menu = db.Categorys.Find(id);
                Menu.Name = name;
                Menu.Typecategory_id = Typecategory_id;
                Menu.ParentId = ParentId;
                Menu.Slug = slug;
                Menu.Created_at = DateTime.Now;
                Menu.Created_by = DateTime.Now;
                Menu.Updated_at = DateTime.Now;
                Menu.Updated_by = DateTime.Now;
                
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch(InvalidCastException e)
            {
                return Json(new { satus = e.Message });
            }
        }

        [HttpPost]
        public ActionResult CreateDanhmuc(string name, string slug, int Typecategory_id, string ParentId)
        {
            try
            {
                Category myItem = new Category();
                myItem.Name = name;
                myItem.Typecategory_id = Typecategory_id;
                myItem.ParentId = ParentId;
                myItem.Slug = slug;
                myItem.Created_at = DateTime.Now;
                db.Categorys.Add(myItem);
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