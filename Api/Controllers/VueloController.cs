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
    public class VueloController : Controller
    {
        private readonly IVueloRepository _vueloRepository;
        private readonly IAeropuertoRepository _aeropuertoRepository;
        private readonly IAvionRepository _avionRepository;

        public VueloController(IVueloRepository vueloRepository, IAeropuertoRepository aeropuertoRepository, IAvionRepository avionRepository)
        {
            _vueloRepository = vueloRepository;
            _aeropuertoRepository = aeropuertoRepository;
            _avionRepository = avionRepository;
        }

        // GET: Vuelo
        public ActionResult Index()
        {
            var vuelos = _vueloRepository.GetAll();
            return View(vuelos);
        }

        // GET: Vuelo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vuelo = _vueloRepository.GetVuelo((int) id);
            if (vuelo == null)
            {
                return HttpNotFound();
            }
            return View(vuelo);
        }

        // GET: Vuelo/Create
        public ActionResult Create()
        {
            var aeropuerto = _aeropuertoRepository.GetAll();
            var aviones = _avionRepository.GetAll();
            ViewBag.Aeropuerto_entrada = new SelectList(aeropuerto, "Id_aeropuerto", "Nombre");
            ViewBag.Aeropuerto_salida = new SelectList(aeropuerto, "Id_aeropuerto", "Nombre");
            ViewBag.Id_avion = new SelectList(aviones, "Id_avion", "Nombre");
            return View();
        }

        // POST: Vuelo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_vuelo,Id_avion,Aeropuerto_salida,Aeropuerto_entrada,Fecha_salida,Fecha_entrada")] Vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                _vueloRepository.Create(vuelo);
                return RedirectToAction("Index");
            }
            var aeropuerto = _aeropuertoRepository.GetAll();
            var aviones = _avionRepository.GetAll();
            ViewBag.Aeropuerto_entrada = new SelectList(aeropuerto, "Id_aeropuerto", "Nombre", vuelo.Aeropuerto_entrada);
            ViewBag.Aeropuerto_salida = new SelectList(aeropuerto, "Id_aeropuerto", "Nombre", vuelo.Aeropuerto_salida);
            ViewBag.Id_avion = new SelectList(aviones, "Id_avion", "Nombre", vuelo.Id_avion);
            return View(vuelo);
        }

        // GET: Vuelo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vuelo = _vueloRepository.GetVuelo((int)id);
            if (vuelo == null)
            {
                return HttpNotFound();
            }

            var aeropuerto = _aeropuertoRepository.GetAll();
            var aviones = _avionRepository.GetAll();
            ViewBag.Aeropuerto_entrada = new SelectList(aeropuerto, "Id_aeropuerto", "Nombre", vuelo.Aeropuerto_entrada);
            ViewBag.Aeropuerto_salida = new SelectList(aeropuerto, "Id_aeropuerto", "Nombre", vuelo.Aeropuerto_salida);
            ViewBag.Id_avion = new SelectList(aviones, "Id_avion", "Nombre", vuelo.Id_avion);
            return View(vuelo);
        }

        // POST: Vuelo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_vuelo,Id_avion,Aeropuerto_salida,Aeropuerto_entrada,Fecha_salida,Fecha_entrada")] Vuelo vuelo)
        {
            if (ModelState.IsValid)
            {
                _vueloRepository.Edit(vuelo);
                return RedirectToAction("Index");
            }

            var aeropuerto = _aeropuertoRepository.GetAll();
            var aviones = _avionRepository.GetAll();
            ViewBag.Aeropuerto_entrada = new SelectList(aeropuerto, "Id_aeropuerto", "Nombre", vuelo.Aeropuerto_entrada);
            ViewBag.Aeropuerto_salida = new SelectList(aeropuerto, "Id_aeropuerto", "Nombre", vuelo.Aeropuerto_salida);
            ViewBag.Id_avion = new SelectList(aviones, "Id_avion", "Nombre", vuelo.Id_avion);
            return View(vuelo);
        }

        // GET: Vuelo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var vuelo = _vueloRepository.GetVuelo((int)id);
            if (vuelo == null)
            {
                return HttpNotFound();
            }
            return View(vuelo);
        }

        // POST: Vuelo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var vuelo = _vueloRepository.GetVuelo(id);
            _vueloRepository.Delete(vuelo);
            return RedirectToAction("Index");
        }
    }
}
