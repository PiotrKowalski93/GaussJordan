using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GaussJordan
{
    public class GaussJordanElimination
    {
        public double[] Calcuale(double[,] A, double[] I, int rowsNumber)
        {
            double[] x = new double[rowsNumber];
            double[,] tmpA = new double[rowsNumber, rowsNumber + 1];
            double tmp = 0;

            for (int i = 0; i < rowsNumber; i++)
            {
                for (int j = 0; j < rowsNumber; j++)
                {
                    tmpA[i, j] = A[i, j];
                }
                tmpA[i, rowsNumber] = I[i];
            }

            for (int k = 0; k < rowsNumber; k++)
            {
                tmp = tmpA[k, k];
                for (int i = 0; i < rowsNumber + 1; i++)
                {
                    tmpA[k, i] = tmpA[k, i] / tmp;
                }

                for (int i = 0; i < rowsNumber; i++)
                {
                    if (i != k)
                    {
                        tmp = tmpA[i, k] / tmpA[k, k];
                        for (int j = k; j < rowsNumber + 1; j++)
                        {
                            tmpA[i, j] -= tmp * tmpA[k, j];
                        }
                    }
                }
            }

            for (int i = 0; i < rowsNumber; i++)
            {
                x[i] = tmpA[i, rowsNumber];
            }

            return x;
        }
        
        public double[] CalcualeParallel(double[,] MatrixA, double[] MatrixI, int rowsNumber)
        {
            List<Task> tasks = new List<Task>();

            double[] result = new double[rowsNumber];
            double[,] tempMatrix = new double[rowsNumber, rowsNumber + 1];
            double diagonalValue;

            // Prepare Temporaty Matrix
            for (int i = 0; i < rowsNumber; i++)
            {
                for (int j = 0; j < rowsNumber; j++)
                {
                    tempMatrix[i, j] = MatrixA[i, j];
                }
                tempMatrix[i, rowsNumber] = MatrixI[i];
            }

            // Do Calculations
            for (int k = 0; k < rowsNumber; k++)
            {
                diagonalValue = tempMatrix[k, k];

                for (int i = 0; i < rowsNumber + 1; i++)
                {
                    int tmp = i;

                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        tempMatrix[k, tmp] = tempMatrix[k, tmp] / diagonalValue;
                    }));
                }
                Task.WaitAll(tasks.ToArray());
                tasks.Clear();

                for (int i = 0; i < rowsNumber; i++)
                {
                    if (i != k)
                    {
                        int tmp = i;

                        tasks.Add(Task.Factory.StartNew(() =>
                        {
                            diagonalValue = tempMatrix[tmp, k] / tempMatrix[k, k];
                            for (int j = k; j < rowsNumber + 1; j++)
                            {
                                tempMatrix[tmp, j] -= diagonalValue * tempMatrix[k, j];
                            }
                        }));                        
                    }
                }
                Task.WaitAll(tasks.ToArray());
                tasks.Clear();
            }

            // Prepare and return result table
            for (int i = 0; i < rowsNumber; i++)
            {
                result[i] = tempMatrix[i, rowsNumber];
            }

            return result;
        }
        
    }
}
