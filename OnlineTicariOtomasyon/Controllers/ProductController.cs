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
        public ActionResult Index(string p)
        {
            var urunler = from x in c.Products select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(x => x.ProductName.Contains(p));

            }
            return View(urunler.ToList());
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
        [HttpGet]
        public ActionResult ProductEdit(int id)
        {
          
            var product = c.Products.Include("Category").FirstOrDefault(x => x.ProductID == id);
            if (product == null)
            {
                return HttpNotFound();
            }

            
            ViewBag.Categories = new SelectList(c.Categories.ToList(), "CategoryID", "CategoryName", product.CategoryID);

            return View(product);
        }
        [HttpPost]
        public ActionResult ProductEdit(Product product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = c.Products.Find(product.ProductID);
                if (existingProduct == null)
                {
                    return HttpNotFound();
                }

               
                existingProduct.ProductName = product.ProductName;
                existingProduct.ProductBrand = product.ProductBrand;
                existingProduct.ProductStock = product.ProductStock;
                existingProduct.PurchasePrice = product.PurchasePrice;
                existingProduct.SellingPrice = product.SellingPrice;
                existingProduct.ProductImage = product.ProductImage;
                existingProduct.CategoryID = product.CategoryID;

                c.SaveChanges();

                TempData["Success"] = "Ürün başarıyla güncellendi!";
                return RedirectToAction("Index");
            }

           
            ViewBag.Categories = new SelectList(c.Categories.ToList(), "CategoryID", "CategoryName", product.CategoryID);

            return View(product);
        }


    }
}
