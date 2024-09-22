namespace trpo.Lab3
{
    public static class Calculator
    {
        public static double Calculate(double x)
        {
            if (Math.Abs(x) > 1)
                return 2 * x * 5;
            else
                return Math.Pow(x, 2) + 8;
        }
    }
}
