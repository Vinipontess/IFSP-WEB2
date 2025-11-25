using Models;
//Vinicius Pontes e Eduardo Barbosa

namespace MVC.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly HttpClient _http;

        public TarefaRepository(HttpClient http)
        {
            _http = http;
        }
        public async Task<IEnumerable<Tarefa>> GetTarefasAsync(CancellationToken cancellationToken = default)
        {
            var response = await _http.GetAsync("api/tarefa", cancellationToken);
            response.EnsureSuccessStatusCode();
            var tarefas = await response.Content.ReadFromJsonAsync<IEnumerable<Tarefa>>(cancellationToken: cancellationToken);
            return tarefas ?? Enumerable.Empty<Tarefa>();
        }
        public async Task<IEnumerable<Tarefa>> GetTarefasConcluidas(CancellationToken cancellationToken = default)
        {
            var response = await _http.GetAsync("api/tarefa/concluidas", cancellationToken);
            response.EnsureSuccessStatusCode();
            var tarefas = await response.Content.ReadFromJsonAsync<IEnumerable<Tarefa>>(cancellationToken: cancellationToken);
            return tarefas ?? Enumerable.Empty<Tarefa>();
        }

        public async Task<IEnumerable<Tarefa>> GetTarefasPendentes(CancellationToken cancellationToken = default)
        {
            var response = await _http.GetAsync("api/tarefa/pendentes", cancellationToken);
            response.EnsureSuccessStatusCode();
            var tarefas = await response.Content.ReadFromJsonAsync<IEnumerable<Tarefa>>(cancellationToken: cancellationToken); 
            return tarefas ?? Enumerable.Empty<Tarefa>(); 
        }

        public async Task<Tarefa> GetTarefaByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = await _http.GetAsync($"api/tarefa/{id}", cancellationToken);
            response.EnsureSuccessStatusCode();
            var tarefa = await response.Content.ReadFromJsonAsync<Tarefa>(cancellationToken: cancellationToken);
            return tarefa!;
        }

        public async Task<bool> AddTarefaAsync(Tarefa tarefa, CancellationToken cancellationToken = default)
        {
            var response = await _http.PostAsJsonAsync("api/tarefa", tarefa, cancellationToken);
            response.EnsureSuccessStatusCode();
            var createdTarefa = await response.Content.ReadFromJsonAsync<Tarefa>(cancellationToken: cancellationToken);
            
            if (createdTarefa != null)
                return true;
            else 
                return false;
        }

        public Task<bool> DeleteTarefaAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = _http.DeleteAsync($"api/tarefa/{id}", cancellationToken);
            response.Result.EnsureSuccessStatusCode();
            return Task.FromResult(true);
        }

        public Task<IEnumerable<Tarefa>> GetTarefaByTituloAsync(string titulo, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
        public Task UpdateTarefaAsync(Tarefa tarefa, CancellationToken cancellationToken = default)
        {
            var response = _http.PutAsJsonAsync($"api/tarefa/{tarefa.Id}", tarefa, cancellationToken);
            response.Result.EnsureSuccessStatusCode();
            return Task.CompletedTask;
        }

        public Task UpdateTarefaStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            var response = _http.PutAsJsonAsync($"api/tarefa/{id}/altera-status", id, cancellationToken);
            response.Result.EnsureSuccessStatusCode();
            return Task.CompletedTask;  
        }
    }
}
