using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;
namespace OnlineTicariOtomasyon.Controllers
{
    public class CategoryController : Controller
    {
        Context c = new Context();

        public ActionResult Index()
        {
            var degerler = c.Categories.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CategoryAdd(Category category)
        {
            c.Categories.Add(category);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
       
        public ActionResult CategoryRemove(int id )
        {
            var ctg = c.Categories.Find(id);
            c.Categories.Remove(ctg);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}