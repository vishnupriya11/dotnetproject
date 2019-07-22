using Aurifier.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Aurifier.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        AuriferLeadsEntities3 db = new AuriferLeadsEntities3();
        public ActionResult Employee()
        {
            List<Role> roleName = db.Roles.ToList();
            List<TeamsTable> teamName = db.TeamsTables.ToList();
            ViewBag.Role = new SelectList(roleName, "Roles_Id", "Roles_Name");
            ViewBag.Team = new SelectList(teamName, "TeamId", "TeamNumber");
            return View();
        }
        public JsonResult getEmployee()
        {
            //int Results;
            List<EmployeeViewModel> empList = db.Registrations.Where(x => x.IsDeleted == false).Select(x => new EmployeeViewModel
            {
                EmpId = x.EmpId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserName = x.UserName,
                MobileNumber = x.MobileNumber,
                Email = x.Email,
                Roles_Name = x.Role.Roles_Name,
                TeamNumber = x.TeamsTable.TeamNumber
            }).ToList();
            ViewBag.EmployeeList = empList;
            return Json(empList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getEmployeeById(int EmpId)
        {
            Registration reg = db.Registrations.Where(x => x.EmpId == EmpId).FirstOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(reg, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveDataInDataBase(EmployeeViewModel model)
        {
            var result = false;
            try
            {
                if (model.EmpId > 0)
                {
                    Registration reg = db.Registrations.SingleOrDefault(x => x.IsDeleted == false && x.EmpId == model.EmpId);
                    reg.FirstName = model.FirstName;
                    reg.LastName = model.LastName;
                    reg.UserName = model.UserName;
                    reg.Email = model.Email;
                    reg.MobileNumber = model.MobileNumber;              
                    reg.Roles_Id = model.Roles_Id;
                    reg.TeamId = model.TeamId;
                    db.SaveChanges();
                    result = true;
                }
                else
                {
                    Registration reg = new Registration();
                    reg.FirstName = model.FirstName;
                    reg.LastName = model.LastName;
                    reg.UserName = model.UserName;
                    reg.Email = model.Email;
                    reg.MobileNumber = model.MobileNumber;
                    reg.Password = model.Password;
                    reg.ConfirmPassword = model.ConfirmPassword;
                    reg.Roles_Id = model.Roles_Id;
                    reg.TeamId = model.TeamId;
                    db.Registrations.Add(reg);
                    db.SaveChanges();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        //this method for receive slected data from view & change the database IsDeleted=true
        public JsonResult DeleteEmployeeRecord(int EmpId)
        {
            bool result = false;
            Registration r = db.Registrations.SingleOrDefault(x => x.IsDeleted == false && x.EmpId == EmpId);
            if (r != null)
            {
                r.IsDeleted = true;
                db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangePassword()
        {
            ChangePassword cp = new ChangePassword();

            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword cp,AdminLogin al,Registration r)
        {
            if (ModelState.IsValid)
            {
                
                string username = Session["UserName"].ToString();
                al = db.AdminLogins.Where(a=>a.UserName== username && a.Password == cp.OldPassword).FirstOrDefault();
                if (al != null)
                {
                    al.Password = cp.Password;
                    al.ConfirmPassword = cp.ConfirmPassword;
                    db.SaveChanges();
                    return RedirectToAction("Login","Leads");

                }
                   
                else
                {
                    r = db.Registrations.Where(a => a.UserName == username && a.Password == cp.Password).FirstOrDefault();
                    if (r != null)
                    {
                        r.Password = cp.Password;
                        r.ConfirmPassword = cp.ConfirmPassword;
                        db.SaveChanges();
                        return RedirectToAction("Login", "Leads");
                    }
                    else
                    {
                        ViewBag.Message = "Sorry! Invalid Old Password!";
                    }
                }
            }
            return View();
        }
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }


    }
}