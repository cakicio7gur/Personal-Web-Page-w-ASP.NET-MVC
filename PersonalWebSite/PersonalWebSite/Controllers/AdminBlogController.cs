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
            var blog = db.Makale.Find(id);
            return View("BlogDetail", blog);
        }

        [HttpPost]
        public ActionResult UpdateBlog(Makale makale,HttpPostedFileBase fotograf)
        {
            var UpdateModel = db.Makale.Find(makale.makaleID);
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
            UpdateModel.kategoriID = makale.kategoriID;
            UpdateModel.makaleDetayID = makale.makaleDetayID;
            db.SaveChanges();
            return View("BlogDetail", UpdateModel);
        }

        [HttpGet]
        public ActionResult Blog()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Blog(Makale makale, HttpPostedFileBase fotograf)
        {
            var NewModel = db.Makale.Find(makale.makaleID);
            if (fotograf != null)
            {
                WebImage Img = new WebImage(fotograf.InputStream);
                FileInfo fotoinfo = new FileInfo(fotograf.FileName);
                string newFoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                Img.Resize(750, 350);
                Img.Save("~/Upload/WorkPhotos/" + newFoto);
                NewModel.MakaleDetay.fotograf = newFoto;
                db.SaveChanges();
            }
            NewModel.MakaleDetay.baslik = makale.MakaleDetay.baslik;
            NewModel.MakaleDetay.icerik = makale.MakaleDetay.icerik;
            NewModel.MakaleDetay.yayınlanmaTarihi = DateTime.Now;
            NewModel.MakaleDetay.goruntulenmeSayisi = 0;
            NewModel.kategoriID = makale.kategoriID;
            NewModel.makaleDetayID = makale.makaleDetayID;
            db.Makale.Add(NewModel);
            db.SaveChanges();
            return View("BlogDetail", NewModel);
        }
        /*public ActionResult UpdateBlog(int id)
        {
            var UpdateBlog = db.Makale.Find(id);
            return View("NewBlog", UpdateBlog);

        }

        public ActionResult RemoveBlog(int id)
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