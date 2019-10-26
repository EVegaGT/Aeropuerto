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
    public class PaisController : Controller
    {
        private AeEntities db = new AeEntities();
        private readonly IPaisService _paisService;

        public PaisController(IPaisService paisService)
        {
            _paisService = paisService;
        }

        // GET: Pais
        public ActionResult Index()
        {
            var paieses = _paisService.GetAll();
            return View(paieses);
        }

        // GET: Pais/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pai = _paisService.GetPai((int)id);
            if (pai == null)
            {
                return HttpNotFound();
            }
            return View(pai);
        }

        // GET: Pais/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pais/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id_pais,Nombre")] Pai pai)
        {
            if (ModelState.IsValid)
            {
                _paisService.Create(pai);
                return RedirectToAction("Index");
            }

            return View(pai);
        }

        // GET: Pais/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pai = _paisService.GetPai((int)id);
            if (pai == null)
            {
                return HttpNotFound();
            }
            return View(pai);
        }

        // POST: Pais/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id_pais,Nombre")] Pai pai)
        {
            if (ModelState.IsValid)
            {
                _paisService.Edit(pai);
                return RedirectToAction("Index");
            }
            return View(pai);
        }

        // GET: Pais/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pai = _paisService.GetPai((int)id);
            if (pai == null)
            {
                return HttpNotFound();
            }
            return View(pai);
        }

        // POST: Pais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var pai = _paisService.GetPai((int)id);
            _paisService.Delete(pai);
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
