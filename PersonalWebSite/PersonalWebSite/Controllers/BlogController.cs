using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalWebSite.Models;
using PagedList;
using PagedList.Mvc;
namespace PersonalWebSite.Controllers
{
    public class BlogController : Controller
    {
        Models.PersonalWebPageDBEntities db = new Models.PersonalWebPageDBEntities();
        PersonalWebPageDBEntities Veri = new PersonalWebPageDBEntities();

        public ActionResult Index(int? SayfaNo)
        {
            int _sayfaNo = SayfaNo ?? 1;
            var MakaleListesi = Veri.Makale.OrderByDescending(m =>m.makaleID).ToPagedList<Makale>(_sayfaNo, 3);
            return View(MakaleListesi);
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
        public ActionResult GetMakaleByTarget(string target = null)
        {
            List<Models.Makale> Makale = new List<Models.Makale>();
            foreach (var makale in db.Makale.ToList())
            {
                foreach (var icerik in makale.MakaleDetay.icerik.ToList())
                {
                    if (icerik.ToString().Contains(target)==true)
                    {
                        Makale.Add(makale);
                    }
                }
            }
            return RedirectToAction("Index", Makale);
        }

    }
}