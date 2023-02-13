using System;
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
    [Authorize]
    public class ORDINEController : Controller
    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: ORDINE
        public ActionResult Index()
        {
            var oRDINE = db.ORDINE.Include(o => o.USER);
            return View(oRDINE.ToList());
        }

        public ActionResult OrdineConfermato()
        {

            USER u = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            List<ORDINE> oRDINEs = db.ORDINE.Where(x => x.IdUser == u.IdUser).ToList();
            return View(oRDINEs);

        }

        // GET: ORDINE/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            List<DETTAGLIO> d = db.DETTAGLIO.Include(x => x.ORDINE).Include(x => x.PRODOTTO).Where(x => x.IdOrdine == id).ToList();
           

            return View(d);
        }

        // GET: ORDINE/Create
        public ActionResult Create()
        {
            ORDINE o = new ORDINE();
            USER u = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            List<DETTAGLIO> d = db.DETTAGLIO.Where(x => x.IdOrdine == null && x.IdUser == u.IdUser).ToList();
            o.ImportoTotale = d.Sum(x => x.PrezzoTotale);
            o.IdUser = u.IdUser;
            o.DETTAGLIO = d;
            o.USER = u;


            return View(o);
        }

        // POST: ORDINE/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( ORDINE o)
        {
            o.Confermato = "Si";
            o.Evaso = "No";
            db.ORDINE.Add(o);
            db.SaveChanges();
            USER u = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            int id = u.IdUser;
            List<DETTAGLIO> list =  db.DETTAGLIO.Where(x => x.IdUser == id && x.IdOrdine == null ).ToList();

            foreach (var item in list)
            {
                item.IdOrdine = o.IdOrdine;
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("OrdineConfermato");
        }

        // GET: ORDINE/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDINE oRDINE = db.ORDINE.Find(id);
            if (oRDINE == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Username", oRDINE.IdUser);
            return View(oRDINE);
        }

        // POST: ORDINE/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdOrdine,IdUser,Confermato,DataOrdine,ImportoTotale,Evaso")] ORDINE oRDINE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(oRDINE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Username", oRDINE.IdUser);
            return View(oRDINE);
        }

        // GET: ORDINE/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ORDINE oRDINE = db.ORDINE.Find(id);
            if (oRDINE == null)
            {
                return HttpNotFound();
            }
            return View(oRDINE);
        }

        // POST: ORDINE/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var ordineDaEliminare = db.ORDINE.Find(id);

            if (ordineDaEliminare != null)
            {
                var dettagliDaEliminare = db.DETTAGLIO.Where(d => d.IdOrdine == id);
                db.DETTAGLIO.RemoveRange(dettagliDaEliminare);
                db.SaveChanges();
            }

            ORDINE oRDINE = db.ORDINE.Find(id);
            db.ORDINE.Remove(oRDINE);
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
