namespace Utility
{
    public class ScalingUtils
    {
        public static int GetFibonacci(int n)
        {
            if (n == 0)
                return 0;
            if (n == 1)
                return 1;
            return GetFibonacci(n - 1) + GetFibonacci(n - 2);
        }
    }
}