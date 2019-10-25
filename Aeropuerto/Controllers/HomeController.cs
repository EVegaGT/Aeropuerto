using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Aeropuerto.Models;
using Domain.Services;

namespace Aeropuerto.Controllers
{
    public class HomeController : Controller
    {
        private IClienteService _clienteService;

        public HomeController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Vuelos()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Cliente()
        {
            ViewData["Message"] = "Your contact page.";
            var cliente = _clienteService.GetAll();

            return View(cliente);
        }
       
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
