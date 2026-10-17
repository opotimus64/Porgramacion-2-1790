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
    public class SeguimientoesController : Controller
    {
        private GestionSolicitudesDB db = new GestionSolicitudesDB();

        // GET: Seguimientoes
        public ActionResult Index()
        {
            var seguimiento = db.Seguimiento.Include(s => s.Estado).Include(s => s.Estado1).Include(s => s.Responsable).Include(s => s.Solicitud);
            return View(seguimiento.ToList());
        }

        // GET: Seguimientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seguimiento seguimiento = db.Seguimiento.Find(id);
            if (seguimiento == null)
            {
                return HttpNotFound();
            }
            return View(seguimiento);
        }

        // GET: Seguimientoes/Create
        public ActionResult Create()
        {
            ViewBag.IdEstadoAnterior = new SelectList(db.Estado, "IdEstado", "Nombre");
            ViewBag.IdEstadoNuevo = new SelectList(db.Estado, "IdEstado", "Nombre");
            ViewBag.IdResponsable = new SelectList(db.Responsable, "IdResponsable", "Nombre");
            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "Descripcion");
            return View();
        }

        // POST: Seguimientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSeguimiento,IdSolicitud,IdResponsable,IdEstadoAnterior,IdEstadoNuevo,Comentario,Fecha")] Seguimiento seguimiento)
        {
            // Asignar automáticamente la fecha y hora actual
            seguimiento.Fecha = DateTime.Now;

            // Asignar un valor por defecto a IdEstadoAnterior para evitar el error de base de datos
            seguimiento.IdEstadoAnterior = 2; // Cambia 1 por el ID del estado inicial que corresponda

            if (ModelState.IsValid)
            {
                db.Seguimiento.Add(seguimiento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            // Si falla, recarga los desplegables...
            ViewBag.IdEstadoAnterior = new SelectList(db.Estado, "IdEstado", "Nombre", seguimiento.IdEstadoAnterior);
            ViewBag.IdEstadoNuevo = new SelectList(db.Estado, "IdEstado", "Nombre", seguimiento.IdEstadoNuevo);
            ViewBag.IdResponsable = new SelectList(db.Responsable, "IdResponsable", "Nombre", seguimiento.IdResponsable);
            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "Descripcion", seguimiento.IdSolicitud);
            return View(seguimiento);
        }

        // GET: Seguimientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seguimiento seguimiento = db.Seguimiento.Find(id);
            if (seguimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEstadoAnterior = new SelectList(db.Estado, "IdEstado", "Nombre", seguimiento.IdEstadoAnterior);
            ViewBag.IdEstadoNuevo = new SelectList(db.Estado, "IdEstado", "Nombre", seguimiento.IdEstadoNuevo);
            ViewBag.IdResponsable = new SelectList(db.Responsable, "IdResponsable", "Nombre", seguimiento.IdResponsable);
            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "Descripcion", seguimiento.IdSolicitud);
            return View(seguimiento);
        }

        // POST: Seguimientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSeguimiento,IdSolicitud,IdResponsable,IdEstadoAnterior,IdEstadoNuevo,Comentario,Fecha")] Seguimiento seguimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(seguimiento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdEstadoAnterior = new SelectList(db.Estado, "IdEstado", "Nombre", seguimiento.IdEstadoAnterior);
            ViewBag.IdEstadoNuevo = new SelectList(db.Estado, "IdEstado", "Nombre", seguimiento.IdEstadoNuevo);
            ViewBag.IdResponsable = new SelectList(db.Responsable, "IdResponsable", "Nombre", seguimiento.IdResponsable);
            ViewBag.IdSolicitud = new SelectList(db.Solicitud, "IdSolicitud", "Descripcion", seguimiento.IdSolicitud);
            return View(seguimiento);
        }

        // GET: Seguimientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Seguimiento seguimiento = db.Seguimiento.Find(id);
            if (seguimiento == null)
            {
                return HttpNotFound();
            }
            return View(seguimiento);
        }

        // POST: Seguimientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Seguimiento seguimiento = db.Seguimiento.Find(id);
            db.Seguimiento.Remove(seguimiento);
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
