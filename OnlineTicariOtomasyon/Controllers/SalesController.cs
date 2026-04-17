using System;
using System.Linq;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class SalesController : Controller
    {
        Context c = new Context();

       
        public ActionResult Index()
        {
            var values = c.SalesTransactions.ToList();
            return View(values);
        }

        
        [HttpGet]
        public ActionResult SalesAdd()
        {
            ViewBag.Products = new SelectList(c.Products.Where(x => x.Status == true), "ProductID", "ProductName");
            ViewBag.Currents = new SelectList(c.Currents, "CurrentID", "CurrentName");
            ViewBag.Employees = new SelectList(c.Employees, "EmployeeID", "EmployeeName");

            return View();
        }

       
        [HttpPost]
        public ActionResult SalesAdd(SalesTransaction sale)
        {
            var product = c.Products.Find(sale.ProductID);

            sale.SalesTransactionsPrice = product.SellingPrice;
            sale.SalesTransactionsTotalAmount = sale.SalesTransactionsTotalAmount;
            sale.SalesTransactionsTotalAmount = product.SellingPrice * sale.SalesTransactionsTotalAmount;
            sale.SalesTransactionsDate = DateTime.Now;

            c.SalesTransactions.Add(sale);
            c.SaveChanges();

            TempData["Success"] = "Satış eklendi!";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult SalesEdit()
        {
            var values = c.SalesTransactions.ToList();
            return View(values);
        }

       
        [HttpPost]
        public ActionResult SalesEdit(SalesTransaction sale)
        {
            var value = c.SalesTransactions.Find(sale.SalesTransactionsID);

            if (value == null)
            {
                TempData["Error"] = "Kayıt bulunamadı!";
                return RedirectToAction("SalesEdit");
            }

            var product = c.Products.Find(sale.ProductID);

            value.ProductID = sale.ProductID;
            value.CurrentID = sale.CurrentID;
            value.EmployeeID = sale.EmployeeID;
            value.SalesTransactionsTotalAmount = sale.SalesTransactionsTotalAmount;

            value.SalesTransactionsPrice = product.SellingPrice;
            value.SalesTransactionsTotalAmount = product.SellingPrice * sale.SalesTransactionsTotalAmount;

            c.SaveChanges();

            TempData["Success"] = "Güncelleme başarılı!";
            return RedirectToAction("SalesEdit");
        }

    
        public ActionResult SalesDelete(int id)
        {
            var value = c.SalesTransactions.Find(id);

            if (value != null)
            {
                c.SalesTransactions.Remove(value);
                c.SaveChanges();
                TempData["Success"] = "Silme işlemi başarılı!";
            }

            return RedirectToAction("SalesEdit");
        }
    }
}