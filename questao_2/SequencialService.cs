using System;
using System.Collections.Generic;

namespace DesafioTecnico
{
    public static class SequencialService
    {
        public static List<int> FindSequencial(List<int> numbers)
        {
            HashSet<int> conjunto = new HashSet<int>(numbers);

            int initial = 0;
            int longesSequence = 0;

            foreach (int number in conjunto)
            {
                if (!conjunto.Contains(number - 1))
                {
                    int current = number;
                    int size = 1;

                    while (conjunto.Contains(current + 1))
                    {
                        current ++;
                        size ++;
                    }

                    if (size > longesSequence)
                    {
                        longesSequence = size;
                        initial = number;
                    }
                }
            }

            List<int> result = new List<int>();

            for (int i = 0; i < longesSequence; i++)
            {
                result.Add(initial + i);
            }
            return result;
        }
    }
}
