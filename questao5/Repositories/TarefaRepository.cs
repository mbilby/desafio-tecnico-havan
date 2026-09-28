using Questao5.Domain.Entities;
using Questao5.Domain.Enums;

namespace Questao5.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly List<Tarefa> _tarefas = new List<Tarefa>();

        public void Adicionar(Tarefa tarefa)
        {
            _tarefas.Add(tarefa);
        }

        public Tarefa? BuscarPorId(Guid id)
        {
            return _tarefas.FirstOrDefault(
                tarefa => tarefa.Id == id
            );
        }

        public IEnumerable<Tarefa> ListarTodas()
        {
            return _tarefas;
        }

        public IEnumerable<Tarefa> ListarAtivas()
        {
            return _tarefas.Where(
                tarefa =>
                    tarefa.Status == StatusTarefa.Pendente ||
                    tarefa.Status == StatusTarefa.EmAndamento
            );
        }

        public IEnumerable<Tarefa> ListarConcluidasPorPeriodo(
            DateTime dataInicio,
            DateTime dataFim)
        {
            return _tarefas.Where(
                tarefa =>
                    tarefa.Status == StatusTarefa.Concluida &&
                    tarefa.DataDeConclusao.HasValue &&
                    tarefa.DataDeConclusao.Value >= dataInicio &&
                    tarefa.DataDeConclusao.Value <= dataFim
            );
        }
    }
}