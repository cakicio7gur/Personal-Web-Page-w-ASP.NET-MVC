using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace PersonalWebSite.Controllers
{
    public class SecurityController : Controller
    {
        Models.PersonalWebPageDBEntities1 db = new Models.PersonalWebPageDBEntities1();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Models.Uye model)
        {
            var user = db.Uye.FirstOrDefault(m => m.UyeDetay.kullaniciAdi == model.UyeDetay.kullaniciAdi && m.UyeDetay.sifre == model.UyeDetay.sifre);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UyeDetay.kullaniciAdi, false);
                Session["Kullanici"] = user.UyeDetay.kullaniciAdi;
                Session["Oturum"] = "true";
                Session["Yetki"] = user.Rol.rol1;
                Session["UyeId"] = user.uyeID;   
                if (user.Rol.rol1 == "Admin")
                {
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.mesaj = "Geçersiz Kullanıcı Adı veya Şifre";
                return View();
            }
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session["Kullanici"] = null;
            Session["Oturum"] = null;
            Session["UyeId"] = null;
            Session["Yetki"] = null;
            return RedirectToAction("Index", "Home");
        }
    }
}