
using gioithieudaihocvinh.Models;
using Gioithieudaihocvinh.Models;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using static System.Net.WebRequestMethods;


namespace gioithieudaihocvinh.Areas.Admin.Controllers
{
    public class PostController : CheckController
    {



        private GioithieuContext db = new GioithieuContext();
        // GET: Admin/Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ajax(string searchnow, int start, int length)
        {

            var recordsTotal = db.Posts.ToList().Count();
            if (searchnow == "")
            {
                var data = (from Post in db.Posts
                            join Category in db.Categorys on Post.CatId equals Category.Id
                                select new
                                {
                                    Id = Post.Id,
                                    CatId = Post.CatId,
                                    Title = Post.Title,
                                    Slug = Post.Slug,
                                    Thumbnail = Post.Thumbnail,
                                    Excerpt = Post.Excerpt,
                                    Content = Post.Content,
                                    IsHighlight = Post.IsHighlight,
                                    Category = Category.Name
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
                var data = (from Post in db.Posts
                            join Category in db.Categorys on Post.Id equals Category.Id
                            where Post.Title.Contains(searchnow)
                            select new
                            {
                                Id = Post.Id,
                                CatId = Post.CatId,
                                Title = Post.Title,
                                Slug = Post.Slug,
                                Thumbnail = Post.Thumbnail,
                                Excerpt = Post.Excerpt,
                                Content = Post.Content,
                                IsHighlight = Post.IsHighlight,
                            }).OrderBy(x => x.Id).Skip(start).Take(length).ToList();


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

            Post data = db.Posts.Find(id);
            db.Posts.Remove(data);
            db.SaveChanges();
            return Json(new { status = true });
            
 

        }


        /*public ActionResult ajaxDanhmuc()
        {
             return 
        }*/

        [HttpPost]
        public ActionResult Edit( string Content, string Title, string Slug, int CatId, string Thumbnail, string Excerpt, int id)
        {
            try
            {
                string[] testThumbnail = Thumbnail.Split(new char[] { '\\' });
                
                Post Menu = db.Posts.Find(id);
                Menu.Thumbnail = "/upload/thumbnails/" + testThumbnail[testThumbnail.Length - 1];
                
                Menu.Title = Title;
                Menu.Content = Content;
                Menu.CatId = CatId;
                Menu.Slug = Slug;
                Menu.Excerpt = Excerpt;
                Menu.Created_at = DateTime.Now;
                Menu.Updated_at = DateTime.Now;
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (InvalidCastException e)
            {
                return Json(new { satus = e.Message });
            }
        }

        [HttpPost]
        public ActionResult Create(string File, string Thumbnail, string Content, string Title, string Slug, int CatId, string Excerpt)
        {
            try
            {
                string[] testThumbnail = Thumbnail.Split(new char[] { '\\' });
                

                Post myItem = new Post();
                myItem.Thumbnail = "/upload/thumbnails/" + testThumbnail[testThumbnail.Length - 1];
                
                myItem.Title = Title;
                myItem.Content = Content;
                myItem.CatId = CatId;
                myItem.Slug = Slug;
                myItem.Excerpt = Excerpt;
                myItem.Created_at = DateTime.Now;

                myItem.Updated_at = DateTime.Now;



                db.Posts.Add(myItem);
                db.SaveChanges();
                return Json(new { status = true });
            }
            catch (InvalidCastException e)
            {
                return Json(new { status = e.Message });
            }
        }


        [HttpPost]
        public ActionResult UploadThumbnail()
        {
            if (Request.Files.Count > 0)
            {
                
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;

                    for (int i = 0; i < files.Count; i++)
                    {
                        

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname =  testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }


                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/upload/thumbnails/"), fname);

                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(new { status = true });
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json(new { status = false });
            }
        }

        [HttpPost]
        public ActionResult UploadFile()
        {
            if (Request.Files.Count > 0)
            {

                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;

                    for (int i = 0; i < files.Count; i++)
                    {


                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }


                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/upload/Files/"), fname);

                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json(new { status = true });
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json(new { status = false });
            }
        }

        

    }
}