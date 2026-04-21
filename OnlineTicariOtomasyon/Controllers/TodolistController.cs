using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes; 

namespace OnlineTicariOtomasyon.Controllers
{
    public class TodolistController : Controller
    {
        Context c = new Context();  
        public ActionResult Index()
        {
            var todolists = c.ToDoLists.ToList();
            return View(todolists); 
        }

        [HttpPost]
        public ActionResult Add(ToDoList p)
        {
            p.TodolistSituation = false;
            c.ToDoLists.Add(p);
            c.SaveChanges();

            TempData["Message"] = "Görev Başarıyla Eklendi ✔";
            TempData["Type"] = "success";

            return RedirectToAction("Index");
        }

      
        public ActionResult Complete(int id)
        {
            var task = c.ToDoLists.Find(id);

            task.TodolistSituation = true;
            c.SaveChanges();

            TempData["Message"] = "Görev Tamamlandı ✔";
            TempData["Type"] = "info";

            return RedirectToAction("Index");
        }

      
        public ActionResult Delete(int id)
        {
            var task = c.ToDoLists.Find(id);
            c.ToDoLists.Remove(task);
            c.SaveChanges();

            TempData["Message"] = "Görev Başarıyla Silindi ❌";
            TempData["Type"] = "danger";

            return RedirectToAction("Index");
        }
    }
}
