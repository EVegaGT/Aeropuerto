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
    public class TarjetaEmbarquesController : Controller
    {
        private AeEntities db = new AeEntities();
        private readonly IEmbarcacionService _embarcacionService;
        private readonly IReservaService _reservaService;
        private readonly IClienteService _clienteService;

        public TarjetaEmbarquesController(IEmbarcacionService embarcacionService, IReservaService reservaService, IClienteService clienteService)
        {
            _embarcacionService = embarcacionService;
            _reservaService = reservaService;
            _clienteService = clienteService;
        }
        // GET: TarjetaEmbarques
        public ActionResult Index()
        {
            var tarjetaEmbarques = _embarcacionService.GetAll();
            return View(tarjetaEmbarques.ToList());
        }

        // GET: TarjetaEmbarques/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tarjetaEmbarque = _embarcacionService.GetTarjetaEmbarque((int)id);
            if (tarjetaEmbarque == null)
            {
                return HttpNotFound();
            }
            return View(tarjetaEmbarque);
        }

        // GET: TarjetaEmbarques/Create
        public ActionResult Create()
        {
            var clientes = _clienteService.GetAll();
            var reservas = _reservaService.GetAll();
            ViewBag.Id_cliente = new SelectList(clientes, "Id_cliente", "Nombre");
            ViewBag.Id_Reserva = new SelectList(reservas, "Id_reserva", "Id_reserva");
            return View();
        }

        // POST: TarjetaEmbarques/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_tarjetaEmbarque,Id_Reserva,Fecha,Id_cliente")] TarjetaEmbarque tarjetaEmbarque)
        {
            if (ModelState.IsValid)
            {
                _embarcacionService.Create(tarjetaEmbarque);
                return RedirectToAction("Index");
            }
            var clientes = _clienteService.GetAll();
            var reservas = _reservaService.GetAll();
            ViewBag.Id_cliente = new SelectList(clientes, "Id_cliente", "Nombre", tarjetaEmbarque.Id_cliente);
            ViewBag.Id_Reserva = new SelectList(reservas, "Id_reserva", "Id_reserva", tarjetaEmbarque.Id_Reserva);
            return View(tarjetaEmbarque);
        }

        // GET: TarjetaEmbarques/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tarjetaEmbarque = _embarcacionService.GetTarjetaEmbarque((int)id);
            if (tarjetaEmbarque == null)
            {
                return HttpNotFound();
            }
            var clientes = _clienteService.GetAll();
            var reservas = _reservaService.GetAll();
            ViewBag.Id_cliente = new SelectList(clientes, "Id_cliente", "Nombre", tarjetaEmbarque.Id_cliente);
            ViewBag.Id_Reserva = new SelectList(reservas, "Id_reserva", "Id_reserva", tarjetaEmbarque.Id_Reserva);
            return View(tarjetaEmbarque);
        }

        // POST: TarjetaEmbarques/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_tarjetaEmbarque,Id_Reserva,Fecha,Id_cliente")] TarjetaEmbarque tarjetaEmbarque)
        {
            if (ModelState.IsValid)
            {
                _embarcacionService.Edit(tarjetaEmbarque);
                return RedirectToAction("Index");
            }

            var clientes = _clienteService.GetAll();
            var reservas = _reservaService.GetAll();
            ViewBag.Id_cliente = new SelectList(clientes, "Id_cliente", "Nombre", tarjetaEmbarque.Id_cliente);
            ViewBag.Id_Reserva = new SelectList(reservas, "Id_reserva", "Id_reserva", tarjetaEmbarque.Id_Reserva);
            return View(tarjetaEmbarque);
        }

        // GET: TarjetaEmbarques/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tarjetaEmbarque = _embarcacionService.GetTarjetaEmbarque((int)id);
            if (tarjetaEmbarque == null)
            {
                return HttpNotFound();
            }
            return View(tarjetaEmbarque);
        }

        // POST: TarjetaEmbarques/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var tarjetaEmbarque = _embarcacionService.GetTarjetaEmbarque(id);
            _embarcacionService.Delete(tarjetaEmbarque);
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
