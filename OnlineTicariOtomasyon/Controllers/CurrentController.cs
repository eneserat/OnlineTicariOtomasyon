using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class CurrentController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {

            var liste = c.Currents.ToList();
            return View(liste);

        }
        public ActionResult Logs()
        {
            var values = c.CurrentLogs.OrderByDescending(x => x.ActionDate).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult CurrentAdd()
        { 
            return View();
        }


        [HttpPost]
        public ActionResult CurrentAdd(Current current)
        {
            if (!ModelState.IsValid)
            {
                return View(current);
            }

            c.Currents.Add(current);
            c.SaveChanges();

            TempData["Success"] = "Cari Sisteme Başarıyla Eklendi :)";
            return RedirectToAction("Index");
        }
        public ActionResult CurrentEdit(int id)
        {
            var value = c.Currents.Find(id);

            if (value == null)
            {
                TempData["Error"] = "Cari bulunamadı!";
                return RedirectToAction("Index");
            }

            return View(value);
        }

        [HttpPost]
        public ActionResult CurrentEdit(Current current)
        {
            if (!ModelState.IsValid)
            {
                return View(current);
            }

            var value = c.Currents.Find(current.CurrentID);

            if (value == null)
            {
                TempData["Error"] = "Cari bulunamadı!";
                return RedirectToAction("Index");
            }

            value.CurrentName = current.CurrentName;
            value.CurrentSurname = current.CurrentSurname;
            value.CurrentCity = current.CurrentCity;
            value.CurrentMail = current.CurrentMail;

            c.SaveChanges();
            CurrentLog log = new CurrentLog();

            log.CurrentID = current.CurrentID;
            log.ActionType = "Güncelleme";
            log.ActionDate = DateTime.Now;


            string user = Session["erateeness"] as string;

            log.EmployeeName = string.IsNullOrEmpty(user)
                ? "Bilinmeyen Kullanıcı"
                : user;

            log.Description = current.CurrentName + " güncellendi";

            c.CurrentLogs.Add(log);
            c.SaveChanges();

            TempData["Success"] = "Cari Başarıyla Güncellendi + Log Kaydedildi!";
            return RedirectToAction("Index");

        }

      
        public ActionResult CurrentDelete(int id)
        {
            var value = c.Currents.Find(id);

            if (value != null)
            {
                c.Currents.Remove(value);
                c.SaveChanges();

                TempData["Success"] = "Cari silindi!";
            }

            return RedirectToAction("Index");
        }

        public ActionResult CurrentSales(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }

            
            var debug = c.SalesTransactions
                         .Select(x => new { x.SalesTransactionsID, x.CurrentID })
                         .ToList();

            var values = (from s in c.SalesTransactions
                          join p in c.Products on s.ProductID equals p.ProductID
                          join e in c.Employees on s.EmployeeID equals e.EmployeeID
                          where s.CurrentID == id
                          select new CurrentSalesViewModel
                          {
                              SalesID = s.SalesTransactionsID,
                              Product = p.ProductName,
                              Employee = e.EmployeeName,
                              Price = s.SalesTransactionsPrice,
                              Quantity = 1,
                              Total = s.SalesTransactionsTotalAmount,
                              Date = s.SalesTransactionsDate
                          }).ToList();

            return View(values);
        }
        public ActionResult CreateInvoice(int id)
        {
            var sale = c.SalesTransactions.Find(id);

            if (sale == null)
            {
                TempData["Error"] = "Satış bulunamadı!";
                return RedirectToAction("Index");
            }

           
            TempData["Success"] = "Fatura oluşturma işlemi tetiklendi!";

            return RedirectToAction("Index");
        }
        public ActionResult Reports()
        {
            var values = (from s in c.SalesTransactions
                          join cr in c.Currents on s.CurrentID equals cr.CurrentID
                          select new CurrentReportViewModel
                          {
                              CurrentID = cr.CurrentID,
                              CurrentName = cr.CurrentName,
                              Total = s.SalesTransactionsTotalAmount,
                              Date = s.SalesTransactionsDate
                          }).ToList();

            return View(values);
        }
    }


}


