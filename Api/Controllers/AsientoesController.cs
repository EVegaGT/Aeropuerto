using System.Linq;
using System.Net;
using System.Web.Mvc;
using Domain;
using Domain.Repositories;

namespace Api.Controllers
{
    public class AsientoesController : Controller
    {
        private readonly IAvionRepository _avionRepository;
        private readonly IAsientoRepository _asientoRepository;

        public AsientoesController(IAsientoRepository asientoRespository, IAvionRepository avionRepository)
        {
            _asientoRepository = asientoRespository;
            _avionRepository = avionRepository;
        }

        // GET: Asientoes
        public ActionResult Index()
        {
            var asientoes = _asientoRepository.GetAll();
            return View(asientoes.ToList());
        }

        // GET: Asientoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var asiento = _asientoRepository.GetAsiento((int)id);
            if (asiento == null)
            {
                return HttpNotFound();
            }
            return View(asiento);
        }

        // GET: Asientoes/Create
        public ActionResult Create()
        {
            var aviones = _avionRepository.GetAll();
            ViewBag.Id_Avion = new SelectList(aviones, "Id_avion", "Nombre");
            return View();
        }

        // POST: Asientoes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_asiento,Id_Avion,Fila,Columna,Planta")] Asiento asiento)
        {
            if (ModelState.IsValid)
            {
                _asientoRepository.Create(asiento);
                return RedirectToAction("Index");
            }

            var aviones = _avionRepository.GetAll();
            ViewBag.Id_Avion = new SelectList(aviones, "Id_avion", "Nombre", asiento.Id_Avion);
            return View(asiento);
        }

        // GET: Asientoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var asiento = _asientoRepository.GetAsiento((int)id);
            if (asiento == null)
            {
                return HttpNotFound();
            }
            var aviones = _avionRepository.GetAll();
            ViewBag.Id_Avion = new SelectList(aviones, "Id_avion", "Nombre", asiento.Id_Avion);
            return View(asiento);
        }

        // POST: Asientoes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_asiento,Id_Avion,Fila,Columna,Planta")] Asiento asiento)
        {
            if (ModelState.IsValid)
            {
                _asientoRepository.Edit(asiento);
                return RedirectToAction("Index");
            }
            var aviones = _avionRepository.GetAll();
            ViewBag.Id_Avion = new SelectList(aviones, "Id_avion", "Nombre", asiento.Id_Avion);
            return View(asiento);
        }

        // GET: Asientoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var asiento = _asientoRepository.GetAsiento((int)id);
            if (asiento == null)
            {
                return HttpNotFound();
            }
            return View(asiento);
        }

        // POST: Asientoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var asiento = _asientoRepository.GetAsiento((int)id);
            _asientoRepository.Delete(asiento);
            return RedirectToAction("Index");
        }
    }
}
