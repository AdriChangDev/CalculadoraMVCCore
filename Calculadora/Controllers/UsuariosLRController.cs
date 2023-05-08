using Calculadora.Intermediario;
using Calculadora.Models;
using Calculadora.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        public IActionResult AddUser(Usuario user)
        {
            try
            {
                _repository.AddUserWithPasswd(user);
                Estatico.IdConectado = user.ID;
                Estatico.UserName = user.NombreUsuario;
                return Redirect("/Home/CalculadoraCon");
            }
            catch
            {
                Estatico.IdConectado = 0;
                Estatico.UserName = "";
                return RedirectToAction("Register", new RouteValueDictionary { { "msjs", "NOADD" } });
            }
        }




        public IActionResult Login(Usuario user)
        {
            try
            {
                if (user.Email != null)
                {

                    if (!_repository.LoginUserPasswd(user))
                    {
                        Estatico.IdConectado = _repository.GetIdByUser(user);
                        Estatico.UserName = user.NombreUsuario;
                        string msjs = "noLogin";
                        return RedirectToAction("Conectarse", new RouteValueDictionary { { "msjs", msjs } });
                    }
                    else
                    {
                        Estatico.IdConectado = _repository.GetIdByUser(user);
                        Estatico.UserName = _repository.GetUserByID(Estatico.IdConectado).NombreUsuario;
                        return Redirect("/Home/CalculadoraCon");
                    }
                }
                else
                {
                    Estatico.IdConectado = 0;
                    Estatico.UserName = "";
                    string msjs = "error";
                    return RedirectToAction("Conectarse", new RouteValueDictionary { { "msjs", msjs } });
                }

            }
            catch (Exception)
            {
                Estatico.IdConectado = 0;
                Estatico.UserName = "";
                return RedirectToAction("Conectarse", new RouteValueDictionary { { "msjs", "error" } });
            }

        }

        public IActionResult Register([FromQuery(Name = "msjs")] string? mensaje)
        {
            return PartialView("_register", mensaje);
        }

        public IActionResult Conectarse([FromQuery(Name = "msjs")] string? mensaje)
        {
            return PartialView("_login", mensaje);
        }
        public IActionResult WatchUserAccount()
        {
            return View(_repository.GetUserByID(Estatico.IdConectado));
        }
        public IActionResult Editar()
        {
            return View("Editar", _repository.GetUserByID(Estatico.IdConectado));
        }
        public IActionResult DeleteConfirm()
        {
            _repository.DeleteUser();
            return Redirect("/Home/Index");
        }
        public IActionResult ConfirmarEdicion(Usuario user)
        {
            Usuario id = _repository.GetUserByID(Estatico.IdConectado);
            id.NombreUsuario = user.NombreUsuario;
            id.Email = user.Email;
            id.Password = user.Password;
            _repository.EditarUsuario(id);
            return Redirect("WatchUserAccount");

        }

    }
}