using Questao5.Constantes;
using Questao5.Domain.Entities;
using Questao5.Domain.Enums;
using Questao5.DTOs;
using Questao5.Repositories;

namespace Questao5.Services
{
    public class TarefaService : ITarefaService
    {
        private readonly ITarefaRepository _repository;

        public TarefaService(ITarefaRepository repository)
        {
            _repository = repository;
        }

        public Tarefa Criar(CriarTarefaRequest request)
        {
            ValidarTitulo(request.Titulo);

            Tarefa tarefa = new Tarefa
            {
                Id = Guid.NewGuid(),
                Titulo = request.Titulo.Trim(),
                Descricao = request.Descricao.Trim(),
                DataDeCriacao = DateTime.Now,
                DataDeConclusao = null,
                Status = StatusTarefa.Pendente
            };

            _repository.Adicionar(tarefa);

            return tarefa;
        }

        public Tarefa BuscarPorId(Guid id)
        {
            Tarefa? tarefa = _repository.BuscarPorId(id);

            if (tarefa == null)
            {
                throw new KeyNotFoundException(
                    TarefaConstantes.MENSAGEM_TAREFA_NAO_ENCONTRADA
                );
            }

            return tarefa;
        }

        public IEnumerable<Tarefa> ListarTodas()
        {
            return _repository.ListarTodas();
        }

        public IEnumerable<Tarefa> ListarAtivas()
        {
            return _repository.ListarAtivas();
        }

        public IEnumerable<Tarefa> ListarConcluidasPorPeriodo(
            DateTime dataInicio,
            DateTime dataFim)
        {
            if (dataInicio > dataFim)
            {
                throw new ArgumentException(
                    TarefaConstantes.MENSAGEM_INTERVALO_INVALIDO
                );
            }

            return _repository.ListarConcluidasPorPeriodo(
                dataInicio,
                dataFim
            );
        }

        public Tarefa AtualizarStatus(
            Guid id,
            StatusTarefa novoStatus)
        {
            Tarefa tarefa = BuscarPorId(id);

            if (tarefa.Status == StatusTarefa.Concluida)
            {
                throw new InvalidOperationException(
                    TarefaConstantes.MENSAGEM_TAREFA_CONCLUIDA
                );
            }

            if (!Enum.IsDefined(typeof(StatusTarefa), novoStatus))
            {
                throw new ArgumentException(
                    TarefaConstantes.MENSAGEM_STATUS_INVALIDO
                );
            }

            tarefa.Status = novoStatus;

            if (novoStatus == StatusTarefa.Concluida)
            {
                tarefa.DataDeConclusao = DateTime.Now;
            }

            return tarefa;
        }

        private void ValidarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new ArgumentException(
                    TarefaConstantes.MENSAGEM_TITULO_OBRIGATORIO
                );
            }

            if (titulo.Trim().Length < TarefaConstantes.TAMANHO_MINIMO_TITULO)
            {
                throw new ArgumentException(
                    TarefaConstantes.MENSAGEM_TITULO_MINIMO
                );
            }
        }
    }
}