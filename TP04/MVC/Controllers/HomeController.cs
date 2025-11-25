using Microsoft.AspNetCore.Mvc;
using Models;
using MVC.Repositories;
using System.Diagnostics;
//Vinicius Pontes e Eduardo Barbosa
namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITarefaRepository _tarefaRepository;
        public HomeController(ILogger<HomeController> logger, ITarefaRepository tarefaRepository)
        {
            _logger = logger;
            _tarefaRepository = tarefaRepository;
        }

        [HttpGet]
        public IActionResult Index(string status_tarefa, string titulo)
        {
            var tarefasCompletas = _tarefaRepository.GetTarefasConcluidas().Result;
            var tarefasPendentes = _tarefaRepository.GetTarefasPendentes().Result;

            if (!string.IsNullOrEmpty(titulo))
            {
                tarefasCompletas = tarefasCompletas.Where(t => t.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase)).ToList();
                tarefasPendentes = tarefasPendentes.Where(t => t.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            ViewBag.Status = string.IsNullOrEmpty(status_tarefa) ? "pendentes" : status_tarefa;
            return View(new Tuple<IEnumerable<Tarefa>, IEnumerable<Tarefa>>(tarefasCompletas, tarefasPendentes));
        }

        public IActionResult Creditos()
        {
            return View();
        }
    }
}


