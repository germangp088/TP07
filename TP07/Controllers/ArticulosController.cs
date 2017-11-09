using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TP07.Models;

namespace TP07.Controllers
{
    public class ArticulosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Articulos
        public ActionResult Index()
        {
            return View(db.ArticulosModels.ToList());
        }

        // GET: Articulos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticulosModels articulosModels = db.ArticulosModels.Find(id);
            if (articulosModels == null)
            {
                return HttpNotFound();
            }
            return View(articulosModels);
        }

        // GET: Articulos/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Articulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,codigo,descripcion,precio")] ArticulosModels articulosModels)
        {
            if (ModelState.IsValid)
            {
                db.ArticulosModels.Add(articulosModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(articulosModels);
        }

        // GET: Articulos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticulosModels articulosModels = db.ArticulosModels.Find(id);
            if (articulosModels == null)
            {
                return HttpNotFound();
            }
            return View(articulosModels);
        }

        // POST: Articulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,codigo,descripcion,precio")] ArticulosModels articulosModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(articulosModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(articulosModels);
        }

        // GET: Articulos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArticulosModels articulosModels = db.ArticulosModels.Find(id);
            if (articulosModels == null)
            {
                return HttpNotFound();
            }
            return View(articulosModels);
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ArticulosModels articulosModels = db.ArticulosModels.Find(id);
            db.ArticulosModels.Remove(articulosModels);
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
