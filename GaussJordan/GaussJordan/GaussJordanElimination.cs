using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussJordan
{
    public class GaussJordanElimination
    {
        public double[] Calcuale(double[,] A, double[] b, int n)
        {
            double[] x = new double[n];
            double[,] tmpA = new double[n, n + 1];
            double tmp = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    tmpA[i, j] = A[i, j];
                }
                tmpA[i, n] = b[i];
            }

            for (int k = 0; k < n; k++)
            {
                tmp = tmpA[k, k];
                for (int i = 0; i < n + 1; i++)
                {
                    tmpA[k, i] = tmpA[k, i] / tmp;
                }

                for (int i = 0; i < n; i++)
                {
                    if (i != k)
                    {
                        tmp = tmpA[i, k] / tmpA[k, k];
                        for (int j = k; j < n + 1; j++)
                        {
                            tmpA[i, j] -= tmp * tmpA[k, j];
                        }
                    }
                }
            }

            for (int i = 0; i < n; i++)
            {
                x[i] = tmpA[i, n];
            }

            return x;
        }
    }
}
