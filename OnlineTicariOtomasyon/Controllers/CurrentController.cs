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

            TempData["Success"] = "Cari başarıyla güncellendi!";
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
    }

}


