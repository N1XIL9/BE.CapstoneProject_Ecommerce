using CapstoneProject_Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
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
                if (ModelState.IsValid)
                {
                    // Imposta le informazioni per inviare l'email tramite SMTP
                    var smtpClient = new SmtpClient("smtp.live.com", 25)
                    {
                        Credentials = new NetworkCredential("nicolalerra@hotmail.it", "pss"),
                        EnableSsl = true
                    };

                    // Crea il messaggio email
                    var message = new MailMessage()
                    {
                        From = new MailAddress(email.EmailMittente),
                        Subject = email.Oggetto,
                        Body = email.Messaggio
                    };

                    // Aggiunge il destinatario
                    message.To.Add("nicolalerra@hotmail.it");

                    // Invia l'email
                    smtpClient.Send(message);

                    return RedirectToAction("Grazie"); // sostituisci "Grazie" con il nome dell'azione che restituisce la view di ringraziamento
                }
                return Contatti(email); // sostituisci "View" con il nome della view che mostra il form per inviare l'email
            }
    }
}
    

   
