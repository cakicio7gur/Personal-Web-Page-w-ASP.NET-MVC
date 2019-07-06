using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalWebSite.Models;

namespace PersonalWebSite.Controllers
{
    public class KategoriController : Controller
    {
        Models.PersonalWebPageDBEntities db = new Models.PersonalWebPageDBEntities();
        [HttpGet]
        public ActionResult KategoriList()
        {
            var model = db.Kategori.ToList();
            return View(model);
        }

        public ActionResult CountBlogByKategori(int kategoriID)
        {
            var makaleSayisi = db.Makale.Where(m => m.kategoriID == kategoriID).Count();
            return View();
        }
    }
}