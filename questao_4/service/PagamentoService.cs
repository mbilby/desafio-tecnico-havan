namespace Contabilidade
{
    public static class PagamentoService
    {

        public static ResultadoPagamento CalcularPagamento(DateTime dataPagamento)
        {
            ResultadoPagamento resultado = new ResultadoPagamento();

            resultado.ValorBase = PagamentoConstante.VALOR_BASE;

            if (dataPagamento.Date < PagamentoConstante.DATA_VENCIMENTO)
            {
                CalcularPagamentoAntecipado(dataPagamento, resultado);
            }
            else if (dataPagamento.Date > PagamentoConstante.DATA_VENCIMENTO)
            {
                CalcularPagamentoAtrasado(dataPagamento, resultado);
            }
            else
            {
                resultado.ValorFinal = PagamentoConstante.VALOR_BASE;
            }

            return resultado;
        }

        private static void CalcularPagamentoAntecipado(
            DateTime dataPagamento,
            ResultadoPagamento resultado)
        {
            int diasAntecipacao =
                (PagamentoConstante.DATA_VENCIMENTO - dataPagamento.Date).Days;

            decimal percentualDesconto =
                diasAntecipacao * PagamentoConstante.TAXA_DESCONTO_DIA;

            if (percentualDesconto > PagamentoConstante.DESCONTO_MAXIMO)
            {
                percentualDesconto = PagamentoConstante.DESCONTO_MAXIMO;
            }

            decimal valorDesconto =
                PagamentoConstante.VALOR_BASE * percentualDesconto;

            resultado.DiasAntecipacao = diasAntecipacao;
            resultado.Desconto = valorDesconto;
            resultado.ValorFinal = PagamentoConstante.VALOR_BASE - valorDesconto;
        }

        private static void CalcularPagamentoAtrasado(
            DateTime dataPagamento,
            ResultadoPagamento resultado)
        {
            int diasAtraso =
                (dataPagamento.Date - PagamentoConstante.DATA_VENCIMENTO).Days;

            decimal valorMulta =
                PagamentoConstante.VALOR_BASE * PagamentoConstante.TAXA_MULTA;

            decimal valorJuros =
                PagamentoConstante.VALOR_BASE * PagamentoConstante.TAXA_JUROS_DIA * diasAtraso;

            resultado.DiasAtraso = diasAtraso;
            resultado.Multa = valorMulta;
            resultado.Juros = valorJuros;

            resultado.ValorFinal =
                PagamentoConstante.VALOR_BASE + valorMulta + valorJuros;
        }
    }
}