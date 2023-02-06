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
    public class DETTAGLIOController : Controller
    {
        private ModelDBcontext db = new ModelDBcontext();

        // GET: DETTAGLIO
        public ActionResult Index(int id, int quantity, string taglia)
        {
            USER utente = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            if (id > 0)
            {
                TAGLIE t = db.TAGLIE.Where(x => x.IdProdotto == id && x.TagliaProdotto == taglia ).FirstOrDefault();
                t.QuantitaTaglia -= quantity;
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();

                DETTAGLIO d = new DETTAGLIO();
                d.IdProdotto = id;
                d.IdTaglia = t.IdTaglie;
                d.Quantita = quantity;
                
                PRODOTTO p = db.PRODOTTO.Find(id);
                d.PrezzoTotale = p.Prezzo * d.Quantita;

                d.IdUser = utente.IdUser;


                db.DETTAGLIO.Add(d);
                db.SaveChanges();
            }

            return View(db.DETTAGLIO.Where(x => x.IdOrdine == null && x.IdUser == utente.IdUser).ToList());
        }


        public ActionResult Carrello()
        {
            USER utente = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
            List<DETTAGLIO> dtg = db.DETTAGLIO.Include(x => x.PRODOTTO).Where(d => d.IdOrdine == null && d.IdUser == utente.IdUser).ToList();
            return View(dtg);
        }


        // GET: DETTAGLIO/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETTAGLIO dETTAGLIO = db.DETTAGLIO.Find(id);
            if (dETTAGLIO == null)
            {
                return HttpNotFound();
            }
            return View(dETTAGLIO);
        }

        // GET: DETTAGLIO/Create
        public ActionResult Create()
        {
            ViewBag.IdOrdine = new SelectList(db.ORDINE, "IdOrdine", "Confermato");
            ViewBag.IdProdotto = new SelectList(db.PRODOTTO, "IdProdotto", "NomeProdotto");
            ViewBag.IdTaglia = new SelectList(db.TAGLIE, "IdTaglie", "TagliaProdotto");
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Username");
            return View();
        }

        // POST: DETTAGLIO/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDettaglio,IdUser,IdTaglia,IdOrdine,IdProdotto,Quantita,PrezzoTotale")] DETTAGLIO dETTAGLIO)
        {
            if (ModelState.IsValid)
            {
                db.DETTAGLIO.Add(dETTAGLIO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdOrdine = new SelectList(db.ORDINE, "IdOrdine", "Confermato", dETTAGLIO.IdOrdine);
            ViewBag.IdProdotto = new SelectList(db.PRODOTTO, "IdProdotto", "NomeProdotto", dETTAGLIO.IdProdotto);
            ViewBag.IdTaglia = new SelectList(db.TAGLIE, "IdTaglie", "TagliaProdotto", dETTAGLIO.IdTaglia);
            ViewBag.IdUser = new SelectList(db.USER, "IdUser", "Username", dETTAGLIO.IdUser);
            return View(dETTAGLIO);
        }

        // GET: DETTAGLIO/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETTAGLIO d = db.DETTAGLIO.Find(id);
            
            
                return View(d);
                   
        }

        // POST: DETTAGLIO/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DETTAGLIO d)
        {
            if (ModelState.IsValid)
            {
                USER utente = db.USER.Where(x => x.Username == User.Identity.Name).FirstOrDefault();
                DETTAGLIO dettaglio = db.DETTAGLIO.Find(d.IdDettaglio);
                d.PrezzoTotale = dettaglio.PRODOTTO.Prezzo * d.Quantita;
           

                d.IdUser = utente.IdUser;
                ModelDBcontext db1 = new ModelDBcontext();
                db1.Entry(d).State = EntityState.Modified;
                db1.SaveChanges();
            }
            return RedirectToAction("Carrello");
        }

        // GET: DETTAGLIO/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DETTAGLIO dETTAGLIO = db.DETTAGLIO.Find(id);
            if (dETTAGLIO == null)
            {
                return HttpNotFound();
            }
            return View(dETTAGLIO);
        }

        // POST: DETTAGLIO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DETTAGLIO dETTAGLIO = db.DETTAGLIO.Find(id);
            db.DETTAGLIO.Remove(dETTAGLIO);
            db.SaveChanges();
            return RedirectToAction("Carrello");
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
