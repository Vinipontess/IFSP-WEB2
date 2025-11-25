//Vinicius Pontes e Eduardo Barbosa
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Models;
using WEB_API.Repositories;

namespace WEB_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepository _tarefaRepository;

        public TarefaController(ITarefaRepository tarefaRepository)
        {
            _tarefaRepository = tarefaRepository;
        }

        // GET: api/tarefa
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            var tarefas = await _tarefaRepository.GetTarefas();
            return Ok(tarefas);
        }

        // GET: api/tarefa/concluidas
        [HttpGet("concluidas")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefasConcluidas()
        {
            var tarefasConcluidas = await _tarefaRepository.GetTarefasConcluidas();
            return Ok(tarefasConcluidas);
        }

        // GET: api/tarefa/pendentes
        [HttpGet("pendentes")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefasPendentes()
        {
            var tarefasPendentes = await _tarefaRepository.GetTarefasPendentes();
            return Ok(tarefasPendentes);
        }


        // GET: api/tarefa/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tarefa>> GetById(int id)
        {
            Tarefa tarefa = await _tarefaRepository.GetTarefaById(id);
            if (tarefa == null)
                return NotFound();
            return Ok(tarefa);
        }

        // GET: api/tarefa/search?titulo=texto
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetByTitulo([FromQuery] string titulo)
        {
            var resultados = await _tarefaRepository.GetTarefaByTitulo(titulo);
            return Ok(resultados);
        }

        // POST: api/tarefa
        [HttpPost]
        public async Task<ActionResult<Tarefa>> Create([FromBody] Tarefa tarefa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _tarefaRepository.AddTarefa(tarefa);

            return CreatedAtAction(nameof(GetById), new { id = tarefa.Id }, tarefa);
        }

        // PUT: api/tarefa/{id}
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] Tarefa tarefa)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != tarefa.Id)
                return BadRequest("Id do caminho diferente do corpo da requisição.");

            var tarefas = await _tarefaRepository.GetTarefas();
            if (!tarefas.Any(t => t.Id == id))
                return NotFound();

            await _tarefaRepository.UpdateTarefa(tarefa);
            return NoContent();
        }

        // PUT: api/tarefa/{id}/altera-status
        [HttpPut("{id:int}/altera-status")]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var tarefa = await _tarefaRepository.GetTarefaById(id);
            if (tarefa == null)
                return NotFound();
            await _tarefaRepository.UpdateStatusTarefa(tarefa.Id);
            return NoContent();
        }

        // DELETE: api/tarefa/{id}
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var tarefas = await _tarefaRepository.GetTarefaById(id);
            if (tarefas == null)
                return NotFound();

            await _tarefaRepository.DeleteTarefa(id);
            return NoContent();
        }
    }
}
