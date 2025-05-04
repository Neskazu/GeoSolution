using GeoSolution.Utils;

namespace GeoSolution.Tests
{

    namespace GeoSolution.Tests
    {
        public class MathTests
        {
            [Fact]
            public void SafeSqrt_PositiveNumber_ReturnsCorrectResult()
            {
                double input = 9.0;
                double expected = 3.0;

                double result = MathForTest.SafeSqrt(input);

                Assert.Equal(expected, result, precision: 5);
            }

            [Fact]
            public void SafeSqrt_NegativeNumber_ThrowsException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => MathForTest.SafeSqrt(-1));
            }
            [Fact]
            public void Inverse_NonZero_ReturnsCorrectResult()
            {
                double input = 4.0;
                double expected = 0.25;

                double result = MathForTest.Inverse(input);

                Assert.Equal(expected, result, precision: 5);
            }
            [Fact]
            public void Inverse_Zero_ThrowsException()
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => MathForTest.Inverse(0));
            }
        }
    }
}
