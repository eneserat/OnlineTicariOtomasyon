using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class SalesController : Controller
    {
        Context c = new Context();  
        public ActionResult Index()
        {
            var Sales = c.SalesTransactions.ToList();
            return View(Sales);
        }
        [HttpGet]
        public ActionResult SalesAdd()
        {
            ViewBag.Products = new SelectList(c.Products.Where(p => p.Status == true).ToList(), "ProductID", "ProductName");
            ViewBag.Currents = new SelectList(c.Currents.ToList(), "CurrentID", "CurrentName");
            ViewBag.Employees = new SelectList(c.Employees.ToList(), "EmployeeID", "EmployeeName");
            return View();
        }

        // Satış ekleme - POST
        [HttpPost]
        public ActionResult SalesAdd(SalesTransaction sale)
        {
            if (ModelState.IsValid)
            {
                var product = c.Products.Find(sale.ProductID);
                if (product == null)
                {
                    TempData["Error"] = "Ürün bulunamadı!";
                    return RedirectToAction("SalesAdd");
                }

                sale.SalesTransactionsPrice = product.SellingPrice;
                sale.SalesTransactionsTotalAmount = product.SellingPrice * sale.SalesTransactionsTotalAmount;
                sale.SalesTransactionsDate = DateTime.Now;

                c.SalesTransactions.Add(sale);
                c.SaveChanges();

                TempData["Success"] = "Satış hareketi başarıyla eklendi!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Lütfen tüm alanları doğru şekilde doldurun!";
            return RedirectToAction("SalesAdd");
        }
    }
}

