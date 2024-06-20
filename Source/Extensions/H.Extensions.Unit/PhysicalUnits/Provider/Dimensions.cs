
using System;

namespace H.Extensions.Unit
{
    /// <summary>
    /// Class to represent a physical dimension or combination of them.
    /// </summary>
    public partial class Dimensions
    {
        // There is a read-only value for the power of each dimension:
        public readonly short M; // Mass
        public readonly short L; // Length
        public readonly short T; // Time
        public readonly short I; // Current
        public readonly short Θ; // Temperature
        public readonly short N; // Amount of Substance
        public readonly short J; // Luminous Intensity
        public readonly short A; // Angle. Strictly this is dimensionless, but it is convenient to treat it as a distinct dimension

        public Dimensions(short m, short l, short t)
        {
            M = m; L = l; T = t;
        }

        public Dimensions(int m, int l, int t)
        {
            M = (short)m; L = (short)l; T = (short)t;
        }

        public Dimensions(short m, short l, short t, short i, short th = 0, short n = 0, short j = 0, short a = 0)
        {
            M = m; L = l; T = t; I = i; Θ = th; N = n; J = j; A = a;
        }

        public Dimensions(int m, int l, int t, int i, int th = 0, int n = 0, int j = 0, int a = 0)
        {
            M = (short)m; L = (short)l; T = (short)t; I = (short)i; Θ = (short)th; N = (short)n; J = (short)j; A = (short)a;
        }

        public static bool operator ==(Dimensions d1, Dimensions d2)
        {
            if (Object.ReferenceEquals(d1, d2))
                return true;
            bool d1null = Object.ReferenceEquals(d1, null);
            bool d2null = Object.ReferenceEquals(d2, null);
            if (!d1null && !d2null)
                return (d1.M == d2.M && d1.L == d2.L && d1.T == d2.T && d1.I == d2.I && d1.Θ == d2.Θ && d1.N == d2.N && d1.J == d2.J && d1.A == d2.A);
            else if (d1null && d2null)
                return true;
            else
                return false;
        }

        public static bool operator !=(Dimensions d1, Dimensions d2)
        {
            bool d1null = Object.ReferenceEquals(d1, null);
            bool d2null = Object.ReferenceEquals(d2, null);
            if (!d1null && !d2null)
                return (d1.M != d2.M || d1.L != d2.L || d1.T != d2.T || d1.I != d2.I || d1.Θ != d2.Θ || d1.N != d2.N || d1.J != d2.J || d1.A != d2.A);
            else if (d1null && d2null)
                return false;
            else
                return true;
        }

        public static Dimensions operator *(Dimensions d1, Dimensions d2)
        {
            return new Dimensions(d1.M + d2.M, d1.L + d2.L, d1.T + d2.T, d1.I + d2.I, d1.Θ + d2.Θ, d1.N + d2.N, d1.J + d2.J, d1.A + d2.A);
        }

        public static Dimensions operator /(Dimensions d1, Dimensions d2)
        {
            return new Dimensions(d1.M - d2.M, d1.L - d2.L, d1.T - d2.T, d1.I - d2.I, d1.Θ - d2.Θ, d1.N - d2.N, d1.J - d2.J, d1.A - d2.A);
        }

        public static Dimensions operator ^(Dimensions d1, int e)
        {
            return new Dimensions(d1.M * e, d1.L * e, d1.T * e, d1.I * e, d1.Θ * e, d1.N * e, d1.J * e, d1.A * e);
        }

        public Dimensions Sqrt()
        {
            // Check that all dimensions are divisible by 2
            Check.True((M % 2) == 0, "M is not even");
            Check.True((L % 2) == 0, "L is not even");
            Check.True((T % 2) == 0, "T is not even");
            Check.True((I % 2) == 0, "I is not even");
            Check.True((Θ % 2) == 0, "Θ is not even");
            Check.True((N % 2) == 0, "N is not even");
            Check.True((J % 2) == 0, "J is not even");
            Check.True((A % 2) == 0, "A is not even");
            return new Dimensions((short)(M / 2), (short)(L / 2), (short)(T / 2), (short)(I / 2), (short)(Θ / 2), (short)(N / 2), (short)(J / 2), (short)(A / 2));
        }

        public override string ToString()
        {
            string text = "";
            if (M != 0)
                text += string.Format("M{0} ", Powers.ToString(M));
            if (L != 0)
                text += string.Format("L{0} ", Powers.ToString(L));
            if (T != 0)
                text += string.Format("T{0} ", Powers.ToString(T));
            if (I != 0)
                text += string.Format("I{0} ", Powers.ToString(I));
            if (Θ != 0)
                text += string.Format("Θ{0} ", Powers.ToString(Θ));
            if (N != 0)
                text += string.Format("N{0} ", Powers.ToString(N));
            if (J != 0)
                text += string.Format("J{0} ", Powers.ToString(J));
            if (A != 0)
                text += string.Format("A{0} ", Powers.ToString(A));
            return text;
        }

    }
}
