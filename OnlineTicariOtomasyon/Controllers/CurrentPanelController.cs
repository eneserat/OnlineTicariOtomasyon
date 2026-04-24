using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class CurrentPanelController : Controller
    {
        // GET: CurrentPanel
        Context c = new Context();

        [Authorize]

        public ActionResult Index()
        {
            var mail = (string)Session["CurrentMail"];

            var degerler = c.Currents.FirstOrDefault(x => x.CurrentMail == mail);

            ViewBag.m = mail;

            return View(degerler);
        }
        public ActionResult Orders()
        {
            var mail = (string)Session["CurrentMail"];
            var id = c.Currents.Where(x => x.CurrentMail == mail).Select(y => y.CurrentID).FirstOrDefault();
            var degerler = c.SalesTransactions.Where(x => x.CurrentID == id).ToList();


            return View(degerler);
        }
    }
}