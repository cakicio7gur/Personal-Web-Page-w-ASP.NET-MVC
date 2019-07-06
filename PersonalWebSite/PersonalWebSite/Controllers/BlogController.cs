using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalWebSite.Models;

namespace PersonalWebSite.Controllers
{
    public class BlogController : Controller
    {
        Models.PersonalWebPageDBEntities db = new Models.PersonalWebPageDBEntities();

        public ActionResult Index()
        {
            var model = db.Makale.ToList();
            return View(model);
        }
        public ActionResult GetMakaleById(int id)
        {
            var model = db.Makale.Find(id);
            model.MakaleDetay.goruntulenmeSayisi++;
            db.SaveChanges();
            return View(model);
        }
        public ActionResult GetMakaleByKategori(int kategoriID)
        {
            var blog = db.Makale.Where(x => x.kategoriID == kategoriID).ToList();
            return View("Index", blog);
        }

    }
}