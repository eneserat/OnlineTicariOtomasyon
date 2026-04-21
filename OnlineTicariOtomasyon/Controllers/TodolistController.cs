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
    }
}