using Questao5.Domain.Entities;
using Questao5.Domain.Enums;
using Questao5.DTOs;

namespace Questao5.Services
{
    public interface ITarefaService
    {
        Tarefa Criar(CriarTarefaRequest request);

        Tarefa BuscarPorId(Guid id);

        IEnumerable<Tarefa> ListarTodas();

        IEnumerable<Tarefa> ListarAtivas();

        IEnumerable<Tarefa> ListarConcluidasPorPeriodo(
            DateTime dataInicio,
            DateTime dataFim
        );

        Tarefa AtualizarStatus(
            Guid id,
            StatusTarefa novoStatus
        );
    }
}