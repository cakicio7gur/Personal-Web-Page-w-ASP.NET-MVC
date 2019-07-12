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
        Models.PersonalWebPageDBEntities1 db = new Models.PersonalWebPageDBEntities1();
        
        public ActionResult Index()
        {
            var model = db.Makale.ToList();
            return View(model);
        }


        // FOR User
        public ActionResult Users()
        {
            var model = db.Uye.ToList();
            return View(model);
        }
        public ActionResult RemoveUser(int id)
        {
            var RemoveUser = db.Uye.Find(id);
            var RemoveUserDetail = db.UyeDetay.Find(RemoveUser.uyeDetayBilgiID);
            if ((RemoveUser != null) && (RemoveUserDetail != null))
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





        // FOR Works
        public ActionResult Works()
        {
            return View();
        }
        public ActionResult WorksList()
        {
            var model = db.Proje.ToList();
            return View(model);
        }
        public ActionResult GetWorkDetailByID(int id)
        {
            var UpdateModel = db.Proje.Find(id);
            return View("Works", UpdateModel);
        }

        public ActionResult UpdateAndAddWorks(Proje proje)
        {
                if (proje.projeID == 0)// YENİ KAYIT EKLENECEĞİNDE ÇALIŞIR
                {
                    db.Proje.Add(proje);
                }
                else
                {
                    var Eskimodel = db.Proje.Find(proje.projeID);
                    Eskimodel.projeBaslik = proje.projeBaslik;
                    Eskimodel.projeLink = proje.projeLink;
                }
                db.SaveChanges();
                return RedirectToAction("Works");
        }
        public ActionResult RemoveProject(int id)
        {
            var RemoveProject = db.Proje.Find(id);
            if (RemoveProject != null)
            {
                db.Proje.Remove(RemoveProject);
                db.SaveChanges();
                return RedirectToAction("WorksList");
            }
            else
            {
                ViewBag.ProjeHataMesajı = "Proje Silinemedi!";
                return RedirectToAction("WorksList");
            }

        }



        // FOR Blog
        public ActionResult AddBlog()
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
        public ActionResult BlogsList()
        {
            var model = db.Makale.ToList();
            return View(model);
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
            return View("AddBlog", UpdateModel);

        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult UpdateAndAddBlog(Makale makale)
        {
            var ktgr = db.Kategori.Where(m => m.kategoriID == makale.Kategori.kategoriID).FirstOrDefault();
            if (makale.makaleID == 0)// YENİ KAYIT EKLENECEĞİNDE ÇALIŞIR
            {
                makale.Kategori = ktgr;
                makale.MakaleDetay.yayınlanmaTarihi = DateTime.Now;
                makale.MakaleDetay.goruntulenmeSayisi = 0;
                makale.MakaleDetay.fotograf = "blogger.jpg";
                db.Makale.Add(makale);
            }
            else// GÜNCELLEME İŞLEMİ YAPILACAĞINDA ÇALIŞIR
            {
                var Eskimodel = db.Makale.Find(makale.makaleID);
                Eskimodel.MakaleDetay.baslik = makale.MakaleDetay.baslik;
                Eskimodel.MakaleDetay.icerik = makale.MakaleDetay.icerik;
                Eskimodel.MakaleDetay.yayınlanmaTarihi = makale.MakaleDetay.yayınlanmaTarihi;
                Eskimodel.MakaleDetay.goruntulenmeSayisi = makale.MakaleDetay.goruntulenmeSayisi;
                Eskimodel.kategoriID = ktgr.kategoriID;
            }
            db.SaveChanges();
            return RedirectToAction("BlogsList");
        }
        public ActionResult RemoveBlog(int id)
        {
            var RemoveBlog = db.Makale.Find(id);
            var RemoveBlogDetail = db.MakaleDetay.Find(RemoveBlog.makaleDetayID);
            if (RemoveBlog != null && RemoveBlogDetail!=null)
            {
                foreach (var yorum in RemoveBlog.Yorum.ToList())
                {
                    db.Yorum.Remove(yorum);
                }
                db.Makale.Remove(RemoveBlog);
                db.MakaleDetay.Remove(RemoveBlogDetail);
                db.SaveChanges();
                return RedirectToAction("BlogsList");
            }
            else
            {
                ViewBag.blogHataMesajı = "Makale Silinemedi!";
                return RedirectToAction("BlogsList");
            }
        }





         // FOR Comment
        public ActionResult CommentsList()
        {
            var model = db.Yorum.ToList();
            return View(model);
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