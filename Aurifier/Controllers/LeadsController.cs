using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Aurifier.Models;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Web.Security;

namespace Aurifier.Controllers
{
    public class LeadsController : Controller
    {
        AuriferLeadsEntities3 db = new AuriferLeadsEntities3();
        // GET: Leads
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Registration r,AdminLogin al)
        {
            if (ModelState.IsValid) // this is check validity
            {
                using (AuriferLeadsEntities3 dc = new AuriferLeadsEntities3())
                {
                    al = dc.AdminLogins.Where(a => a.Email.Equals(al.Email) && a.Password.Equals(al.Password)).FirstOrDefault();
                    if (al != null)
                    {
                        Session["Username"] = al.UserName.ToString();
                        return RedirectToAction("Employee", "Admin");
                    }
                    else 
                    {
                        r = dc.Registrations.Where(a => a.Email == r.Email && a.Password == r.Password).FirstOrDefault();
                        if (r != null)
                        {
                            if (r.Roles_Id == 1)
                            {
                                Session["Username"] = r.UserName.ToString();
                                return RedirectToAction("ManagerPage", "Manager");
                            }
                            else if (r.Roles_Id == 2)
                            {
                                Session["Username"] = r.UserName.ToString();
                                return RedirectToAction("Leads", "SupportTeam");
                            }
                            else if (r.Roles_Id == 3)
                            {
                                Session["Username"] = r.UserName.ToString();
                                return RedirectToAction("AssociatePage", "Associate");
                            }
                           
                        }
                        else
                        {
                            ViewBag.message = "Please enter valid Email And Password";
                        }
                    }

                }
            }
            return View(r);
        }
        [HttpGet]
        public ActionResult Forgot()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Forgot(String Email, AdminLogin al,Registration r)
        {
            if (ModelState.IsValid)
            {
                al = db.AdminLogins.Where(a => a.Email == al.Email).FirstOrDefault();
                if (al != null)
                {
                    try
                    {
                        // send mail 
                        MailMessage m = new MailMessage();
                        m.To.Add(new MailAddress(Email));
                        m.From = new MailAddress("murthybatch@gmail.com");
                        m.Subject = "Password Recovery";
                        m.IsBodyHtml = true;  // body contains HTML
                        m.Body = "Dear " + al.UserName + ",<p>Password : " + al.Password + "<p/>From Admin<br/>";
                        SmtpClient smtp = new SmtpClient();
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential("murthybatch@gmail.com", "Murthy123");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = 587;
                        smtp.Send(m);
                        //SmtpClient smtp = new SmtpClient("127.0.0.1", 25);
                        //smtp.Send(m);
                        ViewBag.Message = "Please use the mail sent to email addre ss to login!";
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Trace.Write("Error : " + ex.Message);
                        ViewBag.Message = "Sorry! Could not send mail!";
                    }
                }
                else
                {
                    r = db.Registrations.Where(a => a.Email == r.Email).FirstOrDefault();
                    if (r != null)
                    {
                        try
                        {
                            // send mail 
                            MailMessage m = new MailMessage();
                            m.To.Add(new MailAddress(Email));
                            m.From = new MailAddress("murthybatch@gmail.com");
                            m.Subject = "Password Recovery";
                            m.IsBodyHtml = true;  // body contains HTML
                            m.Body = "Dear " + r.UserName + ",<p>Password : " + r.Password + "From Admin";
                            SmtpClient smtp = new SmtpClient();
                            smtp.Host = "smtp.gmail.com";
                            smtp.EnableSsl = true;
                            NetworkCredential NetworkCred = new NetworkCredential("murthybatch@gmail.com", "Murthy123");
                            smtp.UseDefaultCredentials = true;
                            smtp.Credentials = NetworkCred;
                            smtp.Port = 587;
                            smtp.Send(m);
                           
                        }
                        catch (Exception ex)
                        {
                            HttpContext.Trace.Write("Error : " + ex.Message);
                            ViewBag.Message = "Sorry! Could not send mail!";
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Sorry! No user found with this email address!";
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