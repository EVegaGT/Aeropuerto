using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Services;

namespace Api.Controllers
{
    public class ReservasController : Controller
    {
        private AeEntities db = new AeEntities();
        private readonly IReservaService _reservaService;
        private readonly IClienteService _clienteService;

        public ReservasController(IReservaService reservaService, IClienteService clienteService)
        {
            _reservaService = reservaService;
            _clienteService = clienteService;
        }

        // GET: Reservas
        public ActionResult Index()
        {
            var reserva = _reservaService.GetAll();
            return View(reserva);
        }

        // GET: Reservas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reserva = _reservaService.GetReserva((int) id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // GET: Reservas/Create
        public ActionResult Create()
        {
            var clientes = _clienteService.GetAll();
            ViewBag.Id_cliente = new SelectList(clientes, "Id_cliente", "Nombre");
            ViewBag.Id_vuelo = new SelectList(db.Vueloes, "Id_vuelo", "Id_vuelo");
            return View();
        }

        // POST: Reservas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_reserva,Id_vuelo,Asientos,Id_cliente")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _reservaService.Create(reserva);
                return RedirectToAction("Index");
            }
            var clientes = _clienteService.GetAll();
            ViewBag.Id_cliente = new SelectList(clientes, "Id_cliente", "Nombre", reserva.Id_cliente);
            ViewBag.Id_vuelo = new SelectList(db.Vueloes, "Id_vuelo", "Id_vuelo", reserva.Id_vuelo);
            return View(reserva);
        }

        // GET: Reservas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reserva = _reservaService.GetReserva((int)id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            var clientes = _clienteService.GetAll();
            ViewBag.Id_cliente = new SelectList(clientes, "Id_cliente", "Nombre", reserva.Id_cliente);
            ViewBag.Id_vuelo = new SelectList(db.Vueloes, "Id_vuelo", "Id_vuelo", reserva.Id_vuelo);
            return View(reserva);
        }

        // POST: Reservas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_reserva,Id_vuelo,Asientos,Id_cliente")] Reserva reserva)
        {
            if (ModelState.IsValid)
            {
                _reservaService.Edit(reserva);
                return RedirectToAction("Index");
            }
            var clientes = _clienteService.GetAll();
            ViewBag.Id_cliente = new SelectList(clientes, "Id_cliente", "Nombre", reserva.Id_cliente);
            ViewBag.Id_vuelo = new SelectList(db.Vueloes, "Id_vuelo", "Id_vuelo", reserva.Id_vuelo);
            return View(reserva);
        }

        // GET: Reservas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var reserva = _reservaService.GetReserva((int)id);
            if (reserva == null)
            {
                return HttpNotFound();
            }
            return View(reserva);
        }

        // POST: Reservas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var reserva = _reservaService.GetReserva((int)id);
            _reservaService.Delete(reserva);
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
