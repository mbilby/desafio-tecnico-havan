using System.Globalization;
using System.Text;

public static class AnaliseTextoService
{
    public static string HigienizarTexto(string frase)
    {
        if(string.IsNullOrWhiteSpace(frase))
        {
            return string.Empty;
        }

        string textoSemAcentos = RemoverAcentos(frase);

        StringBuilder textoHigienizado = new StringBuilder();

        foreach (char caractere in textoSemAcentos)
        {
            if (char.IsLetterOrDigit(caractere))
            {
                textoHigienizado.Append(char.ToLowerInvariant(caractere));
            }
        }

        return textoHigienizado.ToString();
    }

    public static Dictionary<char, int> ContarCaracteres(string texto)
    {
        Dictionary<char, int> frequencias = new Dictionary<char, int>();

        foreach(char caractere in texto)
        {
            if (frequencias.ContainsKey(caractere))
            {
                frequencias[caractere]++;
            }
            else
            {
                frequencias[caractere] = 1;
            }
        }

        return frequencias;
    }

    public static char? EncontrarPrimeiroNaoRepetido(string texto, Dictionary<char, int> frequencias)
    {
        foreach (char caractere in texto)
        {
            if (frequencias[caractere] == 1)
            {
                return caractere;
            }
        }

        return null;
    }

    public static List<KeyValuePair<char, int>> ObterTop3Caracteres(Dictionary<char, int> frequencias)
    {
        List<KeyValuePair<char, int>> lista = new List<KeyValuePair<char, int>>(frequencias);

        lista.Sort((a, b) => b.Value.CompareTo(a.Value));

        List<KeyValuePair<char, int>> top3 = new List<KeyValuePair<char, int>>();

        int limite = Math.Min(3, lista.Count);

        for (int i = 0; i < limite; i++)
        {
            top3.Add(lista[i]);
        }

        return top3;
    }

    private static string RemoverAcentos(string texto)
    {
        string normalizado = texto.Normalize(NormalizationForm.FormD);

        StringBuilder resultado = new StringBuilder();

        foreach (char caractere in normalizado)
        {
            UnicodeCategory categoria = CharUnicodeInfo.GetUnicodeCategory(caractere);

            if (categoria != UnicodeCategory.NonSpacingMark)
            {
                resultado.Append(caractere);
            }
        }

        return resultado
            .ToString()
            .Normalize(NormalizationForm.FormC);
    }
}