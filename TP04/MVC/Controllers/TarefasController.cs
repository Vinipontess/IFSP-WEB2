using Microsoft.AspNetCore.Mvc;
using Models;
using MVC.Repositories;

//Vinicius Pontes e Eduardo Barbosa
namespace MVC.Controllers
{
    public class TarefasController : Controller
    {
        private readonly ITarefaRepository _tarefaRepository;
        public TarefasController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        public IActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar(Tarefa tarefa)
        {
            if (!ModelState.IsValid)
            {
                return View(tarefa);
            }

            var tarefaCompleta = new Tarefa
            {
                Titulo = tarefa.Titulo,
                Descricao = tarefa.Descricao,
                Concluida = false,
                DataCriacao = DateTime.Now
            };

            try
            {
                await _tarefaRepository.AddTarefaAsync(tarefaCompleta);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao salvar a tarefa: " + ex.Message);
                return View(tarefa);
            }
        }

        public IActionResult Editar(int id)
        {
            var tarefa = _tarefaRepository.GetTarefaByIdAsync(id).Result;
            if (tarefa == null)
                return NotFound();

            return View(tarefa);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Tarefa tarefa)
        {
            if (!ModelState.IsValid)
                return View(tarefa);

            try
            {
                Tarefa tarefaAntiga = await _tarefaRepository.GetTarefaByIdAsync(tarefa.Id);

                Tarefa tarefaEditada = new Tarefa
                {
                    Id = tarefa.Id,
                    Titulo = tarefa.Titulo,
                    Descricao = tarefa.Descricao,
                    Concluida = tarefaAntiga.Concluida,
                    DataCriacao = tarefaAntiga.DataCriacao
                };

                await _tarefaRepository.UpdateTarefaAsync(tarefaEditada);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao editar a tarefa: " + ex.Message);
                return View(tarefa);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Excluir(int id)
        {
            try
            {
                await _tarefaRepository.DeleteTarefaAsync(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao excluir a tarefa: " + ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AlterarStatus(int id)
        {
            try
            {
                await _tarefaRepository.UpdateTarefaStatusAsync(id);
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Erro ao alterar o status da tarefa: " + ex.Message);
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
