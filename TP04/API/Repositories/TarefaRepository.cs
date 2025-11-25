//Vinicius Pontes e Eduardo Barbosa
using Microsoft.EntityFrameworkCore;
using WEB_API.Data;
using Models;

namespace WEB_API.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext _context;

        public TarefaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddTarefa(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
        }

        public Task DeleteTarefa(int id)
        {
            Tarefa? tarefaEncontrada = _context.Tarefas.FirstOrDefault(t => t.Id == id);

            if (tarefaEncontrada == null)
                return Task.FromResult(false);

            _context.Tarefas.Remove(tarefaEncontrada);
            return _context.SaveChangesAsync();
        }

        public Task<Tarefa> GetTarefaById(int id)
        {
            return _context.Tarefas.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Tarefa>> GetTarefaByTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                return Enumerable.Empty<Tarefa>();

            string pattern = $"%{titulo}%";
            return await _context.Tarefas
                                 .Where(t => EF.Functions.Like(t.Titulo, pattern))
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetTarefas()
        {
            return await _context.Tarefas.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasConcluidas()
        {
            return await _context.Tarefas.AsNoTracking()
                                 .Where(t => t.Concluida == true)
                                 .ToListAsync();
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasPendentes()
        {
            return await _context.Tarefas.AsNoTracking()
                                 .Where(t => t.Concluida == false)
                                 .ToListAsync();    
        }

        public Task UpdateStatusTarefa(int id)
        {
            Tarefa tarefaEncontrada = _context.Tarefas.FirstOrDefault(t => t.Id == id);
            
            if (tarefaEncontrada == null)
                return Task.FromResult(false);

            tarefaEncontrada.Concluida = !tarefaEncontrada.Concluida;
            _context.Tarefas.Update(tarefaEncontrada);

            return _context.SaveChangesAsync();
        }

        public Task UpdateTarefa(Tarefa tarefa)
        {
            _context.Tarefas.Update(tarefa);
            return _context.SaveChangesAsync();
        }
    }
}
