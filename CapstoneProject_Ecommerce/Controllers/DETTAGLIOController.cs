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

                if (t.QuantitaTaglia < 0)
                {
                    t.QuantitaTaglia += quantity;
                    ViewBag.Message= $"Quantità non disponibile, rimaste in stock {t.QuantitaTaglia}";
                    return RedirectToAction("Index", "PRODOTTO");
                }
                else
                {
                db.Entry(t).State = EntityState.Modified;
                db.SaveChanges();                   
                }

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
            List<SelectListItem> ListaTaglie = new List<SelectListItem>();
            SelectListItem item1 = new SelectListItem { Text = "S", Value = "1" };
            SelectListItem item2 = new SelectListItem { Text = "M", Value = "2" };
            SelectListItem item3 = new SelectListItem { Text = "L", Value = "3" };
            ListaTaglie.Add(item1);
            ListaTaglie.Add(item2);
            ListaTaglie.Add(item3);
            ViewBag.lista = ListaTaglie;

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
                
                DETTAGLIO dettaglio = db.DETTAGLIO.Find(d.IdDettaglio);

                var a = "";

                if (d.IdTaglia == 1)
                {
                    a = "S";
                }
                else if (d.IdTaglia == 2)
                {
                    a = "M";
                }
                else if (d.IdTaglia == 3)
                {
                    a = "L";
                }
                if (d.IdTaglia == dettaglio.IdTaglia) 
                {
                    TAGLIE t = db.TAGLIE.Where(x => x.IdProdotto == d.IdProdotto && x.IdTaglie == dettaglio.IdTaglia).FirstOrDefault();
                    t.QuantitaTaglia += dettaglio.Quantita;
                    db.Entry(t).State = EntityState.Modified;
                    db.SaveChanges();
                    dettaglio.IdTaglia = d.IdTaglia;
                }
               
                dettaglio.Quantita = d.Quantita;
                TAGLIE t1 = db.TAGLIE.Where(x => x.IdProdotto == d.IdProdotto && x.IdTaglie == d.IdTaglia).FirstOrDefault();
                t1.QuantitaTaglia -= d.Quantita;
                db.Entry(t1).State = EntityState.Modified;
                db.SaveChanges();
                dettaglio.PrezzoTotale = dettaglio.PRODOTTO.Prezzo * d.Quantita;
                               
                db.Entry(dettaglio).State = EntityState.Modified;
                db.SaveChanges();
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
