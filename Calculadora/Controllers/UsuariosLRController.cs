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
                if (Estatico.IdConectado != 0 && user == null)
                {
                    return Redirect("WatchUserAccount");
                }
                else
                {
                    _repository.AddUserWithPasswd(user);
                    Estatico.IdConectado = _repository.GetIdByUser(user);
                    Estatico.UserName = user.NombreUsuario;
                    return Redirect("/Home/CalculadoraCon");
                }



            }
            catch
            {
                Estatico.IdConectado = 0;
                Estatico.UserName = "";
                return RedirectToAction("_register", new RouteValueDictionary { { "msjs", "NOADD" } });
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
                        if (!_repository.LoginName(user.NombreUsuario))
                        {
                            Estatico.IdConectado = 0;
                            Estatico.UserName = "";
                            string msjs = "error";
                            return RedirectToAction("Conectarse", new RouteValueDictionary { { "msjs", msjs } });
                        }
                        else
                        {
                            Estatico.IdConectado = 0;
                            Estatico.UserName = "";
                            string msjs = "noLogin";
                            return RedirectToAction("Conectarse", new RouteValueDictionary { { "msjs", msjs } });
                        }
                    }
                    else
                    {
                        Estatico.IdConectado = _repository.GetIdByUser(user);
                        Estatico.UserName = _repository.GetUserByID().NombreUsuario;
                        return Redirect("/Home/CalculadoraCon");
                    }
                }
                else
                {
                    if (Estatico.IdConectado != 0)
                    {
                        return Redirect("WatchUserAccount");
                    }
                    else
                    {
                        Estatico.IdConectado = 0;
                        Estatico.UserName = "";
                        string msjs = "error";
                        return RedirectToAction("Conectarse", new RouteValueDictionary { { "msjs", msjs } });
                    }
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
            return View(_repository.GetUserByID());
        }
        public IActionResult Editar()
        {
            return View("Editar", _repository.GetUserByID());
        }
        public IActionResult DeleteConfirm()
        {
            _repository.DeleteUser();
            Estatico.UserName = "";
            Estatico.IdConectado = 0;
            return RedirectToAction("CalculadoraCon", "Home");
        }
        public IActionResult ConfirmarEdicion(Usuario user)
        {
            Usuario id = _repository.GetUserByID();
            id.NombreUsuario = user.NombreUsuario;
            id.Email = user.Email;
            id.Password = user.Password;
            _repository.EditarUsuario(id);
            return Redirect("WatchUserAccount");

        }

    }
}