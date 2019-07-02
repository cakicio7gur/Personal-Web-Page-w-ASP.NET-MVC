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

        public ActionResult BlogDetail()
        {
            return View();
        }
        public ActionResult Index()
        {
            var model = db.Makale.ToList();
            return View(model);

        }


    }
}