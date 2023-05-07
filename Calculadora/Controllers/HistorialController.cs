using Calculadora.Intermediario;
using Calculadora.Repository;
using Microsoft.AspNetCore.Mvc;

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
            return View(_repository.GetOperationById(Estatico.IdConectado));
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
