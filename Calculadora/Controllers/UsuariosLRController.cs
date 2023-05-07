﻿using Calculadora.Intermediario;
using Calculadora.Repository;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Calculadora.Controllers
{
    public class UsuariosLRController : Controller
    {

        private ICalculadoraRepository _repository;

        private IWebHostEnvironment _environment;

        public UsuariosLRController(ICalculadoraRepository context, IWebHostEnvironment environment)
        {
            _repository = context;
            _environment = environment;
        }

        public IActionResult AddUser(string email, string password)
        {
            try
            {
				_repository.AddUserWithPasswd(email, password);
				var id = _repository.GetUsuarioIDByNamePasswd(email, password);
				Estatico.IdConectado = id;
				Estatico.IsVerified = true;
				return Redirect("/Home/CalculadoraCon");
            }
            catch
            {
				Estatico.IdConectado = 0;
				Estatico.IsVerified = false;
				return RedirectToAction("Register", new RouteValueDictionary { { "msjs", "NOADD" } });
			}
		}




		public IActionResult Login(string email, string password)
        {
            try
            {
                if (email != null)
                {
                    var id = _repository.GetUsuarioIDByNamePasswd(email, password);
                    Estatico.IdConectado = id;
                    Estatico.IsVerified = true;
                    if (!_repository.LoginUserPasswd(email, password))
                    {
                        string msjs = "noLogin";
                        return RedirectToAction("Conectarse", new RouteValueDictionary { { "msjs", msjs } });
                    }
                    else
                    {
                        return Redirect("/Home/CalculadoraCon");
                    }
                }
                else
                {
                    Estatico.IdConectado = 0;
                    Estatico.IsVerified = false;
                    string msjs = "error";
                    return RedirectToAction("Conectarse", new RouteValueDictionary { { "msjs", msjs } });
                }

            }
            catch (Exception)
            {
                Estatico.IdConectado = 0;
                Estatico.IsVerified = false;
				return RedirectToAction("Conectarse", new RouteValueDictionary { { "msjs", "error" } });
			}

		}

        public IActionResult Register([FromQuery(Name = "msjs")] string? mensaje)
        {
            return PartialView("_register",mensaje);
        }

        public IActionResult Conectarse([FromQuery(Name = "msjs")] string? mensaje)
        {
            return PartialView("_login", mensaje);
        }

    }
}