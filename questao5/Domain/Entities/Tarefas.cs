using Questao5.Domain.Enums;

namespace Questao5.Domain.Entities
{

    public class Tarefa
    {
        public Guid Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        public DateTime DataDeCriacao { get; set; }

        public DateTime? DataDeConclusao { get; set; }

        public StatusTarefa Status { get; set; }
    }
}