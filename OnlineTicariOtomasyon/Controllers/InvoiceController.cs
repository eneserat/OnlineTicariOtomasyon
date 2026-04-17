using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;
namespace OnlineTicariOtomasyon.Controllers
{
    public class InvoiceController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
            var liste = c.Invoices.ToList();
            return View(liste);
        }
        [HttpGet]
        public ActionResult InvoiceAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult InvoiceAdd(Invoice invoice)
        {
            if (!ModelState.IsValid)
            {
                return View(invoice);
            }

            invoice.InvoiceDate = DateTime.Now;

            c.Invoices.Add(invoice);
            c.SaveChanges();

            TempData["Success"] = "Fatura Sisteme Başarıyla Eklendi! :) ";
            return RedirectToAction("Index");
        }

    }
}