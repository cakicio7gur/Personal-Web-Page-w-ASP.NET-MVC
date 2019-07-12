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
        Models.PersonalWebPageDBEntities1 db = new Models.PersonalWebPageDBEntities1();
        [HttpGet]
        public ActionResult KategoriList()
        {
            var model = db.Kategori.ToList();
            return View(model);
        }
    }
}