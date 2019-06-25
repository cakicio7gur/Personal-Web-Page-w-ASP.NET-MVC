using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonalWebSite.Controllers
{
    public class DiscoverController : Controller
    {
        // GET: Discover
        public ActionResult Culture()
        {
            return View();
        }
        public ActionResult Motivation()
        {
            return View();
        }
    }
}