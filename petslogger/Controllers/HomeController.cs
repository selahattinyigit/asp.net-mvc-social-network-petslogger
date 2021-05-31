using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using petslogger.Models;
namespace petslogger.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        dbPetsloggerEntities ent = new dbPetsloggerEntities();
        public ActionResult Index()
        {
            string id = Session["id"].ToString();
           
            var ekle = from sf in ent.tb_post
                       join sb in ent.tb_user on sf.user_id equals sb.id
                       join sb2 in ent.tb_friend on sf.user_id equals sb2.friend_id
                       where sb2.status==true && sb2.user_id.ToString()==id && sf.user_id.ToString()!=id
                       select new
                       {
                          sf.id

                       };
           
                ViewBag.posts = ent.tb_post.Where(x => x.tb_user.tb_friend1.Where(z=>z.user_id.ToString()==id).FirstOrDefault().friend_id==x.user_id&&x.user_id.ToString()!=id).OrderByDescending(f=>f.id).ToList();
            ViewBag.oneri=ent.tb_user.Where(x=>x.id.ToString()!=id).OrderBy(p=>Guid.NewGuid()).Take(8);


            return View(ViewBag.posts);
        }
        [HttpPost]
        public ActionResult modal(int? id)
        {



            ViewBag.comment = ent.tb_comment.Where(x => x.tb_post.id == id).ToList();
            return View(ent.tb_post.Where(x => x.id == id).SingleOrDefault());

        }
    }
}