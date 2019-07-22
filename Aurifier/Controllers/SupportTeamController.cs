using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aurifier.Models;
using Newtonsoft.Json;

namespace Aurifier.Controllers
{
    public class SupportTeamController : Controller
    {
        // GET: SupportTeam 
        AuriferLeadsEntities3 db = new AuriferLeadsEntities3();
       
        public ActionResult Leads()
        {
            List<Role> roleName = db.Roles.ToList();
            List<TeamsTable> teamName = db.TeamsTables.ToList();
            List<Registration> userName = db.Registrations.ToList();
            ViewBag.Role = new SelectList(roleName, "Roles_Id", "Roles_Name");
            ViewBag.Team = new SelectList(teamName, "TeamId", "TeamNumber");
            //ViewBag.UserName = new SelectList(userName, "EmpId", "UserName");
            return View();

        }
        public JsonResult GetRoles()
        {
            return Json(db.Roles.ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUserNameByRolesId(string roleId)
        {
            int Id = Convert.ToInt32(roleId);
            var username = from a in db.Registrations where a.Roles_Id == Id select a;
            return Json(username);
        }
        //public JsonResult getUserNameList(int RolesId)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    List<Registration> UserNameList = db.Registrations.Where(x => x.Roles_Id == RolesId && x.IsDeleted == false).ToList();
        //    return Json(UserNameList, JsonRequestBehavior.AllowGet);
        //}
        public JsonResult getLead(FormCollection fc)
        {
            List<LeadViewModel> leadList = db.LeadsTables.Select(x => new LeadViewModel
            {
                Lead_Id = x.Lead_Id,
                LeadName = x.LeadName,
                Company = x.Company,
                Address = x.Address,
                Mail_Id = x.Mail_Id,
                MobileNumber = x.MobileNumber,
                GeneratedBy = x.GeneratedBy,
                TeamNumber = x.TeamsTable.TeamNumber,
                Manager = x.Manager,
                Roles_Name = x.Role.Roles_Name,
                UserName = x.Registration.UserName,
                Date = x.Date,
                Time = x.Time,
                Descreption = x.Descreption
            }).ToList();
            ViewBag.LeadList = leadList;
            return Json(leadList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult getEmployeeById(int LeadId)
        {
            LeadsTable led = db.LeadsTables.Where(x => x.Lead_Id == LeadId).FirstOrDefault();
            string value = string.Empty;
            value = JsonConvert.SerializeObject(led, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(value, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChangePassword()
        {
            ChangePassword cp = new ChangePassword();

            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(ChangePassword cp, AdminLogin al, Registration r)
        {
            if (ModelState.IsValid)
            {

                string username = Session["UserName"].ToString();
                al = db.AdminLogins.Where(a => a.UserName == username && a.Password == cp.OldPassword).FirstOrDefault();
                if (al != null)
                {
                    al.Password = cp.Password;
                    al.ConfirmPassword = cp.ConfirmPassword;
                    db.SaveChanges();
                    return RedirectToAction("Login", "Leads");

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
    }
}