using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalWebSite.Models;

namespace PersonalWebSite.Controllers
{
    public class WorksController : Controller
    {
        Models.PersonalWebPageDBEntities db = new Models.PersonalWebPageDBEntities();
        public ActionResult Index()
        {
            var model = db.Proje.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult YeniProje()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniProje(Proje p)
        {
            db.Proje.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}