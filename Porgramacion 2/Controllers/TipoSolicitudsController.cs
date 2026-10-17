using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Porgramacion_2.Models;

namespace Porgramacion_2.Controllers
{
    public class TipoSolicitudsController : Controller
    {
        private GestionSolicitudesDB db = new GestionSolicitudesDB();

        // GET: TipoSolicituds
        public ActionResult Index()
        {
            return View(db.TipoSolicitud.ToList());
        }

        // GET: TipoSolicituds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoSolicitud tipoSolicitud = db.TipoSolicitud.Find(id);
            if (tipoSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(tipoSolicitud);
        }

        // GET: TipoSolicituds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoSolicituds/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdTipo,Nombre,Activo")] TipoSolicitud tipoSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.TipoSolicitud.Add(tipoSolicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoSolicitud);
        }

        // GET: TipoSolicituds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoSolicitud tipoSolicitud = db.TipoSolicitud.Find(id);
            if (tipoSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(tipoSolicitud);
        }

        // POST: TipoSolicituds/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdTipo,Nombre,Activo")] TipoSolicitud tipoSolicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoSolicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoSolicitud);
        }

        // GET: TipoSolicituds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoSolicitud tipoSolicitud = db.TipoSolicitud.Find(id);
            if (tipoSolicitud == null)
            {
                return HttpNotFound();
            }
            return View(tipoSolicitud);
        }

        // POST: TipoSolicituds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoSolicitud tipoSolicitud = db.TipoSolicitud.Find(id);
            db.TipoSolicitud.Remove(tipoSolicitud);
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
