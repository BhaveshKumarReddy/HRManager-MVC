using HRM_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using EncryptionLibrary;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace HRM_Project.Controllers
{
    public class HrListController : Controller
    {
        private readonly HRM_DB_ProjectContext db;
        RailFence cipher = new RailFence();
        public HrListController(HRM_DB_ProjectContext _db)
        {
            db = _db;
        }
        public IActionResult display_HRs()
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                var hr_list = db.HrLists.Include(hrRow => hrRow.Location).ToList();
                return View(hr_list);
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }

        public IActionResult create_HR()
        {
            try
            {
                ViewBag.LocationId = db.LocationLists.Select(x => new SelectListItem { Value = x.LocationId.ToString(), Text = x.LocationName }).ToList();
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }

        [HttpPost]
        public IActionResult create_HR(HrList hr)
        {
            try
            {
                HrList new_hr = hr;
                new_hr.HrPassword = cipher.Encrypt(hr.HrPassword);
                db.HrLists.Add(new_hr);
                db.SaveChanges();
                HttpContext.Session.SetString("Location", db.HrLists.FirstOrDefault(x => x.HrEmail == hr.HrEmail).LocationId);
                HttpContext.Session.SetString("Email", hr.HrEmail);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        public IActionResult login_HR()
        {
            try
            {
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult login_HR(HrList hr)
        {
            try
            {
                string encrypted_password = cipher.Encrypt(hr.HrPassword);
                var login_account = db.HrLists.SingleOrDefault(x => x.HrEmail == hr.HrEmail && x.HrPassword == encrypted_password);
                if (login_account == null)
                {
                    ViewBag.error = "Login details are incorrect!";
                    return View();
                }
                HttpContext.Session.SetString("Location", db.HrLists.FirstOrDefault(x => x.HrEmail == hr.HrEmail).LocationId);
                HttpContext.Session.SetString("Email", hr.HrEmail);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        public IActionResult logout_HR()
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        public IActionResult confirmLogout_HR()
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                HttpContext.Session.Clear();
                return RedirectToAction("login_HR");
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        public IActionResult edit_HR()
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                string email = HttpContext.Session.GetString("Email");
                var account = db.HrLists.Include(x => x.Location).SingleOrDefault(x => x.HrEmail == email);
                account.HrPassword = cipher.Decrypt(account.HrPassword);
                //ViewBag.Location = db.LocationLists.SingleOrDefault(x => x.LocationId == account.LocationId).LocationName;
                //ViewBag.Location = db.LocationLists.Where(x => x.LocationId == account.LocationId).Select(x => new SelectListItem { Value = x.LocationId.ToString(), Text = x.LocationName }).ToList();
                return View(account);
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }
        [HttpPost]
        public IActionResult edit_HR(HrList hr)
        {
            try
            {
                if (HttpContext.Session.GetString("Email") == null)
                {
                    return RedirectToAction("login_HR", "HrList");
                }
                HrList new_hr = hr;
                new_hr.HrPassword = cipher.Encrypt(hr.HrPassword);
                db.HrLists.Update(new_hr);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception e)
            {
                return RedirectToAction("handleException", "Home", new { message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult IsEmailAvailable(HrList hr)
        {
            if(db.HrLists.Any(x=> x.HrEmail == hr.HrEmail))
            {
                return Json(false);
            }
            return Json(true);
        }
    }
}
