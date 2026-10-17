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
    public class SolicitudsController : Controller
    {
        private GestionSolicitudesDB db = new GestionSolicitudesDB();

        // GET: Solicituds
        public ActionResult Index()
        {
            var solicitud = db.Solicitud.Include(s => s.Estado).Include(s => s.Prioridad).Include(s => s.Responsable).Include(s => s.TipoSolicitud).Include(s => s.Usuario);
            return View(solicitud.ToList());
        }

        // GET: Solicituds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // GET: Solicituds/Create
        public ActionResult Create()
        {
            ViewBag.IdEstado = new SelectList(db.Estado, "IdEstado", "Nombre");
            ViewBag.IdPrioridad = new SelectList(db.Prioridad, "IdPrioridad", "Nombre");
            ViewBag.IdResponsable = new SelectList(db.Responsable, "IdResponsable", "Nombre");
            ViewBag.IdTipo = new SelectList(db.TipoSolicitud, "IdTipo", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre");
            return View();
        }

        // POST: Solicituds/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSolicitud,IdUsuario,IdTipo,IdEstado,IdPrioridad,IdResponsable,Descripcion,Solucion,FechaCreacion,FechaCierre")] Solicitud solicitud)
        {
            solicitud.FechaCreacion = DateTime.Now;

            if (ModelState.IsValid)
            {
                db.Solicitud.Add(solicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdEstado = new SelectList(db.Estado, "IdEstado", "Nombre", solicitud.IdEstado);
            ViewBag.IdPrioridad = new SelectList(db.Prioridad, "IdPrioridad", "Nombre", solicitud.IdPrioridad);
            ViewBag.IdResponsable = new SelectList(db.Responsable, "IdResponsable", "Nombre", solicitud.IdResponsable);
            ViewBag.IdTipo = new SelectList(db.TipoSolicitud, "IdTipo", "Nombre", solicitud.IdTipo);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", solicitud.IdUsuario);
            return View(solicitud);
        }

        // GET: Solicituds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEstado = new SelectList(db.Estado, "IdEstado", "Nombre", solicitud.IdEstado);
            ViewBag.IdPrioridad = new SelectList(db.Prioridad, "IdPrioridad", "Nombre", solicitud.IdPrioridad);
            ViewBag.IdResponsable = new SelectList(db.Responsable, "IdResponsable", "Nombre", solicitud.IdResponsable);
            ViewBag.IdTipo = new SelectList(db.TipoSolicitud, "IdTipo", "Nombre", solicitud.IdTipo);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", solicitud.IdUsuario);
            return View(solicitud);
        }

        // POST: Solicituds/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSolicitud,IdUsuario,IdTipo,IdEstado,IdPrioridad,IdResponsable,Descripcion,Solucion,FechaCreacion,FechaCierre")] Solicitud solicitud)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstado = new SelectList(db.Estado, "IdEstado", "Nombre", solicitud.IdEstado);
            ViewBag.IdPrioridad = new SelectList(db.Prioridad, "IdPrioridad", "Nombre", solicitud.IdPrioridad);
            ViewBag.IdResponsable = new SelectList(db.Responsable, "IdResponsable", "Nombre", solicitud.IdResponsable);
            ViewBag.IdTipo = new SelectList(db.TipoSolicitud, "IdTipo", "Nombre", solicitud.IdTipo);
            ViewBag.IdUsuario = new SelectList(db.Usuario, "IdUsuario", "Nombre", solicitud.IdUsuario);
            return View(solicitud);
        }

        // GET: Solicituds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Solicitud solicitud = db.Solicitud.Find(id);
            if (solicitud == null)
            {
                return HttpNotFound();
            }
            return View(solicitud);
        }

        // POST: Solicituds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Solicitud solicitud = db.Solicitud.Find(id);
            db.Solicitud.Remove(solicitud);
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
