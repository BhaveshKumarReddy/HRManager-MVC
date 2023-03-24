using HRM_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRM_Project.Controllers
{
    public class EmployeesListController : Controller
    {
        private readonly HRM_DB_ProjectContext db;
        public EmployeesListController(HRM_DB_ProjectContext _db)
        {
            db = _db;
        }
        public IActionResult displayEmployees()
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                string email = HttpContext.Session.GetString("Email");
                string location_name = db.HrLists.Include(hrl => hrl.Location).SingleOrDefault(x => x.HrEmail == email).Location.LocationName;
                ViewBag.location_name = location_name;
                var employees = db.EmployeesLists.Include(emp => emp.Location).Where(emp => emp.Location.LocationName == location_name).ToList();
                return View(employees);
            }
            catch(Exception e)
            {
                return RedirectToAction("handleException","Home", new { message = e.Message });
            }
        }
        public IActionResult add_Employee()
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                string hr_location = HttpContext.Session.GetString("Location");
                ViewBag.LocationId = db.LocationLists.FirstOrDefault(x => x.LocationId == hr_location).LocationId;
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult add_Employee(EmployeesList employee)
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                db.EmployeesLists.Add(employee);
                db.SaveChanges();
                return RedirectToAction("displayEmployees");
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        public IActionResult open_Employee(int id)
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                EmployeesList employee = db.EmployeesLists.Include(x => x.Location).SingleOrDefault(emp => emp.EmployeeId == id);
                ViewBag.locations = db.LocationLists.Select(x => new SelectListItem { Value = x.LocationId.ToString(), Text = x.LocationName }).ToList();
                ViewBag.eligible = (employee.EmployeeAppraisalDate <= DateTime.Today);
                return View(employee);
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult incrementSalary(EmployeesList employee)
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                EmployeesList emp = db.EmployeesLists.SingleOrDefault(emp => emp.EmployeeId == employee.EmployeeId);
                emp.EmployeeSalary = employee.EmployeeSalary;
                emp.EmployeeAppraisalDate = DateTime.Today.AddYears(1);
                db.EmployeesLists.Update(emp);
                db.SaveChanges();
                return RedirectToAction("open_Employee", new { id = employee.EmployeeId });
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult editDetails(EmployeesList employee)
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                db.EmployeesLists.Update(employee);
                db.SaveChanges();
                return RedirectToAction("displayEmployees");
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult fireEmployee(EmployeesList employee)
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                db.EmployeesLists.Remove(employee);
                db.SaveChanges();
                return RedirectToAction("displayEmployees");
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult transferEmployee(EmployeesList employee)
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                EmployeesList employee1 = db.EmployeesLists.SingleOrDefault(emp => emp.EmployeeId == employee.EmployeeId);
                employee1.LocationId = employee.LocationId;
                db.EmployeesLists.Update(employee1);
                db.SaveChanges();
                return RedirectToAction("displayEmployees");
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
    }
}
