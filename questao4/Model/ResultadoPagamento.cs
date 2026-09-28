namespace Questao4.Model
{
    public class ResultadoPagamento
    {
        public decimal ValorBase { get; set; }

        public int DiasAntecipacao { get; set; }

        public int DiasAtraso { get; set; }

        public decimal Desconto { get; set; }

        public decimal Multa { get; set; }

        public decimal Juros { get; set; }

        public decimal ValorFinal { get; set; }
    }
}
