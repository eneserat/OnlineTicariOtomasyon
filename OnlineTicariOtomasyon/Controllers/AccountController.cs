using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class AccountController : Controller
    {
       Context c = new Context();

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string Username, string Password, string Authority)
        {
            if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(Authority))
            {
                TempData["Error"] = "Lütfen tüm alanları doldurun!";
                return View();
            }

            var admin = c.Admins.FirstOrDefault(a =>
                a.UserName == Username &&
                a.Password == Password &&
                a.Authority == Authority);

            if (admin != null)
            {
               
                Session["AdminID"] = admin.AdminID;
                Session["AdminName"] = admin.UserName;
                Session["AdminAuthority"] = admin.Authority;

                TempData["Success"] = $"Hoşgeldiniz, {admin.UserName}!";
                return RedirectToAction("Index", "Category"); 
            }

            TempData["Error"] = "Kullanıcı adı, şifre veya yetki hatalı!";
            return View();
        }

        
        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }
    }
}