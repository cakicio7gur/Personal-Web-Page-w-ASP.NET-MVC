using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using PersonalWebSite.Models;

namespace PersonalWebSite.Controllers
{
    public class UserController : Controller
    {
        Models.PersonalWebPageDBEntities1 db = new Models.PersonalWebPageDBEntities1();

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Uye uye)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.mesaj = "Kayıt Başarısız!";
                return RedirectToAction("Register", "User");
            }
            else
            {
                uye.rolID = 2;
                uye.UyeDetay.fotograf = "user.png";
                db.Uye.Add(uye);
                db.SaveChanges();
                ViewBag.mesaj = "Kayıt Başarılı!";
                return RedirectToAction("Register", "User");
            }
        }
        public ActionResult UserProfile(int id)
        {
            var user = db.Uye.Find(id);
            return View("UserProfile", user);
        }

        [HttpPost]
        public ActionResult UpdateUser(Uye uye,HttpPostedFileBase fotograf)
        {
            var UpdateModel = db.Uye.Find(uye.uyeID);
            if(fotograf != null)
            {
                WebImage Img = new WebImage(fotograf.InputStream);
                FileInfo fotoinfo = new FileInfo(fotograf.FileName);
                string newFoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                Img.Resize(120,120);
                Img.Save("~/Upload/UserProfilePhotos/" + newFoto);
                UpdateModel.UyeDetay.fotograf = newFoto;
                db.SaveChanges();
            }
            UpdateModel.adSoyad = uye.adSoyad;
            UpdateModel.uyeDetayBilgiID = uye.uyeDetayBilgiID;
            UpdateModel.UyeDetay.kullaniciAdi = uye.UyeDetay.kullaniciAdi;
            UpdateModel.UyeDetay.eMail = uye.UyeDetay.eMail;
            UpdateModel.UyeDetay.sifre = uye.UyeDetay.sifre;

            db.SaveChanges();
            return View("UserProfile",UpdateModel);
        }


    }
}