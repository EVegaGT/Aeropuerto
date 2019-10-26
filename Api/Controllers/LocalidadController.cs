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
using Domain.Services;

namespace Api.Controllers
{
    public class LocalidadController : Controller
    {
        private AeEntities db = new AeEntities();
        private readonly IPaisService _paisService;
        private readonly ILocalidadRepository _localidadRepository;

        public LocalidadController(IPaisService paisService, ILocalidadRepository localidadRepository)
        {
            _paisService = paisService;
            _localidadRepository = localidadRepository;
        }

        // GET: Localidad
        public ActionResult Index()
        {
            var localidades = _localidadRepository.GetAll();
            return View(localidades);
        }

        // GET: Localidad/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var localidad = _localidadRepository.GetLocalidad((int) id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

        // GET: Localidad/Create
        public ActionResult Create()
        {
            var paieses = _paisService.GetAll();
            ViewBag.Id_pais = new SelectList(paieses, "Id_pais", "Nombre");
            return View();
        }

        // POST: Localidad/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_localidad,Nombre,Id_pais")] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                _localidadRepository.Create(localidad);
                return RedirectToAction("Index");
            }

            var paieses = _paisService.GetAll();
            ViewBag.Id_pais = new SelectList(paieses, "Id_pais", "Nombre", localidad.Id_pais);
            return View(localidad);
        }

        // GET: Localidad/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var localidad = _localidadRepository.GetLocalidad((int)id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            var paieses = _paisService.GetAll();
            ViewBag.Id_pais = new SelectList(paieses, "Id_pais", "Nombre", localidad.Id_pais);
            return View(localidad);
        }

        // POST: Localidad/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_localidad,Nombre,Id_pais")] Localidad localidad)
        {
            if (ModelState.IsValid)
            {
                _localidadRepository.Edit(localidad);
                return RedirectToAction("Index");
            }
            var paieses = _paisService.GetAll();
            ViewBag.Id_pais = new SelectList(paieses, "Id_pais", "Nombre", localidad.Id_pais);
            return View(localidad);
        }

        // GET: Localidad/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var localidad = _localidadRepository.GetLocalidad((int)id);
            if (localidad == null)
            {
                return HttpNotFound();
            }
            return View(localidad);
        }

        // POST: Localidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var localidad = _localidadRepository.GetLocalidad((int)id);
            _localidadRepository.Delete(localidad);
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
