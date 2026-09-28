namespace Questao4.Constantes
{
    public static class PagamentoConstante
    {
        public const decimal VALOR_BASE = 1000.00m;

        public const decimal TAXA_DESCONTO_DIA = 0.01m;

        public const decimal DESCONTO_MAXIMO = 0.10m;

        public const decimal TAXA_MULTA = 0.02m;

        public const decimal TAXA_JUROS_DIA = 0.005m;

        public static readonly DateTime DATA_VENCIMENTO =
            new DateTime(2026, 10, 10);
    }    
}
