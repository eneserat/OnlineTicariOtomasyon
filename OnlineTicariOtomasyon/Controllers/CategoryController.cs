using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;
using PagedList;
using PagedList.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class CategoryController : Controller
    {
        Context c = new Context();

       
        public ActionResult Index(int page=1)
        {
            ViewBag.Title = "Kategori Listesi";
            var degerler = c.Categories.OrderBy(x => x.CategoryID).ToPagedList(page, 10);
            return View(degerler);
        }

        
        [HttpGet]
        public ActionResult CategoryAdd()
        {
            ViewBag.Title = "Kategori Ekleme";
            return View();
        }

      
        [HttpPost]
        public ActionResult CategoryAdd(Category category)
        {
            if (ModelState.IsValid)
            {
                c.Categories.Add(category);
                c.SaveChanges();

                TempData["Success"] = "Kategori başarıyla eklendi!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Kategori eklenirken bir hata oluştu!";
            return View(category);
        }

       
        public ActionResult CategoryRemove(int id)
        {
            var ctg = c.Categories.Find(id);
            if (ctg == null)
            {
                TempData["Error"] = "Kategori bulunamadı!";
                return RedirectToAction("Index");
            }

            c.Categories.Remove(ctg);
            c.SaveChanges();

            TempData["Success"] = "Kategori başarıyla silindi!";
            return RedirectToAction("Index");
        }

     
        public ActionResult CategoryGet(int id)
        {
            var ctg = c.Categories.Find(id);
            if (ctg == null)
            {
                TempData["Error"] = "Kategori bulunamadı!";
                return RedirectToAction("Index");
            }

            ViewBag.Title = "Kategori Güncelleme";
            return View("CategoryGet", ctg);
        }

       
        [HttpPost]
        public ActionResult CategoryUpdate(Category category)
        {
            var ctg = c.Categories.Find(category.CategoryID);
            if (ctg == null)
            {
                TempData["Error"] = "Kategori bulunamadı!";
                return RedirectToAction("Index");
            }

            ctg.CategoryName = category.CategoryName;
            c.SaveChanges();

            TempData["Success"] = "Kategori başarıyla güncellendi!";
            return RedirectToAction("Index");
        }
    }
}