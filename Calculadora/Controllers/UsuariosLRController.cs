
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Calculadora.Data;
using Calculadora.Models;
using Calculadora.Intermediario;
using Calculadora.Repository;


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
                    if (ModelState.IsValid)
                    {
                        _repository.AddUserWithPasswd(user);
                        Estatico.IdConectado = _repository.GetIdByUser(user);
                        Estatico.UserName = user.NombreUsuario;
                        return Redirect("/Home/CalculadoraCon");
                    }
                    else
                    {
                        return RedirectToAction("Register", new { user = user });

                    }
                }


            }
            catch
            {
                Estatico.IdConectado = 0;
                Estatico.UserName = "";
                return RedirectToAction("Register", new { mensaje = "NOADD" });
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
                            return View("Login", "error");
                        }
                        else
                        {
                            Estatico.IdConectado = 0;
                            Estatico.UserName = "";
                            return View("Login", "noLogin");
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
                        return View("Login", "error");
                    }
                }

            }
            catch (Exception)
            {

                Estatico.IdConectado = 0;
                Estatico.UserName = "";
                return View("Login", "error");

            }

        }

        public IActionResult Register(string? mensaje, Usuario? user)
        {
            if (Estatico.IdConectado == 0)
            {
                var mensajes = mensaje;
                ViewBag.Message = mensajes;
                return View(user);
            }
            else
            {
                return Redirect("WatchUserAccount");

            }
        }

        public IActionResult Conectarse()
        {
            if (Estatico.IdConectado == 0)
            {
                return View("Login");
            }
            else
            {
                return View("WatchUserAccount");
            }

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
            return Redirect("/Home/CalculadoraCon");
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