using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Numerics;

namespace GeoSolution.Utils
{
    public class MathForTest
    {
        //метод Ньютона
        public static double SafeSqrt(double number, double epsilon = 1e-10)
        {
            if (number < 0)
            {

                throw new ArgumentOutOfRangeException(nameof(number), "The number should be positive");
            }
            if (number == 0 || number == 1)
            {
                return number;
            }
            if (epsilon <= 0)
            {
                throw new ArgumentException("Epsilot should be positive", nameof(epsilon));
            }

            double guess = number / 2.0;

            while (Math.Abs(guess * guess - number) > epsilon)
            {
                guess = (guess + number / guess) / 2.0;
            }

            return guess;
        }
        // matrix
        public static double[,] Inverse(double[,] matrix, double epsilon = 1e-10)
        {
            if (matrix == null)
            {
                throw new ArgumentNullException(nameof(matrix), "Matrix cannot be null.");
            }
            if (epsilon <= 0)
            {
                throw new ArgumentException("Epsilon should be positive", nameof(epsilon));
            }
            int x = matrix.GetLength(0);
            int y = matrix.GetLength(1);
            if (x != y)
            {
                throw new ArgumentException("matrix should be square");
            }
            //algo
            //[A | I]
            double[,] aug = new double[x, 2 * x];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    aug[i, j] = matrix[i, j];
                }
                for (int j = 0; j < x; j++)
                {
                    aug[i, x + j] = (i == j) ? 1.0 : 0.0; //I
                }
            }
            //Гаусс-Жордан
            for (int col = 0; col < x; col++)
            {
                // 1 ищем строку с макс значением
                int pivotRow = col;
                double maxAbs = Math.Abs(aug[pivotRow, col]);
                for (int r = col + 1; r < x; r++)
                {
                    double absVal = Math.Abs(aug[r, col]);
                    if (absVal > maxAbs)
                    {
                        maxAbs = absVal;
                        pivotRow = r;
                    }
                }
                if (maxAbs<epsilon)
                {
                    throw new ArgumentException("Singular matrix");
                }
                //2 swap col pivotRow
                if (pivotRow != col)
                {
                    for (int c = 0; c < 2 * x; c++)
                    {
                        double tmp = aug[col, c];
                        aug[col, c] = aug[pivotRow, c];
                        aug[pivotRow, c] = tmp;
                    }
                }
                //3 получаем единицы
                double diag = aug[col, col];
                for (int c = 0; c < 2 * x; c++)
                {
                    aug[col, c] /= diag;
                }
                //4 обнуляем
                for(int r = 0; r<x; r++)
                {
                    if(r != col)
                    {
                        double factor = aug[r, col];
                        if (Math.Abs(factor)<epsilon)
                        {
                            continue;
                        }
                        for(int c = 0;c < 2 * x; c++)
                        {
                            aug[r, c] -= factor* aug[col,c];
                        }
                    }
                }
            }
            double[,] inverse = new double[x,y];
            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    inverse[i, j] = aug[i, x + j];
                }
                    
            }
            return inverse;

        }
    }
}
