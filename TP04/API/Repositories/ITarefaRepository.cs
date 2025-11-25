using Models;

//Vinicius Pontes e Eduardo Barbosa
namespace WEB_API.Repositories
{
    public interface ITarefaRepository
    {
        Task<IEnumerable<Tarefa>> GetTarefas();
        Task<IEnumerable<Tarefa>> GetTarefasConcluidas();
        Task<IEnumerable<Tarefa>> GetTarefasPendentes();
        Task<Tarefa> GetTarefaById(int id);
        Task<IEnumerable<Tarefa>> GetTarefaByTitulo(string titulo); 
        Task AddTarefa(Tarefa tarefa);
        Task UpdateTarefa(Tarefa tarefa);
        Task UpdateStatusTarefa(int id);
        Task DeleteTarefa(int id);
    }
}
