using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using OnlineTicariOtomasyon.Models.Classes;

namespace OnlineTicariOtomasyon.Controllers
{
    public class EmployeeController : Controller
    {
        Context c = new Context();
        public ActionResult Index()
        {   
            var personeller = c.Employees.Include(e => e.Department).ToList();
           
            return View(personeller);

        }
        [HttpGet]
        public ActionResult EmployeeAdd()
        {
            ViewBag.Departments = new SelectList(c.Departments.ToList(), "DepartmentID", "DepartmentName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeAdd(Employee employee, HttpPostedFileBase EmployeeImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                
                    if (EmployeeImage != null && EmployeeImage.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(EmployeeImage.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/EmployeeImages"), fileName);
                        EmployeeImage.SaveAs(path);
                        employee.EmployeeImage = "~/Content/EmployeeImages/" + fileName;
                    }

                    c.Employees.Add(employee);
                    c.SaveChanges();

                    TempData["Success"] = "Personel başarıyla sisteme eklendi!";
                    return RedirectToAction("EmployeeAdd"); 
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Personel eklenirken bir hata oluştu: " + ex.Message;
            }

         
            ViewBag.Departments = new SelectList(c.Departments.ToList(), "DepartmentID", "DepartmentName", employee.DepartmentID);
            return View(employee);
        }
        [HttpGet]
        public ActionResult EmployeeEdit(int? id)
        {
            if (id == null)
                return RedirectToAction("Index");

            var employee = c.Employees.Find(id.Value);
            if (employee == null)
                return HttpNotFound();

         
            ViewBag.Departments = new SelectList(c.Departments.ToList(), "DepartmentID", "DepartmentName", employee.DepartmentID);

            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeEdit(Employee employee, HttpPostedFileBase EmployeeImage)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = c.Employees.Find(employee.EmployeeID);
                    if (existing == null)
                    {
                        ViewBag.Error = "Personel bulunamadı!";
                        ViewBag.Departments = new SelectList(c.Departments.ToList(), "DepartmentID", "DepartmentName", employee.DepartmentID);
                        return View(employee);
                    }


                    existing.EmployeeName = employee.EmployeeName;
                    existing.EmployeeSurname = employee.EmployeeSurname;
                    existing.DepartmentID = employee.DepartmentID;

                    if (EmployeeImage != null && EmployeeImage.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(EmployeeImage.FileName);
                        var path = Path.Combine(Server.MapPath("~/Content/EmployeeImages"), fileName);
                        EmployeeImage.SaveAs(path);
                        existing.EmployeeImage = "~/Content/EmployeeImages/" + fileName;
                    }

                    c.SaveChanges();

                 
                    ViewBag.Success = "Personel başarıyla güncellendi!";
                    ViewBag.Departments = new SelectList(c.Departments.ToList(), "DepartmentID", "DepartmentName", employee.DepartmentID);
                    return View(existing);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Personel güncellenirken bir hata oluştu: " + ex.Message;
            }

            ViewBag.Departments = new SelectList(c.Departments.ToList(), "DepartmentID", "DepartmentName", employee.DepartmentID);
            return View(employee);
        }
        [HttpGet]
        public ActionResult EmployeeAssignDepartment(int? id)
        {
            if (id == null)
            {
                TempData["Error"] = "Personel seçilmedi!";
                return RedirectToAction("Index");
            }

            var employee = c.Employees.Find(id.Value);
            if (employee == null)
            {
                TempData["Error"] = "Personel bulunamadı!";
                return RedirectToAction("Index");
            }

            // Departman listesini dropdown için
            ViewBag.Departments = new SelectList(c.Departments.ToList(), "DepartmentID", "DepartmentName", employee.DepartmentID);

            return View(employee);
        }

        // POST: Departman atama işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EmployeeAssignDepartment(int EmployeeID, int DepartmentID)
        {
            var employee = c.Employees.Find(EmployeeID);
            if (employee == null)
            {
                TempData["Error"] = "Personel bulunamadı!";
                return RedirectToAction("Index");
            }

            employee.DepartmentID = DepartmentID;
            c.SaveChanges();

            TempData["Success"] = "Personelin departmanı başarıyla güncellendi!";
            return RedirectToAction("EmployeeAssignDepartment", new { id = EmployeeID }); 
        }
    }
}
    

   

    
