using Models;
//Vinicius Pontes e Eduardo Barbosa

namespace MVC.Repositories
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> GetTarefasAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Tarefa>> GetTarefasConcluidas(CancellationToken cancellationToken = default);
        Task<IEnumerable<Tarefa>> GetTarefasPendentes(CancellationToken cancellationToken = default);
        Task<Tarefa> GetTarefaByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<IEnumerable<Tarefa>> GetTarefaByTituloAsync(string titulo, CancellationToken cancellationToken = default);
        Task<bool>AddTarefaAsync(Tarefa tarefa, CancellationToken cancellationToken = default);
        Task<bool> DeleteTarefaAsync(int id, CancellationToken cancellationToken = default);
        Task UpdateTarefaAsync(Tarefa tarefa, CancellationToken cancellationToken = default);
        Task UpdateTarefaStatusAsync(int id, CancellationToken cancellationToken = default);
    }
}
