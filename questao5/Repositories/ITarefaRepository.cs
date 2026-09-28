using Questao5.Domain.Entities;
using Questao5.Domain.Enums;

namespace Questao5.Repositories
{
    public interface ITarefaRepository
    {
        void Adicionar(Tarefa tarefa);

        Tarefa? BuscarPorId(Guid id);

        IEnumerable<Tarefa> ListarTodas();

        IEnumerable<Tarefa> ListarAtivas();

        IEnumerable<Tarefa> ListarConcluidasPorPeriodo(
            DateTime dataInicio,
            DateTime dataFim
        );
    }
}