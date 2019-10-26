using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.Data;
using Domain.Data.Interfaces;
using Domain.Services;

namespace Api.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClienteService _clienteService;
        protected readonly IDbContextScopeFactory DbOrgContextScopeFactory;

        public HomeController(IClienteService clienteService)
        {
            DbOrgContextScopeFactory = ObjectFactory.Container.GetInstance<IDbContextScopeFactory>();
            _clienteService = clienteService;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            var clientes = _clienteService.GetAll();
            return View(clientes);
        }
    }
}
