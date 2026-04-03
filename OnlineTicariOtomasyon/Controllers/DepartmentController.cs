using System;
using System.Linq;
using System.Web.Mvc;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class DepartmentController : Controller
    {
        Context c = new Context();

        
        public ActionResult Index()
        {
            var departments = c.Departments.ToList();
            return View(departments);
        }

      
        [HttpGet]
        public ActionResult DepartmentAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult DepartmentAdd(Department department)
        {
            if (ModelState.IsValid)
            {
                c.Departments.Add(department);
                c.SaveChanges();
                TempData["Success"] = "Departman başarıyla eklendi!";
                return View(); 
            }
            TempData["Error"] = "Departman eklenirken bir hata oluştu!";
            return View(department);
        }

       
        [HttpGet]
        public ActionResult DepartmentEdit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var dept = c.Departments.Find(id.Value);
            if (dept == null)
                return HttpNotFound();

            return View(dept);
        }

        [HttpPost]
        public ActionResult DepartmentEdit(Department department)
        {
            if (ModelState.IsValid)
            {
                var existingDept = c.Departments.Find(department.DepartmentID);
                if (existingDept == null)
                {
                    TempData["Error"] = "Departman bulunamadı!";
                    return RedirectToAction("Index");
                }

                existingDept.DepartmentName = department.DepartmentName;
                existingDept.DepartmentImage = department.DepartmentImage;
                c.SaveChanges();

            
                TempData["Success"] = "Departman başarıyla güncellendi!";
                return RedirectToAction("Index");
            }

            TempData["Error"] = "Departman güncellenirken bir hata oluştu!";
            return View(department);
        }

        [HttpPost]
        public ActionResult DepartmentDelete(int id)
        {
            var dept = c.Departments.Find(id);
            if (dept == null)
            {
                TempData["Error"] = "Departman bulunamadı!";
                return RedirectToAction("Index");
            }

            c.Departments.Remove(dept);
            c.SaveChanges();

           
            TempData["Success"] = "Departman başarıyla silindi!";
            return RedirectToAction("Index");
        }

    }
}