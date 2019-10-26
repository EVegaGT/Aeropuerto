using Domain.Services;
using System.Web.Mvc;
using Domain;
namespace Api.Controllers
{
    public class ClienteController : Controller
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }
        
        // GET: Cliente
        public ActionResult Index()
        {
            var clientes = _clienteService.GetAll();
            return View(clientes);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            var cliente = _clienteService.GetCliente(id);
            return View(cliente);
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                var cliente = new Cliente
                {
                    DPI = int.Parse(collection["DPI"]),
                    Nombre = collection["Nombre"],
                    Apellido = collection["Apellido"],
                    Direccion = collection["Direccion"],
                    Telefono = int.Parse(collection["Telefono"]),
                    Tarjeta = int.Parse(collection["Tarjeta"])
                };
                _clienteService.Create(cliente);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            var cliente = _clienteService.GetCliente(id);
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here]
                var cliente = new Cliente
                {
                    Id_cliente = id,
                    DPI = int.Parse(collection["DPI"]),
                    Nombre = collection["Nombre"],
                    Apellido = collection["Apellido"],
                    Direccion = collection["Direccion"],
                    Telefono = int.Parse(collection["Telefono"]),
                    Tarjeta = int.Parse(collection["Tarjeta"])
                };
                _clienteService.Edit(cliente);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            var cliente = _clienteService.GetCliente(id);
            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
