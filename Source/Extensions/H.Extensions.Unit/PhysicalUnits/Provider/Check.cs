
using System;

namespace H.Extensions.Unit
{
    public class Check
    {
        public static void True(bool condition, string message)
        {
            if (!condition) throw new Exception(message);
        }

        public static void False(bool condition, string message)
        {
            if (condition) throw new Exception(message);
        }

        public static void Equal(int p1, int p2, string message)
        {
            True(p1 == p2, message);
        }

        public static void Equal(double p1, double p2, string message, double epsilon = 0.000001)
        {
            True(Math.Abs(p1 - p2) < epsilon, message);
        }

        public static void Equal(string p1, string p2, string message)
        {
            True(p1 == p2, message);
        }

        public static void ThrowsException(Action p, string expectedException)
        {
            try
            {
                p();
            }
            catch (Exception ex)
            {
                Equal(ex.Message, expectedException, $"Code threw incorrect exception: {ex.Message}");
                return;
            }
            throw new Exception($"Code did not throw exception: {expectedException}");
        }

        public static void Fail(string message)
        {
            True(false, message);
        }
    }
}
