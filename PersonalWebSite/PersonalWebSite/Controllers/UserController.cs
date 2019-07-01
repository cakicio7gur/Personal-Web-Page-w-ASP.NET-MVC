using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace PersonalWebSite.Controllers
{
    public class UserController : Controller
    {
        Models.PersonalWebPageDBEntities db = new Models.PersonalWebPageDBEntities();
        [HttpGet]
        public ActionResult UserList()
        {
            var model = db.Uye.ToList();
            return View(model);
        }

        /*public ActionResult NewUser()
        {
            return View(new Models.Uye());
        }

        public ActionResult RegistrationNewUser(Models.Uye model,HttpPostedFileBase foto)
        {
            if(ModelState.IsValid)
            {
                if(model.uyeID==0)
                {
                    if(foto!=null)
                    {
                        WebImage Img = new WebImage(foto.InputStream);
                        FileInfo fotoinfo = new FileInfo(foto.FileName);
                        string newFoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                        Img.Resize(60, 60);
                        Img.Save("~/Upload/UyeFoto/" + newFoto);

                        model.UyeDetay.fotograf = "~/Upload/UyeFoto/" + newFoto;
                        model.rolID = 2;
                        db.Uye.Add(model);
                        db.SaveChanges();
                    }
                    else
                    {
                        return RedirectToAction("Index", "Uye");
                    }
                }
                else
                {
                    WebImage Img = new WebImage(foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(foto.FileName);
                    string newFoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    Img.Resize(60, 60);
                    Img.Save("~/Upload/UyeFoto/" + newFoto);

                    var eskimodel = db.Uye.Find(model.uyeID);
                    eskimodel.rolID = model.rolID;
                    eskimodel.UyeDetay.kullaniciAdi = model.UyeDetay.kullaniciAdi;
                    eskimodel.UyeDetay.fotograf = "~/Upload/UyeFoto/" + newFoto;
                    eskimodel.UyeDetay.eMail = model.UyeDetay.eMail;
                    eskimodel.adSoyad = model.adSoyad;
                    eskimodel.UyeDetay.sifre= model.UyeDetay.sifre;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
        }
            else
            {
                return View("YeniUye");
            }*/

        }
}