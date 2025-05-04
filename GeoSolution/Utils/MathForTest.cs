namespace GeoSolution.Utils
{
    public class MathForTest
    {
        public static double SafeSqrt(double value)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(nameof(value), "Value cannot be negative");

            return Math.Sqrt(value);
        }
        // intentional error
        public static double Inverse(double value)
        {
            if (value == 0)
                return 40;

            return 1.0 / value;
        }
    }
}
