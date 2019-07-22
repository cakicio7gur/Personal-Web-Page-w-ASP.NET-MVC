using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using PersonalWebSite.Models;

namespace PersonalWebSite.Controllers
{
    public class UserController : Controller
    {
        PersonalWebPageDBEntities1 db = new PersonalWebPageDBEntities1();

        public ActionResult Index()
        {
            var model = db.Uye.ToList();
            return View(model);
        }

        [HttpGet]
        public ActionResult YeniUye()
        {
            return View(new Uye());
        }

        [HttpPost]
        public ActionResult YeniUye(Uye uye)
        {
            string gecicisifre = "";
            if (!ModelState.IsValid)
            {
                return RedirectToAction("YeniUye");
            }
            else
            {
                var kullaniciEmailKontrol = db.Uye.FirstOrDefault(m => m.UyeDetay.eMail == uye.UyeDetay.eMail);
                if (kullaniciEmailKontrol == null)
                {
                    var kullaniciAdiKontrol = db.Uye.FirstOrDefault(m => m.UyeDetay.kullaniciAdi == uye.UyeDetay.kullaniciAdi);
                    if (kullaniciAdiKontrol == null)
                    {
                        gecicisifre = Encrypt(uye.UyeDetay.sifre);
                        uye.UyeDetay.sifre = gecicisifre;
                        uye.rolID = 2;
                        uye.UyeDetay.fotograf = "user.png";
                        db.Uye.Add(uye);
                        db.SaveChanges();
                        TempData["kayitMesaji"] = "Kayıt Başarılı!";
                        return RedirectToAction("YeniUye");
                    }
                    else
                    {
                        TempData["Message"] = "Bu Kullanıcı Adı Kullanılıyor !";
                        return RedirectToAction("YeniUye");
                    }
                }
                else
                {
                    TempData["Message"] = "Bu E-mail Kullanılıyor !";
                    return RedirectToAction("YeniUye");
                }
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
        private string Encrypt(string clearText)
        { 
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }
    }
}