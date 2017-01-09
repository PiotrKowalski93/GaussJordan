using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussJordan
{
    class Program
    {      
        static void Main(string[] args)
        {
            double[] result;
            double[,] A = new double[3, 3] { { 1, -2, 3 },
                                         { 2, 1, 1},
                                         { -3, 2, -2 } };
            double[] b = { 7, 4, -10 };

            GaussJordanElimination obj = new GaussJordanElimination();

            result = obj.Calcuale(A, b, b.Count());

            for (int i = 0; i < result.Count(); i++)
            {
                Console.WriteLine(result[i] + " ");
            }

            Console.ReadKey();
        }        
    }
}
