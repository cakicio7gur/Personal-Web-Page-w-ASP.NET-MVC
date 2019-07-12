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
        Models.PersonalWebPageDBEntities1 db = new Models.PersonalWebPageDBEntities1();
        public ActionResult Index()
        {
            var model = db.Proje.ToList();
            return View(model);
        }
    }
}