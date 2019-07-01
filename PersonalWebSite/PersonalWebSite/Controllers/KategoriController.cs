using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}