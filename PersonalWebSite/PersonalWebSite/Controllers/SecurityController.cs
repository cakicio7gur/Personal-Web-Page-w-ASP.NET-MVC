using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Web.Security;
using System.IO;
using System.Text;

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

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(Models.Uye model)
        {
            model.UyeDetay.sifre = Encrypt(model.UyeDetay.sifre);

            var user = db.Uye.FirstOrDefault(m => m.UyeDetay.kullaniciAdi == model.UyeDetay.kullaniciAdi && m.UyeDetay.sifre == model.UyeDetay.sifre);
            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.UyeDetay.kullaniciAdi, false);
                Session["Kullanici"] = user.UyeDetay.kullaniciAdi;
                Session["Oturum"] = "true";
                Session["Yetki"] = user.Rol.rol1;
                Session["UyeID"] = user.uyeID;   
                if (user.Rol.rol1 == "Admin")
                {
                    ViewBag.GirisBilgisi = "OK";
                    TempData["GirisBilgisi"] = "OK";
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ViewBag.GirisBilgisi = "OK";
                    TempData["GirisBilgisi"] = "OK";
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
            Session["UyeID"] = null;
            Session["Yetki"] = null;
            ViewBag.GirisBilgisi = null;
            TempData["GirisBilgisi"] = null;
            return RedirectToAction("Index", "Home");
        }

        private string Encrypt(string clearText)
        {  // kullanıcı şifresinin kodlanarak şifrelendiği metotdur.
            // şifreyi kodlanmış  şekilde geri döndürür.
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