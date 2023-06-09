﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Calculadora.Data;
using Calculadora.Models;
using Calculadora.Intermediario;
using Calculadora.Repository;

namespace CalculadoraFuncional.Controllers
{
	public class HomeController : Controller
	{

		private ICalculadoraRepository _repository;

		private IWebHostEnvironment _environment;

		public HomeController(ICalculadoraRepository context, IWebHostEnvironment environment)
		{
			_repository = context;
			_environment = environment;
		}


		public IActionResult CalculadoraCon()
		{
			if (Estatico.idOperacion!=0)
			{
				return View(_repository.GetOperacionesById(Estatico.idOperacion));
			}
			else
			{
				return View();
			}
		}

		public IActionResult Index()
		{
			Estatico.IdConectado = 0;
			Estatico.UserName = "";
			return View("CalculadoraCon");
		}


		[HttpPost]
		public IActionResult Addition(string operacion, string resultado)
		{
			if (Estatico.IdConectado != 0)
			{
				Operaciones op = new Operaciones
				{
					ExpresionMatematica = operacion.ToString(),
					Resultado = resultado,
					UsuarioId = Estatico.IdConectado
				};
				_repository.AddOperation(op);
			}
			return Json("");
		}


		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
		public IActionResult DarValor(int id)
		{
			Estatico.idOperacion = id;
            return RedirectToAction("CalculadoraCon","Home");
        }

	}
}