using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class ProductController : Controller
    {
       Context c = new Context();
        public ActionResult Index()
        {
            var urunler = c.Products.ToList();
            return View(urunler);
        }
       
        [HttpGet]
        public ActionResult ProductAdd()
        {
          
            ViewBag.Categories = new SelectList(c.Categories.ToList(), "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult ProductAdd(Product product)
        {
            c.Products.Add(product);
            c.SaveChanges();

            TempData["Success"] = "Ürün başarıyla eklendi!";
            return RedirectToAction("Index");
        }
    }
}
