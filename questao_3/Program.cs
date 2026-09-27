public class Program
{
    public static void Main()
    {
        Console.Write("Digite uma frase: ");

        string frase = Console.ReadLine() ?? string.Empty;

        string textoHigienizado = AnaliseTextoService.HigienizarTexto(frase);

        Dictionary<char, int> frequencias = AnaliseTextoService.ContarCaracteres(textoHigienizado);

        char? primeiroNaoRepetido = AnaliseTextoService.EncontrarPrimeiroNaoRepetido(
        textoHigienizado,
        frequencias
        );

        List<KeyValuePair<char, int>> top3 = AnaliseTextoService.ObterTop3Caracteres(frequencias);

        Console.WriteLine();
        Console.WriteLine($"Texto higienizado: {textoHigienizado}");

        if (primeiroNaoRepetido.HasValue)
        {
            Console.WriteLine(
                $"Primeiro caractere não repetido: '{primeiroNaoRepetido.Value}'"
            );
        }
        else
        {
            Console.WriteLine(
                "Não existe caractere não repetido."
            );
        }

        Console.WriteLine();
        Console.WriteLine("Top 3 caracteres mais frequentes:");

        foreach (var item in top3)
        {
            Console.WriteLine(
                $"Letra '{item.Key}': {item.Value} vezes"
            );
        }
    }
}