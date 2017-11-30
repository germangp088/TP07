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
    public class FacturaDetallesModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: FacturaDetallesModels
        public ActionResult Index()
        {
            var facturaDetallesModels = db.FacturaDetallesModels.Include(f => f.articulo).Include(f => f.factura);
            return View(facturaDetallesModels.ToList());
        }

        // GET: FacturaDetallesModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturaDetallesModels facturaDetallesModels = db.FacturaDetallesModels.Find(id);
            if (facturaDetallesModels == null)
            {
                return HttpNotFound();
            }
            return View(facturaDetallesModels);
        }

        // GET: FacturaDetallesModels/Create
        public ActionResult Create()
        {
            ViewBag.articuloid = new SelectList(db.ArticulosModels, "id", "codigo");
            ViewBag.facturaid = new SelectList(db.FacturasModels, "id", "id");
            return View();
        }

        // POST: FacturaDetallesModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FacturaViewModel facturaViewModel)
        {
            if (ModelState.IsValid)
            {
                
                facturaViewModel.detalle.facturaid = facturaViewModel.factura.id;

                db.FacturaDetallesModels.Add(facturaViewModel.detalle);
                List<FacturasModels> fm = db.FacturasModels.Where(x => x.id == facturaViewModel.factura.id).ToList();
                facturaViewModel.factura = fm[0];
                db.SaveChanges();
            }
            facturaViewModel.articulos = db.ArticulosModels.ToList().Select(i => new SelectListItem() { Value = i.id.ToString(), Text = i.descripcion }).ToList();
            List<FacturaDetallesModels> detalles = db.FacturaDetallesModels.Where(x => x.facturaid == facturaViewModel.factura.id).ToList();
            facturaViewModel.factura.total = detalles.Sum(x => x.precio * x.cantidad);
            TempData.Add("recargar", "no");
            TempData.Remove("facturaViewModel");
            TempData.Add("facturaViewModel", facturaViewModel);

            return RedirectToAction("Create", "Facturas");
        }

        // GET: FacturaDetallesModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturaDetallesModels facturaDetallesModels = db.FacturaDetallesModels.Find(id);
            if (facturaDetallesModels == null)
            {
                return HttpNotFound();
            }
            ViewBag.articuloid = new SelectList(db.ArticulosModels, "id", "codigo", facturaDetallesModels.articuloid);
            ViewBag.facturaid = new SelectList(db.FacturasModels, "id", "id", facturaDetallesModels.facturaid);
            return View(facturaDetallesModels);
        }

        // POST: FacturaDetallesModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,facturaid,articuloid,cantidad,precio")] FacturaDetallesModels facturaDetallesModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(facturaDetallesModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.articuloid = new SelectList(db.ArticulosModels, "id", "codigo", facturaDetallesModels.articuloid);
            ViewBag.facturaid = new SelectList(db.FacturasModels, "id", "id", facturaDetallesModels.facturaid);
            return View(facturaDetallesModels);
        }

        // GET: FacturaDetallesModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FacturaDetallesModels facturaDetallesModels = db.FacturaDetallesModels.Find(id);
            if (facturaDetallesModels == null)
            {
                return HttpNotFound();
            }
            return View(facturaDetallesModels);
        }

        // POST: FacturaDetallesModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            FacturaDetallesModels facturaDetallesModels = db.FacturaDetallesModels.Find(id);
            db.FacturaDetallesModels.Remove(facturaDetallesModels);
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
