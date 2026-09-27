using System.Globalization;


namespace Contabilidade;

public class Program
{
    public static void Main(string[] args)
    {
        Console.Write("Digite a data do pagamento (dd/MM/yyyy): ");

        string entrada = Console.ReadLine() ?? string.Empty;

        bool dataValida = DateTime.TryParseExact(
            entrada,
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime dataPagamento
        );

        if (!dataValida)
        {
            Console.WriteLine("Data inválida.");
            return;
        }

        ResultadoPagamento resultado =
            PagamentoService.CalcularPagamento(dataPagamento);

        Console.WriteLine();
        Console.WriteLine($"Valor base: {resultado.ValorBase:C2}");

        if (resultado.DiasAntecipacao > 0)
        {
            Console.WriteLine(
                $"Pagamento antecipado em {resultado.DiasAntecipacao} dia(s)."
            );

            Console.WriteLine(
                $"Desconto: {resultado.Desconto:C2}"
            );
        }
        else if (resultado.DiasAtraso > 0)
        {
            Console.WriteLine(
                $"Pagamento atrasado em {resultado.DiasAtraso} dia(s)."
            );

            Console.WriteLine(
                $"Multa: {resultado.Multa:C2}"
            );

            Console.WriteLine(
                $"Juros: {resultado.Juros:C2}"
            );
        }
        else
        {
            Console.WriteLine(
                "Pagamento realizado na data do vencimento."
            );
        }

        Console.WriteLine();
        Console.WriteLine(
            $"Valor final: {resultado.ValorFinal:C2}"
        );
    }
}