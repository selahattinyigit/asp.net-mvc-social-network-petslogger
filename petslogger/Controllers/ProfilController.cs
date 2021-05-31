using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using petslogger.Models;
namespace petslogger.Controllers
{
    public class ProfilController : Controller
    {
        // GET: Profil
        dbPetsloggerEntities ent = new dbPetsloggerEntities();
    


        public ActionResult Index()
        {
            if (Session["user_name"] != null)
            {
                string k = Session["user_name"].ToString();
                string id = Session["id"].ToString();

                ViewBag.profil = ent.tb_user.Where(x => x.user_name == k).ToList();
                ViewBag.dost = ent.tb_friend.Where(x => id==x.tb_user.id.ToString() && x.status==true).ToList();
                ViewBag.post = ent.tb_post.Where(x => x.user_id==x.tb_user.id&&x.tb_user.user_name==k).ToList();
                ViewBag.istk = ent.tb_friend.Where(x => x.friend_id.ToString()==id && x.status==false).ToList();
                
            }
               
            return View();
        }
        public ActionResult edit(int id)
        {
            var user = ent.tb_user.Where(x => x.id == id).SingleOrDefault();
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult edit(int id,tb_user user, HttpPostedFileBase user_image_url)
        {
            if (ModelState.IsValid)
            {
              
                    var u = ent.tb_user.Where(x => x.user_name == user.user_name).SingleOrDefault();
                
                
                    if (u==null || u.id == user.id)
                    {
                    if (user.password.Length == 8)
                    {
                        u.user_name = user.user_name;
                        u.name = user.name;
                        u.about = user.about;
                        u.email = user.email;
                        u.password = user.password;
                        var k = ent.tb_user.Where(x => x.id == id).SingleOrDefault();
                        if (user_image_url != null)
                        {
                            if (System.IO.File.Exists(Server.MapPath("~/" + k.user_image_url)))
                            {
                                System.IO.File.Delete(Server.MapPath("~/" + k.user_image_url));
                            }
                            WebImage img = new WebImage(user_image_url.InputStream);
                            FileInfo imginfo = new FileInfo(user_image_url.FileName);

                            string imagename = u.user_name;
                            img.Resize(200, 200);
                            img.Save("~/img/profil/" + imagename);
                            k.user_image_url = "/img/profil/" + imagename+ "." + img.ImageFormat;

                        }
                        ent.SaveChanges();
                        return RedirectToAction("/Index");
                    }
                    else
                    {
                        TempData["mesaj"] = "Şifreniz 8 karekter uzunluğunda olmalıdır";
                        return RedirectToAction("/edit/" + id);
                    }
                    }
                    else
                    {
                        TempData["mesaj"] = "Kullanıcı Adı kullanılıyor";
                    return RedirectToAction("/edit/"+id);
                }
                }
            return View();
        }
        [HttpPost]
        public ActionResult modal(int? id)
        {

          

            ViewBag.comment = ent.tb_comment.Where(x => x.tb_post.id == id).ToList();

            Session.Add("post_id",id);
            return View(ent.tb_post.Where(x => x.id == id).SingleOrDefault());
            
        }
        [HttpPost]
        public ActionResult comment(tb_comment text)
        {
            string id = Session["id"].ToString();
            string postid = Session["post_id"].ToString();
            text.user_id = int.Parse(id);
            text.since= DateTime.Now;
            text.post_id = int.Parse(postid);
            ent.tb_comment.Add(text);
            var durum=ent.SaveChanges();

            return RedirectToAction("modal", "Profil");

        }
        public ActionResult view(int id)
        {
            string id2 = Session["id"].ToString();
            ViewBag.profil2 = ent.tb_user.Where(x => x.id == id).ToList();
            ViewBag.dost2 = ent.tb_friend.Where(x => id == x.tb_user.id && x.status == true).ToList();
            ViewBag.post2 = ent.tb_post.Where(x => x.user_id == x.tb_user.id && x.tb_user.id == id).ToList();
            ViewBag.istk2 = ent.tb_friend.Where(x => x.user_id.ToString() == id2&&x.friend_id==id).ToList();
            return View();
        }
        public ActionResult ekle(int id)
        {
            string id2 = Session["id"].ToString();
            var k = ent.tb_friend.Where(x => x.user_id.ToString() == id2 && x.friend_id == id).ToList();
            if (k==null)
            {
                tb_friend f = new tb_friend();
                f.user_id = int.Parse(id2);
                f.friend_id = id;
                f.status = false;
                ent.tb_friend.Add(f);
                ent.SaveChanges();
            }
          
            return RedirectToAction("/Profil/view/"+id);
        }
    }
}