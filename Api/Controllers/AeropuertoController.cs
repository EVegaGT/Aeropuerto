using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Repositories;

namespace Api.Controllers
{
    public class AeropuertoController : Controller
    {
        private AeEntities db = new AeEntities();
        private readonly IAeropuertoRepository _aeropuertoRepository;
        private readonly ILocalidadRepository _localidadRepository;

        public AeropuertoController(IAeropuertoRepository aeropuertoRepository, ILocalidadRepository localidadRepository)
        {
            _aeropuertoRepository = aeropuertoRepository;
            _localidadRepository = localidadRepository;
        }


        // GET: Aeropuerto
        public ActionResult Index()
        {
            var aeropuertoes = _aeropuertoRepository.GetAll();
            return View(aeropuertoes.ToList());
        }

        // GET: Aeropuerto/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aeropuerto = _aeropuertoRepository.GetAeropuerto((int)id);
            if (aeropuerto == null)
            {
                return HttpNotFound();
            }
            return View(aeropuerto);
        }

        // GET: Aeropuerto/Create
        public ActionResult Create()
        {
            ViewBag.Id_localidad = new SelectList(db.Localidads, "Id_localidad", "Nombre");
            return View();
        }

        // POST: Aeropuerto/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_aeropuerto,Nombre,Id_localidad")] Aeropuerto aeropuerto)
        {
            if (ModelState.IsValid)
            {
                _aeropuertoRepository.Create(aeropuerto);
                return RedirectToAction("Index");
            }

            ViewBag.Id_localidad = new SelectList(db.Localidads, "Id_localidad", "Nombre", aeropuerto.Id_localidad);
            return View(aeropuerto);
        }

        // GET: Aeropuerto/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var aeropuerto = _aeropuertoRepository.GetAeropuerto((int)id);
            if (aeropuerto == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_localidad = new SelectList(db.Localidads, "Id_localidad", "Nombre", aeropuerto.Id_localidad);
            return View(aeropuerto);
        }

        // POST: Aeropuerto/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_aeropuerto,Nombre,Id_localidad")] Aeropuerto aeropuerto)
        {
            if (ModelState.IsValid)
            {
                _aeropuertoRepository.Edit(aeropuerto);
                return RedirectToAction("Index");
            }
            ViewBag.Id_localidad = new SelectList(db.Localidads, "Id_localidad", "Nombre", aeropuerto.Id_localidad);
            return View(aeropuerto);
        }

        // GET: Aeropuerto/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aeropuerto aeropuerto = db.Aeropuertoes.Find(id);
            if (aeropuerto == null)
            {
                return HttpNotFound();
            }
            return View(aeropuerto);
        }

        // POST: Aeropuerto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var aeropuerto = _aeropuertoRepository.GetAeropuerto(id);
            _aeropuertoRepository.Delete(aeropuerto);
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
