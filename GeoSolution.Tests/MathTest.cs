using GeoSolution.Utils;

namespace GeoSolution.Tests
{

    namespace GeoSolution.Tests
    {
        public class MathTests
        {
            //sqrt(9)=3
            [Fact]
            public void SafeSqrt_PositiveNumber_ReturnsCorrectResult()
            {
                double input = 9.0;
                double expected = 3.0;

                double result = MathForTest.SafeSqrt(input);

                Assert.Equal(expected, result, precision: 10);
            }
            //eps<=0
            [Fact]
            public void SafeSqrt_NonPositiveEpsilon_ThrowsArgumentException()
            {
                double num = 4.0;
                double eps = -1;
                Assert.Throws<ArgumentException>(() => MathForTest.SafeSqrt(num, eps));

            }
            //num<0 error
            [Fact]
            public void SafeSqrt_NegativeNumber_ThrowsException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => MathForTest.SafeSqrt(-1));
            }
            //sqrt(0)=0
            [Fact]
            public void SafeSqrt_Zero_ReturnsZero()
            {
                double zero = 0.0;
                double result = MathForTest.SafeSqrt(zero);
                double expected = 0.0;
                Assert.Equal(zero, result, precision: 5);
            }
            //sqrt(1)=1
            [Fact]
            public void SafeSqrt_One_ReturnsOne()
            {
                double one = 1.0;
                double result = MathForTest.SafeSqrt(one);
                double expected = 1.0;
                Assert.Equal(one, result, precision: 5);
            }
            //higher eps
            [Fact]
            public void SafeSqrt_CustomEps_Higher()
            {
                double eps = 1.0;
                double number = 2.0;
                double expected = 1.0;
                double result = MathForTest.SafeSqrt(number, eps);
                Assert.Equal(expected, result, precision: 10);
            }
            //precision quality
            [Fact]
            public void SafeSqrt_CustomEps_HighrerPrecision()
            {
                double number = 10.0;
                double eps1 = 1e-12;
                double eps2 = 1e-2;

                double result1 = MathForTest.SafeSqrt(number, eps1);
                double result2 = MathForTest.SafeSqrt(number, eps2);

                double error1 = Math.Abs(result1 * result1 - number);
                double error2 = Math.Abs(result2 * result2 - number);
                Assert.True(error1 < error2, "eps1<eps2 => better precision");
            }
            //null
            [Fact]
            public void Inverse_NullMatrix_ThrowsArgumentNullException()
            {
                double[,] mat = null;
                Assert.Throws<ArgumentNullException>(() => MathForTest.Inverse(mat));
            }
            //eps<=0
            [Fact]
            public void Inverse_NonPositiveEpsilon_ThrowsArgumentException()
            {
                double[,] mat = new double[,] { { 4.0, 7.0 }, { 2.0, 6.0 } };
                double eps = -1;
                Assert.Throws<ArgumentException>(() => MathForTest.Inverse(mat, eps));

            }
            //non square
            [Fact]
            public void Inverse_NonSquareMatrix_ThrowsArgumentException()
            {
                double[,] mat = new double[,] { { 4.0, 7.0 }, { 2.0, 6.0 }, { 2.0, 6.0 } };
                var ex = Assert.Throws<ArgumentException>(() => MathForTest.Inverse(mat));
                //Можно проверить конкретно ли с этой ошибкой у нас проходит тест
                Assert.Contains("Matrix should be square", ex.Message, StringComparison.OrdinalIgnoreCase);
            }
            [Fact]
            public void Inverse_SingularMatrix_ThrowsArgumentException()
            {
                //singulat mat
                double[,] mat = new double[,] { { 2.0, 4.0 }, { 1.0, 2.0 } };
                var ex = Assert.Throws<ArgumentException>(() => MathForTest.Inverse(mat));
                Assert.Contains("Singular matrix", ex.Message, StringComparison.OrdinalIgnoreCase);
            }
            //matrix 1x1
            [Theory]
            [InlineData(5.0)]
            [InlineData(-2.0)]
            public void Inverse_1x1Matrix_ReturnsInverse(double value)
            {
                double[,] mat = new double[,] { { value } };
                double[,] inv = MathForTest.Inverse(mat);
                Assert.Equal(1, inv.GetLength(0));
                Assert.Equal(1, inv.GetLength(1));
                Assert.Equal(1.0 / value, inv[0, 0], precision: 5);
            }
            //единичные матрицы NxN
            [Theory]
            [InlineData(2)]
            [InlineData(3)]
            [InlineData(4)]
            public void Inverse_IdentityMatrix_ReturnsIdentity(int size)
            {
                double[,] identity = new double[size, size];
                for (int i = 0; i < size; i++)
                    identity[i, i] = 1.0;
                double[,] inv = MathForTest.Inverse(identity);
                Assert.Equal(size, inv.GetLength(0));
                Assert.Equal(size, inv.GetLength(1));
                for (int i = 0; i < size; i++)
                {
                    for (int j = 0; j < size; j++)
                    {
                        if (i == j)
                            Assert.Equal(1.0, inv[i, j], precision: 5);
                        else
                            Assert.Equal(0.0, inv[i, j], precision: 5);
                    }
                }
            }
            //2x2 matrix
            [Fact]
            public void Inverse_2x2Matrix_CorrectResult()
            {
                double[,] mat = new double[,] { { 4.0, 7.0 }, { 2.0, 6.0 } };
                double det = 4.0 * 6.0 - 7.0 * 2.0;
                double[,] expected = new double[,]
                {
                    {  6.0 / det, -7.0 / det },
                    { -2.0 / det,  4.0 / det }
                };
                double[,] inv = MathForTest.Inverse(mat);

                // Assert
                Assert.Equal(2, inv.GetLength(0));
                Assert.Equal(2, inv.GetLength(1));
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Assert.Equal(expected[i, j], inv[i, j], precision: 12);
                    }
                }
            }
            //3x3
            [Fact]
            public void Inverse_3x3Matrix_CorrectResult()
            {
                double[,] mat = new double[,]
                {
                    { 3.0, 0.0, 2.0 },
                    { 2.0, 0.0, -2.0 },
                    { 0.0, 1.0, 1.0 }
                };
                double[,] expectedInv = new double[,]
                {
                    {  0.2,  0.2,  0.0 },
                    { -0.2,  0.3,  1.0 },
                    {  0.2, -0.3,  0.0 }
                };
                double[,] inv = MathForTest.Inverse(mat);
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        Assert.Equal(expectedInv[i, j], inv[i, j], precision: 12);
                    }
                }
                        
            }
            [Fact]
            public void Inverse_PivotBelowEps()
            {
                double small = 1e-16;
                double[,] mat = new double[,]
                {
                { 1.0, small },
                { 0.0, small }
                };
                double epsilon = 1e-17;

                var ex = Assert.Throws<ArgumentException>(() => MathForTest.Inverse(mat));
                Assert.Contains("Singular matrix", ex.Message, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}
