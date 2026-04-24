using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class LoginController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Partial1()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult Partial1(Current p)
        {
            if (p != null)
            {
                c.Currents.Add(p);
                c.SaveChanges();
            }

            return PartialView();
        }
        [HttpGet]
        public ActionResult CurrentLogin1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CurrentLogin1(Current p)
        {

            var bilgiler = c.Currents.FirstOrDefault(x =>
                x.CurrentMail == p.CurrentMail &&
                x.CurrentPassword == p.CurrentPassword);

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.CurrentMail, false);
                Session["CurrentMail"] = bilgiler.CurrentMail.ToString()    ;
                return RedirectToAction ("Index","CurrentPanel"); 
            }
            else
            {
               
                return RedirectToAction("Index","Login");
            }
            

        }
        [HttpGet]
        public ActionResult AdminLogin()
        {
            return View();
        }
        public ActionResult AdminLogin(Admin p)
        {
            var bilgiler = c.Admins.FirstOrDefault(x =>
                x.UserName == p.UserName &&
                x.Password == p.Password);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.UserName, false);
                Session["UserName"] = bilgiler.UserName.ToString();
                return RedirectToAction("Index", "Category");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
}