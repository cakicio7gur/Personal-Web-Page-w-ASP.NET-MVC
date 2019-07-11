using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Helpers;
using System.IO;
using PersonalWebSite.Models;


namespace PersonalWebSite.Controllers
{
    public class AdminController : Controller
    {
        Models.PersonalWebPageDBEntities db = new Models.PersonalWebPageDBEntities();
        public ActionResult Index()
        {
            var model = db.Makale.ToList();
            return View(model);
        }
        public ActionResult Users()
        {
            var model = db.Uye.ToList();
            return View(model);
        }
        [HttpGet]
        public ActionResult Works()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Works(Proje p)
        {
            db.Proje.Add(p);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult WorksList()
        {
            var model = db.Proje.ToList();
            return View(model);
        }
        public ActionResult NewBlog()
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
        public ActionResult GetBlogDetailByID(int id)
        {
            var UpdateModel = db.Makale.Find(id);
            List<SelectListItem> kategoriler = (from i in db.Kategori.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = i.kategori1,
                                                    Value = i.kategoriID.ToString()
                                                }).ToList();
            ViewBag.ktgr = kategoriler;
            return View("NewBlog", UpdateModel);

        }
        public ActionResult UpdateAndAddBlog(Makale makale, HttpPostedFileBase fotograf)
        {
            if (ModelState.IsValid)
            {
                if (makale.makaleID == 0)// YENİ KAYIT EKLENECEĞİNDE ÇALIŞIR
                {
                    if (fotograf != null)
                    {
                        WebImage Img = new WebImage(fotograf.InputStream);
                        FileInfo fotoinfo = new FileInfo(fotograf.FileName);
                        string newFoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                        Img.Resize(750, 350);
                        Img.Save("~/Upload/WorkPhotos/" + newFoto);
                        makale.MakaleDetay.fotograf = newFoto;
                        makale.MakaleDetay.yayınlanmaTarihi = DateTime.Now;
                        makale.MakaleDetay.goruntulenmeSayisi = 0;
                        db.Makale.Add(makale);
                    }
                    else
                    {
                        makale.MakaleDetay.fotograf = "blog.jpg";
                    }
                }
                else// GÜNCELLEME İŞLEMİ YAPILACAĞINDA ÇALIŞIR
                {
                    var Eskimodel = db.Makale.Find(makale.makaleID);
                    if (fotograf != null)
                    {
                        WebImage Img = new WebImage(fotograf.InputStream);
                        FileInfo fotoinfo = new FileInfo(fotograf.FileName);
                        string newFoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                        Img.Resize(600, 300);
                        Img.Save("~/Upload/WorkPhotos/" + newFoto);
                        Eskimodel.MakaleDetay.fotograf = newFoto;
                    }
                    else
                    {
                        Eskimodel.MakaleDetay.fotograf = "blog.jpg";
                    }
                    Eskimodel.MakaleDetay.baslik = makale.MakaleDetay.baslik;
                    Eskimodel.MakaleDetay.icerik = makale.MakaleDetay.icerik;
                    Eskimodel.MakaleDetay.yayınlanmaTarihi = makale.MakaleDetay.yayınlanmaTarihi;
                    Eskimodel.MakaleDetay.goruntulenmeSayisi = makale.MakaleDetay.goruntulenmeSayisi;
                    var ktgr = db.Kategori.Where(m => m.kategoriID == makale.Kategori.kategoriID).FirstOrDefault();
                    Eskimodel.kategoriID = ktgr.kategoriID;
                }
                db.SaveChanges();
                return RedirectToAction("NewBlog");
            }
            else
            {
                return View("BlogList");
            }
        }
        public ActionResult BlogsList()
        {
            var model = db.Makale.ToList();
            return View(model);
        }
        public ActionResult CommentsList()
        {
            var model = db.Yorum.ToList();
            return View(model);
        }
        public ActionResult RemoveUser(int id)
        {
            var RemoveUser = db.Uye.Find(id);
            var RemoveUserDetail = db.UyeDetay.Find(RemoveUser.uyeDetayBilgiID);
            if((RemoveUser !=null) && (RemoveUserDetail != null))
            {
                db.Uye.Remove(RemoveUser);
                db.UyeDetay.Remove(RemoveUserDetail);
                db.SaveChanges();
                return RedirectToAction("Users");
            }
            else
            {
                ViewBag.UserHataMesajı = "Kullanıcı Silinemedi!";
                return RedirectToAction("Users");
            }

        }
        public ActionResult RemoveProject(int id)
        {
            var RemoveProject = db.Proje.Find(id);
            if (RemoveProject!=null)
            {
                db.Proje.Remove(RemoveProject);
                db.SaveChanges();
                return RedirectToAction("WorksList");
            }
            else
            {
                ViewBag.ProjeHataMesajı = "Makale Silinemedi!";
                return RedirectToAction("WorksList");
            }

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
                return RedirectToAction("BlogsList");
            }
            else
            {
                ViewBag.blogHataMesajı = "Makale Silinemedi!";
                return RedirectToAction("BlogsList");
            }
        }
        public ActionResult RemoveComment(int id)
        {
            var RemoveComment = db.Yorum.Find(id);
            if (RemoveComment != null)
            {
                db.Yorum.Remove(RemoveComment);
                db.SaveChanges();
                return RedirectToAction("CommentsList");
            }
            else
            {
                ViewBag.commentHataMesajı = "Yorum Silinemedi!";
                return RedirectToAction("CommentsList");
            }
        }
    }
}