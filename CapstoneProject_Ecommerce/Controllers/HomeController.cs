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

            //MailMessage mm = new MailMessage();
            //mm.From = new MailAddress(email.mittenteEmail);
            //mm.To.Add("nicolalerra@gmail.com");
            //mm.Subject = email.Oggetto;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.EnableSsl = true;
            //NetworkCredential nc = new NetworkCredential("nicolalerra@gmail.com", "gmiufcbbchqhnauf");
            //smtp.Credentials = nc;
            //mm.Body = email.Messaggio;
            //smtp.Send(mm);


            
            
            MailAddress from = new MailAddress(email.mittenteEmail);
            MailAddress to = new MailAddress("nicolalerra@gmail.com");
            MailMessage message = new MailMessage(from, to);
                
            message.Subject = email.Oggetto;
            message.Body = email.Messaggio;
            message.ReplyToList.Add(new MailAddress(email.mittenteEmail));

           
            MailAddress copy = new MailAddress("guns.r_91@hotmail.it");
            message.CC.Add(copy);
            SmtpClient smtp = new SmtpClient();

            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("nicolalerra@gmail.com", "gmiufcbbchqhnauf");
            smtp.Credentials = nc;
            Console.WriteLine("Sending an email message to {0} by using the SMTP host {1}.",
                     to.Address, smtp.Host);

                try
                {
                    smtp.Send(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught in CreateCopyMessage(): {0}",
                        ex.ToString());
                }
            return View("Contatti");
        }

        public ActionResult ABoutMe () 
        {
            return View();
        }
    }
}
    

   
