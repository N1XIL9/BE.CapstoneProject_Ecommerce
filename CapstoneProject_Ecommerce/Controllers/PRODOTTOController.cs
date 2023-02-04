﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CapstoneProject_Ecommerce.Models;

namespace CapstoneProject_Ecommerce.Controllers
    
{
    [Authorize(Roles = "Amministratore")]
    public class PRODOTTOController : Controller
    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: PRODOTTO
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.PRODOTTO.ToList());
        }

        public ActionResult ListaAdmin()
        {
            return View(db.PRODOTTO.ToList());
        }

        // GET: PRODOTTO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODOTTO pRODOTTO = db.PRODOTTO.Find(id);
            if (pRODOTTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODOTTO);
        }

        // GET: PRODOTTO/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PRODOTTO/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( PRODOTTO pRODOTTO, HttpPostedFileBase File)
        {
            if (ModelState.IsValid)
            {
                File.SaveAs(Server.MapPath("/Content/Img/" + File.FileName));
                pRODOTTO.Foto = File.FileName;
                db.PRODOTTO.Add(pRODOTTO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pRODOTTO);
        }

        // GET: PRODOTTO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODOTTO pRODOTTO = db.PRODOTTO.Find(id);
            if (pRODOTTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODOTTO);
        }

        // POST: PRODOTTO/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PRODOTTO pRODOTTO, HttpPostedFileBase File)
        {
            if (File != null)
            {
                string Path = Server.MapPath("/Content/img/" + File.FileName);
                File.SaveAs(Path);
                pRODOTTO.Foto = File.FileName;
            }
            else
            {
                PRODOTTO p = db.PRODOTTO.Find(pRODOTTO.IdProdotto);
                pRODOTTO.Foto = p.Foto;
            }
            ModelDBcontext db1 = new ModelDBcontext();
            db1.Entry(pRODOTTO).State = EntityState.Modified;
            db1.SaveChanges();

            return RedirectToAction("ListaAdmin");
        }

        // GET: PRODOTTO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRODOTTO pRODOTTO = db.PRODOTTO.Find(id);
            if (pRODOTTO == null)
            {
                return HttpNotFound();
            }
            return View(pRODOTTO);
        }

        // POST: PRODOTTO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PRODOTTO pRODOTTO = db.PRODOTTO.Find(id);
            db.PRODOTTO.Remove(pRODOTTO);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
