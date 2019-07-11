using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalWebSite.Models;

namespace PersonalWebSite.Controllers
{
    public class YorumController : Controller
    {
        Models.PersonalWebPageDBEntities db = new Models.PersonalWebPageDBEntities();
        public ActionResult Index()
        {
            var model = db.Yorum.ToList();
            return View(model);
        }
        public ActionResult AddComment(string isim,string yorum,int makaleID)
        {
            Models.Yorum model = new Models.Yorum();
            model.adSoyad = isim;
            model.icerik = yorum;
            model.makaleID = makaleID;
            model.tarih = DateTime.Now;
            db.Yorum.Add(model);
            db.SaveChanges();
            return RedirectToAction("GetMakaleById", "Blog", new { id = makaleID });
        }
    }
}