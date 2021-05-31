using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace petslogger.Controllers
{
    public class MesajlarController : Controller
    {
        // GET: Mesajlar
        public ActionResult Index()
        {
            return View();
        }
    }
}