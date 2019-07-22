using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aurifier.Models;

namespace Aurifier.Controllers
{
    public class SupportController : Controller
    {
        // GET: Support
        AuriferLeadsEntities3 db = new AuriferLeadsEntities3();
        public ActionResult SupportPage()
        {
            List<Role> roleName = db.Roles.ToList();
            List<TeamsTable> teamName = db.TeamsTables.ToList();
            ViewBag.Role = new SelectList(roleName, "Roles_Id", "Roles_Name");
            ViewBag.Team = new SelectList(teamName, "TeamId", "TeamNumber");
            return View();
        }
        public JsonResult getLead()
        {
            List<LeadViewModel> leadList = db.LeadsTables.Select(x => new LeadViewModel
            {
                Lead_Id = x.Lead_Id,
                LeadName = x.LeadName,
                Company = x.Company,
                Address = x.Address,
                Mail_Id = x.Mail_Id,
                MobileNumber = x.MobileNumber,
                GeneratedBy=x.GeneratedBy,
                TeamNumber = x.TeamsTable.TeamNumber,
                Manager=x.Manager,
                Roles_Name = x.Role.Roles_Name,
                UserName=x.Registration.UserName,
                Date=x.Date,
                Time=x.Time,
                Descreption=x.Descreption
            }).ToList();
            ViewBag.LeadList = leadList;
            return Json(leadList, JsonRequestBehavior.AllowGet);
        }

    }
}