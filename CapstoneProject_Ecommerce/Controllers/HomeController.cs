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



            MailMessage mm = new MailMessage();
            MailAddress fromAddress = new MailAddress(email.EmailMittente);
            mm.From = fromAddress;
            mm.To.Add("nicolalerra@gmail.com");
            mm.Subject = email.Oggetto;



            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            NetworkCredential nc = new NetworkCredential("nicolalerra@gmail.com", "gmiufcbbchqhnauf");
            smtp.Credentials = nc;

            mm.Body = email.Messaggio;

            smtp.Send(mm);




            //MailMessage mm = new MailMessage();
            //mm.From = new MailAddress(email.EmailMittente); // Imposta l'indirizzo email del mittente
            //mm.To.Add("nicolalerra@gmail.com");
            //mm.Subject = email.Oggetto;
            //mm.Body = email.Messaggio;
            //SmtpClient smtp = new SmtpClient();
            //smtp.Host = "smtp.gmail.com";
            //smtp.Port = 587;
            //smtp.EnableSsl = true;
            //smtp.Credentials = new NetworkCredential("nicolalerra@gmail.com", "gmiufcbbchqhnauf"); // Imposta le credenziali di autenticazione
            //smtp.Send(mm);



            return Contatti(email); // sostituisco "View" con il nome della view che mostra il form per inviare l'email
            }

        public ActionResult ABoutMe () 
        {
            return View();
        }
    }
}
    

   
