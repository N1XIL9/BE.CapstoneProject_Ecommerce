using CapstoneProject_Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
