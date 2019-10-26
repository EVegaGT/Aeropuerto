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
    public class AvionsController : Controller
    {
        private readonly IAvionRepository _avionRepository;

        public AvionsController(IAvionRepository avionRepository)
        {
            _avionRepository = avionRepository;
        }

        // GET: Avions
        public ActionResult Index()
        {
            var aviones = _avionRepository.GetAll();
            return View(aviones);
        }

        // GET: Avions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var avion = _avionRepository.GetAvion((int)id);
            if (avion == null)
            {
                return HttpNotFound();
            }
            return View(avion);
        }

        // GET: Avions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Avions/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_avion,Nombre,Numero_plazas,Max_Columna,Max_Fila")] Avion avion)
        {
            if (ModelState.IsValid)
            {
                _avionRepository.Create(avion);
                return RedirectToAction("Index");
            }

            return View(avion);
        }

        // GET: Avions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var avion = _avionRepository.GetAvion((int)id);
            if (avion == null)
            {
                return HttpNotFound();
            }
            return View(avion);
        }

        // POST: Avions/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_avion,Nombre,Numero_plazas,Max_Columna,Max_Fila")] Avion avion)
        {
            if (ModelState.IsValid)
            {
                _avionRepository.Edit(avion);
                return RedirectToAction("Index");
            }
            return View(avion);
        }

        // GET: Avions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var avion = _avionRepository.GetAvion((int)id);
            if (avion == null)
            {
                return HttpNotFound();
            }
            return View(avion);
        }

        // POST: Avions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var avion = _avionRepository.GetAvion(id);
            _avionRepository.Delete(avion);
            return RedirectToAction("Index");
        }
    }
}
