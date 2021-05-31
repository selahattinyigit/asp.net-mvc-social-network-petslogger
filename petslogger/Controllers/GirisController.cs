using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using petslogger.Models;
namespace petslogger.Controllers
{
    
    public class GirisController : Controller
    {
        // GET: Giris
        dbPetsloggerEntities ent = new dbPetsloggerEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Login([Bind(Include = "user_name,password")] tb_user course)
        {
            TempData["mesaj"] = null;
            try
            {
                if (ModelState.IsValid)
                {
                    if (course.user_name == null || course.password == null)
                    {
                        TempData["mesaj"] = "Lütfen Boş alanları doldurun";
                        return RedirectToAction("/Login");
                    }
                    else
                    {
                        var k = ent.tb_user.Where(x => x.user_name == course.user_name && x.password==course.password).SingleOrDefault();
                        if (k != null)
                        {
                            Session.Add("user_name", k.user_name);
                            Session.Add("id", k.id);
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            TempData["mesaj"] = "Kullanıcı Adı veya Şifre hatalı";
                            return RedirectToAction("/Login");
                        }
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
       
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        
        
        public ActionResult Register([Bind(Include ="email,user_name,password")]tb_user course)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (course.password == null || course.user_name == null || course.email == null)
                    {
                        TempData["mesaj"] = "Lütfen Boş alanları doldurun";
                        return RedirectToAction("/Register");
                    }
                    else
                    {
                        var k = ent.tb_user.Where(x => x.user_name == course.user_name).SingleOrDefault();
                        if (k == null)
                        {
                            if (course.password.Length == 8)
                            {
                                
                                ent.tb_user.Add(course);
                                ent.SaveChanges();
                                return RedirectToAction("/");
                            }
                            else
                                TempData["mesaj"] = "Şifreniz 8 karekter uzunluğunda olmalıdır";
                            return RedirectToAction("/Register");
                        }
                        else
                        {
                            TempData["mesaj"] = "Kullanıcı Adı kullanımda";
                            return RedirectToAction("/Register");
                        }
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
        public ActionResult Forgot()
        {
            return View();
        }
    }
}