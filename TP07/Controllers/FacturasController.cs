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
    public class FacturasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Facturas
        public ActionResult Index()
        {
            return View(db.FacturasModels.ToList());
        }

        // GET: Facturas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturasModels facturasModels = db.FacturasModels.Find(id);
            if (facturasModels == null)
            {
                return HttpNotFound();
            }
            return View(facturasModels);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            if (TempData.Count > 0 && TempData["recargar"].ToString() == "no")
            {
                TempData.Remove("recargar");
                return View(TempData["facturaViewModel"]);
            }
            FacturaViewModel facturaViewModel = new FacturaViewModel();
            facturaViewModel.factura = new FacturasModels();
            facturaViewModel.factura.fecha = DateTime.Now;
            facturaViewModel.detalle = new FacturaDetallesModels();
            facturaViewModel.articulos = db.ArticulosModels.ToList().Select(i => new SelectListItem() { Value = i.id.ToString(), Text = i.descripcion }).ToList();
            return View(facturaViewModel);
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacturaViewModel facturaViewModel)
        {
            if (ModelState.IsValid)
            {
                db.FacturasModels.Add(facturaViewModel.factura);
                db.SaveChanges();
                //return RedirectToAction("Create");
            }
            facturaViewModel.articulos = db.ArticulosModels.ToList().Select(i => new SelectListItem() { Value = i.id.ToString(), Text = i.descripcion }).ToList();
            return View(facturaViewModel);
        }

        // GET: Facturas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturasModels facturasModels = db.FacturasModels.Find(id);
            if (facturasModels == null)
            {
                return HttpNotFound();
            }
            return View(facturasModels);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,numeroFactura,fecha")] FacturasModels facturasModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturasModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(facturasModels);
        }

        // GET: Facturas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturasModels facturasModels = db.FacturasModels.Find(id);
            if (facturasModels == null)
            {
                return HttpNotFound();
            }
            return View(facturasModels);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FacturasModels facturasModels = db.FacturasModels.Find(id);
            db.FacturasModels.Remove(facturasModels);
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
