using System;
using System.Collections.Generic;
using DesafioTecnico;

namespace DesafioTecnico
{
    public class Program
    {
        public static void Main()
        {
            List<int> numeros = new List<int>
            {
                100, 4, 200, 1, 3, 2
            };
            var longesSequence = SequencialService.FindSequencial(numeros);

            Console.WriteLine(
                $"Maior sequência: [{string.Join(", ", longesSequence)}]"
            );

            Console.WriteLine(
                $"Tamanho: {longesSequence.Count}"
            );
        }
    }
}



