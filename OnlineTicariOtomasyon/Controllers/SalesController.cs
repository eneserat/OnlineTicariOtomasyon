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

        [HttpPost]
        public ActionResult SalesAdd(SalesTransaction sale)
        {
           
            ViewBag.Products = new SelectList(c.Products.Where(p => p.Status == true).ToList(), "ProductID", "ProductName");
            ViewBag.Currents = new SelectList(c.Currents.ToList(), "CurrentID", "CurrentName");
            ViewBag.Employees = new SelectList(c.Employees.ToList(), "EmployeeID", "EmployeeName");

            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen tüm alanları doğru şekilde doldurun!";
                return View(sale);
            }

            var product = c.Products.Find(sale.ProductID);
            if (product == null)
            {
                TempData["Error"] = "Ürün bulunamadı!";
                return View(sale);
            }

           
            sale.SalesTransactionsPrice = product.SellingPrice; 
            sale.SalesTransactionsTotalAmount = sale.SalesTransactionsTotalAmount; 
            sale.SalesTransactionsDate = DateTime.Now;

            c.SalesTransactions.Add(sale);
            c.SaveChanges();

            TempData["Success"] = "Satış hareketi başarıyla eklendi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SalesEdit(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Satış ID boş olamaz!";
                return RedirectToAction("Index");
            }

            var sale = c.SalesTransactions.Find(id);
            if (sale == null)
            {
                TempData["Error"] = "Satış hareketi bulunamadı!";
                return RedirectToAction("Index");
            }

            ViewBag.Products = new SelectList(c.Products.Where(p => p.Status).ToList(), "ProductID", "ProductName", sale.ProductID);
            ViewBag.Currents = new SelectList(c.Currents.ToList(), "CurrentID", "CurrentName", sale.CurrentID);
            ViewBag.Employees = new SelectList(c.Employees.ToList(), "EmployeeID", "EmployeeName", sale.EmployeeID);

            return View(sale);
        }

        [HttpPost]
        public ActionResult SalesEdit(SalesTransaction sale)
        {
            var existingSale = c.SalesTransactions.Find(sale.SalesTransactionsID);
            if (existingSale == null)
            {
                TempData["Error"] = "Satış hareketi bulunamadı!";
                return RedirectToAction("Index");
            }

            var product = c.Products.Find(sale.ProductID);
            if (product == null)
            {
                TempData["Error"] = "Seçilen ürün bulunamadı!";
                ViewBag.Products = new SelectList(c.Products.Where(p => p.Status).ToList(), "ProductID", "ProductName", sale.ProductID);
                ViewBag.Currents = new SelectList(c.Currents.ToList(), "CurrentID", "CurrentName", sale.CurrentID);
                ViewBag.Employees = new SelectList(c.Employees.ToList(), "EmployeeID", "EmployeeName", sale.EmployeeID);
                return View(sale);
            }

            existingSale.ProductID = sale.ProductID;
            existingSale.CurrentID = sale.CurrentID;
            existingSale.EmployeeID = sale.EmployeeID;
            existingSale.SalesTransactionsTotalAmount = sale.SalesTransactionsTotalAmount;
            existingSale.SalesTransactionsPrice = product.SellingPrice;
            existingSale.SalesTransactionsDate = DateTime.Now;

            c.SaveChanges();

            TempData["Success"] = "Satış hareketi başarıyla güncellendi!";
            TempData["UpdatedSaleID"] = existingSale.SalesTransactionsID;

            return RedirectToAction("Index");
        }
    }
}


