using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class AdminController : Controller
    {
       Context c = new Context();
        public ActionResult Index()
        {
            var adminler = c.Admins.ToList();
            return View(adminler);
        }
        // GET: Admin Ekle
        [HttpGet]
        public ActionResult AdminAdd()
        {
            return View();
        }

        // POST: Admin Ekle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminAdd(Admin admin)
        {
            try
            {
                if (string.IsNullOrEmpty(admin.UserName) || string.IsNullOrEmpty(admin.Password) || string.IsNullOrEmpty(admin.Authority))
                {
                    TempData["Error"] = "Lütfen tüm alanları doğru şekilde doldurun!";
                    return View(admin);
                }

                c.Admins.Add(admin);
                c.SaveChanges();

                TempData["Success"] = "Admin Sisteme Başarıyla Eklendi!";
                return RedirectToAction("AdminAdd");
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException ex)
            {
                var errorMessages = ex.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.PropertyName + ": " + x.ErrorMessage);

                TempData["Error"] = "Validation Hatası: " + string.Join("; ", errorMessages);
                return View(admin);
            }
        
        }

     
        [HttpGet]
        public ActionResult AdminEdit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Admin seçilmedi!";
                return RedirectToAction("Index");
            }

            var admin = c.Admins.Find(id.Value);
            if (admin == null)
            {
                TempData["Error"] = "Admin bulunamadı!";
                return RedirectToAction("Index");
            }

            return View(admin);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminEdit(Admin admin)
        {
            if (ModelState.IsValid)
            {
                var existingAdmin = c.Admins.Find(admin.AdminID);
                if (existingAdmin == null)
                {
                    TempData["Error"] = "Admin bulunamadı!";
                    return RedirectToAction("Index");
                }

                existingAdmin.UserName = admin.UserName;
                existingAdmin.Password = admin.Password;
                existingAdmin.Authority = admin.Authority;
                c.SaveChanges();

                TempData["Success"] = "Admin başarıyla güncellendi!";
                return RedirectToAction("Index");
            }
            TempData["Error"] = "Admin güncellenirken bir hata oluştu!";
            return View(admin);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AdminDelete(int id)
        {
            var admin = c.Admins.Find(id);
            if (admin == null)
            {
                TempData["Error"] = "Admin bulunamadı!";
                return RedirectToAction("Index");
            }

            c.Admins.Remove(admin);
            c.SaveChanges();
            TempData["Success"] = "Admin başarıyla silindi!";
            return RedirectToAction("Index");
        }
    }
}

   