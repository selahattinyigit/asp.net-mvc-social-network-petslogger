using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using petslogger.Models;
namespace petslogger.Controllers
{
    public class AraController : Controller
    {
        // GET: Ara
        dbPetsloggerEntities ent = new dbPetsloggerEntities();
        public ActionResult Index()
        {
            string id = Session["id"].ToString();
            ViewBag.oneri = ent.tb_user.Where(x => x.id.ToString() != id).OrderBy(p => Guid.NewGuid()).Take(8);
            return View(ViewBag.oneri);
        }
        public ActionResult detay(string name)
        {
            ViewBag.ara = ent.tb_user.Where(x => x.user_name.Contains(name) || x.name.Contains(name)).ToList();
            return View(ViewBag.ara);
        }
    }
}