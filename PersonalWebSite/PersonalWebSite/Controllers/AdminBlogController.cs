using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using PersonalWebSite.Models;



namespace PersonalWebSite.Controllers
{
    public class AdminBlogController : Controller
    {
        Models.PersonalWebPageDBEntities db = new Models.PersonalWebPageDBEntities();

        public ActionResult Index()
        {
            var model = db.Makale.ToList();
            return View(model);
        }
        
        public ActionResult BlogDetail(int id)
        {
            List<SelectListItem> kategorilerbd = (from i in db.Kategori.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = i.kategori1,
                                                    Value = i.kategoriID.ToString()
                                                }).ToList();
            ViewBag.ktgrbd = kategorilerbd;
            var blog = db.Makale.Find(id);
            return View("BlogDetail", blog);
        }

        [HttpPost]
        public ActionResult UpdateBlog(Makale makale,HttpPostedFileBase fotograf)
        {
            var UpdateModel = db.Makale.Find(makale.makaleID);
            var ktgr = db.Kategori.Where(m => m.kategoriID == makale.Kategori.kategoriID).FirstOrDefault();
            makale.kategoriID = ktgr.kategoriID;
            UpdateModel.kategoriID = makale.kategoriID;
            if (fotograf != null)
            {
                WebImage Img = new WebImage(fotograf.InputStream);
                FileInfo fotoinfo = new FileInfo(fotograf.FileName);
                string newFoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                Img.Resize(750,350);
                Img.Save("~/Upload/WorkPhotos/" + newFoto);
                UpdateModel.MakaleDetay.fotograf = newFoto;
                db.SaveChanges();
            }
            UpdateModel.MakaleDetay.baslik = makale.MakaleDetay.baslik;
            UpdateModel.MakaleDetay.icerik = makale.MakaleDetay.icerik;
            UpdateModel.MakaleDetay.yayınlanmaTarihi = makale.MakaleDetay.yayınlanmaTarihi;
            UpdateModel.MakaleDetay.goruntulenmeSayisi = makale.MakaleDetay.goruntulenmeSayisi;
            UpdateModel.makaleDetayID = makale.makaleDetayID;
            db.SaveChanges();
            return View("BlogDetail", UpdateModel);
        }

        [HttpGet]
        public ActionResult Blog()
        {
            List<SelectListItem> kategoriler = (from i in db.Kategori.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = i.kategori1,
                                                    Value = i.kategoriID.ToString()
                                                }).ToList();
            ViewBag.ktgr = kategoriler;
            return View();
        }

        [HttpPost]
        public ActionResult Blog(Makale makale, HttpPostedFileBase fotograf)
        {
            var ktgr = db.Kategori.Where(m => m.kategoriID == makale.Kategori.kategoriID).FirstOrDefault();
            makale.Kategori = ktgr;
            makale.MakaleDetay.yayınlanmaTarihi = DateTime.Now;
            makale.MakaleDetay.goruntulenmeSayisi = 0;
            if (fotograf != null)
            {
                WebImage Img = new WebImage(fotograf.InputStream);
                FileInfo fotoinfo = new FileInfo(fotograf.FileName);
                string newFoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                Img.Resize(750, 350);
                Img.Save("~/Upload/WorkPhotos/" + newFoto);
                makale.MakaleDetay.fotograf = newFoto;
                db.SaveChanges();
            }
            else
            {
                makale.MakaleDetay.fotograf = "blog.jpg";
                db.SaveChanges();
            }
            db.Makale.Add(makale);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        /*public ActionResult RemoveBlog(int id)
         {
             var RemoveBlog = db.Makale.Find(id);
             if (RemoveBlog != null)
             {
                 foreach (var yorum in RemoveBlog.Yorum.ToList())
                 {
                     db.Yorum.Remove(yorum);
                 }
                 db.Makale.Remove(RemoveBlog);
                 db.SaveChanges();
                 return RedirectToAction("Index");
             }

             else
             {
                 return RedirectToAction("Index");
             }
         }*/
    }
}