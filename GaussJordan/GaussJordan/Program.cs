using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussJordan
{
    class Program
    {
        static double[,] a;
        static double[] b;
        static int n = 200;


        static void Main(string[] args)
        {
            Stopwatch watch = new Stopwatch();
            PrepareData();
            double[] result;
            //double[,] A = new double[4,4] { { 1, -2, 3, 3 },
            //                             { 2, 1, 1, 7},
            //                             { -3, 2, -2, 1 },
            //                            { -1, 3, -1, 2 }};
            //double[] I = { 4, 1, -2,0 };

            GaussJordanElimination obj = new GaussJordanElimination();

            watch.Start();
            result = obj.Calcuale(a, b, n);
            watch.Stop();
            Console.WriteLine("first 10 results");
            PrintResultTable(result);
            Console.WriteLine("...");
            Console.WriteLine(string.Format("single thread: {0} [ms]",watch.ElapsedMilliseconds));
            Console.WriteLine("");

            watch.Reset();

            watch.Start();
            result = obj.CalcualeParallel(a, b, n);
            watch.Stop();
            Console.WriteLine("");
            Console.WriteLine("first 10 results");
            PrintResultTable(result);
            Console.WriteLine("...");
            Console.WriteLine(string.Format("multi thread: {0} [ms]", watch.ElapsedMilliseconds));

            Console.ReadKey();
        }        

        static void PrintResultTable(double[] result)
        {
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(result[i] + " ");
            }
        }

        static void PrepareData()
        {
            a = new double[n, n];
            b = new double[n];
            Random rand = new Random();
            for (int i = 0; i < n; i++)
            {
                
                for (int j = 0; j < n; j++)
                {
                    a[i,j] = rand.NextDouble();
                }
                b[i] = rand.NextDouble();

            }
        }
    }
}
