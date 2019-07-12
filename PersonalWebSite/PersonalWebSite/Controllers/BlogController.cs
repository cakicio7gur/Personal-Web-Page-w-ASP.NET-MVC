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
        Models.PersonalWebPageDBEntities1 db = new Models.PersonalWebPageDBEntities1();
        public ActionResult Index()
        {
            var model = db.Makale.ToList();
            return View(model);
        }
        public ActionResult GetMakaleById(int id)
        {
            var yorumlar = db.Yorum.Where(m => m.makaleID == id).ToList();
            ViewBag.makaleyeAitYorumlar = yorumlar;
            var yorumSayisi = db.Yorum.Where(m => m.makaleID == id).Count();
            ViewBag.makaleyeAitYorumSayisi = yorumSayisi;
            var model = db.Makale.Find(id);
            model.MakaleDetay.goruntulenmeSayisi++;
            db.SaveChanges();
            return View(model);
        }
        public ActionResult GetBlogBySearch(string search)
        {
            List<Models.Makale> makaleList = new List<Models.Makale>();
            foreach (var makale in db.Makale.ToList())
            {
                if ((makale.MakaleDetay.baslik.ToLower().Contains(search.ToLower())|| makale.MakaleDetay.icerik.ToLower().Contains(search.ToLower())
                    || makale.Kategori.kategori1.ToLower().Contains(search.ToLower())) == true)
                {
                    makaleList.Add(makale);
                }
            }
            return View("Index", makaleList);
        }
        public ActionResult GetBlogByKategori(int id)
        {
            List<Models.Makale> makaleList = new List<Models.Makale>();
            foreach (var makale in db.Makale.ToList())
            {
                if(makale.kategoriID== id)
                {
                    makaleList.Add(makale);
                }
            }
            return View("Index", makaleList);
        }
        public int GetBlogCountAsKategori(int id)
        {
            var makaleSayisi = db.Makale.Where(x => x.kategoriID == id).Count();
            return makaleSayisi;
        }
    }
}