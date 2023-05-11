
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;
using Calculadora.Data;
using Calculadora.Models;
using Calculadora.Intermediario;
using Calculadora.Repository;


namespace Calculadora.Controllers
{
    public class HistorialController : Controller
    {

        private ICalculadoraRepository _repository;
        private IWebHostEnvironment _environment;

        public HistorialController(ICalculadoraRepository repository, IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public IActionResult VerOperaciones()
        {
            return View("VerOperaciones",_repository.GetOperationById());
        }

        public IActionResult Delete(int id)
        {
            _repository.DeleteOperation(id);
            return RedirectToAction("VerOperaciones","/Historial");
        }

        public void DeleteAll(int id)
        {
            _repository.DeleteOperation(id);
        }
       
    }
}
