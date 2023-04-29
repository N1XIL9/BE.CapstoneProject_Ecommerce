using CapstoneProject_Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace CapstoneProject_Ecommerce.Controllers
{
    public class HomeController : Controller
    {
        private ModelDBcontext db = new ModelDBcontext();
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(USER u)
        {
            if (USER.Autenticato(u.Username, u.Pass))
            {
                FormsAuthentication.SetAuthCookie(u.Username, false);
                return Redirect(FormsAuthentication.DefaultUrl);
            }
            return Redirect("/Home/Login");
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return Redirect(FormsAuthentication.DefaultUrl);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Register([Bind(Include ="Username,Nome,Cognome,Email,Pass")] USER u) 
        {
                u.Ruolo = "Utente";
            if (db.USER.Where(x => x.Username == u.Username).Count() == 0)
            {
                db.USER.Add(u);
                db.SaveChanges();
                return RedirectToAction("Login", "Home");
            }
            return RedirectToAction("Register", "Home");
        }

        public ActionResult Contatti()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Contatti(Email email)
        {
            
            MailAddress from = new MailAddress(email.mittenteEmail);
            MailAddress to = new MailAddress("nicolalerra@gmail.com");
            MailMessage message = new MailMessage(from, to);
                
            message.Subject = email.Oggetto;
            message.Body = email.Messaggio;
            message.ReplyToList.Add(new MailAddress(email.mittenteEmail));

           
            MailAddress copy = new MailAddress("nicolalerra@hotmail.it");
            message.CC.Add(copy);
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("nicolalerra@gmail.com", "gmiufcbbchqhnauf");
            smtp.Credentials = nc;
            
                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {               
                        ex.ToString();
                }
            return View("EmailDone");
        }

        public ActionResult EmailDone()
        {
            return View();
        }

        public ActionResult ABoutMe () 
        {
            return View();
        }
    }
}
    

   
