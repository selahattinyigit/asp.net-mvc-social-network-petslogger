using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using ImageResizer;
using petslogger.Models;
namespace petslogger.Controllers
{
    public class YukleController : Controller
    {
        dbPetsloggerEntities ent = new dbPetsloggerEntities();
        // GET: Yukle
        public ActionResult Index()
        {
            return View();
        }

       

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Index(tb_post post, HttpPostedFileBase post_url)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (post_url == null)
                    {
                        TempData["post"] = "Lütfen Boş alanları doldurun";
                        return RedirectToAction("/Index");
                    }
                    else
                    {
                        
                        string id= Session["id"].ToString();
                        int p = ent.tb_post.Where(x => x.tb_user.id.ToString() == id).Count();
                        post.since = DateTime.Now;
                        post.user_id = int.Parse(id);
                        WebImage img = new WebImage(post_url.InputStream);
                        FileInfo imginfo = new FileInfo(post_url.FileName);
                       
                       
                        img.Resize(800, 800);

                        img.Save("~/img/post/" + id + "" +p);
                        post.post_url = "/img/post/" + id +""+p+"."+img.ImageFormat;
                        string path= "~/img/post/" + id + "" +p + "." + img.ImageFormat;
                        ResizeSettings resizeSetting = new ResizeSettings
                        {
                            Width = 800,
                            Height = 800,
                            Format = img.ImageFormat
                        };
                        ImageBuilder.Current.Build(path, path, resizeSetting);
                        ent.tb_post.Add(post);
                        ent.SaveChanges();
                        TempData["post"] = "Gönderi Paylaşıldı";

                        return RedirectToAction("/Index");
                    }
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {

                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
            return View();
        }
        }
}