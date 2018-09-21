using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bingo
{
    class Program
    {
        static void Main(string[] args)
        {

            var input = new int[] { 3, 4, 8, 13, 18, 19, 23 };
            if (Bingo(input))
                Console.WriteLine("Bingo");
            else
                Console.WriteLine("Not Binggo");

            input = new int[] { 21, 17, 13, 9, 0 };
            if (Bingo(input))
                Console.WriteLine("Bingo");
            else
                Console.WriteLine("Not Binggo");

            Console.ReadLine();

        }

        static bool Bingo(int[] input)
        {
            var card = new int[,] { { 1, 2, 3, 4, 5 }, { 6, 7, 8, 9, 10 }, { 11, 12, 13, 14, 15 }, { 16, 17, 18, 19, 20 }, { 17, 18, 19, 20, 21 } };
            var result = new bool?[5, 5];
            foreach (var inp in input)
            {
                for (var i = 0; i < card.GetLength(0); i++)
                {
                    for (var n = 0; n < card.GetLength(1); n++)
                    {
                        if (card[i, n] == inp)
                        {
                            result[i, n] = true;
                        }
                    }
                }
            }

            for (var i = 0; i < result.GetLength(0); i++)
            { 
                if (result[i, 0] == true &&
                    result[i, 1] == true &&
                    result[i, 2] == true &&
                    result[i, 3] == true &&
                    result[i, 4] == true)
                    return true;

                for (var n = 0; n < result.GetLength(1); n++)
                {
                    if (result[0, n] == true &&
                        result[1, n] == true &&
                        result[2, n] == true &&
                        result[3, n] == true &&
                        result[4, n] == true)
                        return true;
                }
            }

            if (result[0, 0] == true &&
                result[1, 1] == true &&
                result[2, 2] == true &&
                result[3, 3] == true &&
                result[4, 4] == true)
                return true;
            else if (result[0, 4] == true &&
                result[1, 3] == true &&
                result[2, 2] == true &&
                result[3, 1] == true &&
                result[4, 0] == true)
                return true;
            else
                return false;
        }
    }
}
