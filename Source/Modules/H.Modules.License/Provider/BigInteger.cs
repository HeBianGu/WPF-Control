// Copyright © 2024 By HeBianGu(QQ:908293466) https://github.com/HeBianGu/WPF-Control

using System;

namespace H.Modules.License
{
    internal class BigInteger
    {
        private const int maxLength = 70;
        public static readonly int[] primesBelow2000 = {
        2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43, 47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97,
        101, 103, 107, 109, 113, 127, 131, 137, 139, 149, 151, 157, 163, 167, 173, 179, 181, 191, 193, 197, 199,
    211, 223, 227, 229, 233, 239, 241, 251, 257, 263, 269, 271, 277, 281, 283, 293,
    307, 311, 313, 317, 331, 337, 347, 349, 353, 359, 367, 373, 379, 383, 389, 397,
    401, 409, 419, 421, 431, 433, 439, 443, 449, 457, 461, 463, 467, 479, 487, 491, 499,
    503, 509, 521, 523, 541, 547, 557, 563, 569, 571, 577, 587, 593, 599,
    601, 607, 613, 617, 619, 631, 641, 643, 647, 653, 659, 661, 673, 677, 683, 691,
    701, 709, 719, 727, 733, 739, 743, 751, 757, 761, 769, 773, 787, 797,
    809, 811, 821, 823, 827, 829, 839, 853, 857, 859, 863, 877, 881, 883, 887,
    907, 911, 919, 929, 937, 941, 947, 953, 967, 971, 977, 983, 991, 997,
    1009, 1013, 1019, 1021, 1031, 1033, 1039, 1049, 1051, 1061, 1063, 1069, 1087, 1091, 1093, 1097,
    1103, 1109, 1117, 1123, 1129, 1151, 1153, 1163, 1171, 1181, 1187, 1193,
    1201, 1213, 1217, 1223, 1229, 1231, 1237, 1249, 1259, 1277, 1279, 1283, 1289, 1291, 1297,
    1301, 1303, 1307, 1319, 1321, 1327, 1361, 1367, 1373, 1381, 1399,
    1409, 1423, 1427, 1429, 1433, 1439, 1447, 1451, 1453, 1459, 1471, 1481, 1483, 1487, 1489, 1493, 1499,
    1511, 1523, 1531, 1543, 1549, 1553, 1559, 1567, 1571, 1579, 1583, 1597,
    1601, 1607, 1609, 1613, 1619, 1621, 1627, 1637, 1657, 1663, 1667, 1669, 1693, 1697, 1699,
    1709, 1721, 1723, 1733, 1741, 1747, 1753, 1759, 1777, 1783, 1787, 1789,
    1801, 1811, 1823, 1831, 1847, 1861, 1867, 1871, 1873, 1877, 1879, 1889,
    1901, 1907, 1913, 1931, 1933, 1949, 1951, 1973, 1979, 1987, 1993, 1997, 1999 };


        private uint[] data = null;
        public int dataLength;
        public BigInteger()
        {
            data = new uint[maxLength];
            dataLength = 1;
        }

        public BigInteger(long value)
        {
            data = new uint[maxLength];
            long tempVal = value;
            dataLength = 0;
            while (value != 0 && dataLength < maxLength)
            {
                data[dataLength] = (uint)(value & 0xFFFFFFFF);
                value >>= 32;
                dataLength++;
            }

            if (tempVal > 0)
            {
                if (value != 0 || (data[maxLength - 1] & 0x80000000) != 0)
                    throw (new ArithmeticException("Positive overflow in constructor."));
            }
            else if (tempVal < 0)
            {
                if (value != -1 || (data[dataLength - 1] & 0x80000000) == 0)
                    throw (new ArithmeticException("Negative underflow in constructor."));
            }

            if (dataLength == 0)
                dataLength = 1;
        }

        public BigInteger(ulong value)
        {
            data = new uint[maxLength];
            dataLength = 0;
            while (value != 0 && dataLength < maxLength)
            {
                data[dataLength] = (uint)(value & 0xFFFFFFFF);
                value >>= 32;
                dataLength++;
            }

            if (value != 0 || (data[maxLength - 1] & 0x80000000) != 0)
                throw (new ArithmeticException("Positive overflow in constructor."));

            if (dataLength == 0)
                dataLength = 1;
        }
        public BigInteger(BigInteger bi)
        {
            data = new uint[maxLength];

            dataLength = bi.dataLength;

            for (int i = 0; i < dataLength; i++)
                data[i] = bi.data[i];
        }

        public BigInteger(string value, int radix)
        {
            BigInteger multiplier = new BigInteger(1);
            BigInteger result = new BigInteger();
            value = (value.ToUpper()).Trim();
            int limit = 0;

            if (value[0] == '-')
                limit = 1;

            for (int i = value.Length - 1; i >= limit; i--)
            {
                int posVal = value[i];

                if (posVal >= '0' && posVal <= '9')
                    posVal -= '0';
                else if (posVal >= 'A' && posVal <= 'Z')
                    posVal = (posVal - 'A') + 10;
                else
                    posVal = 9999999;


                if (posVal >= radix)
                    throw (new ArithmeticException("Invalid string in constructor."));
                else
                {
                    if (value[0] == '-')
                        posVal = -posVal;

                    result = result + (multiplier * posVal);

                    if ((i - 1) >= limit)
                        multiplier = multiplier * radix;
                }
            }

            if (value[0] == '-')
            {
                if ((result.data[maxLength - 1] & 0x80000000) == 0)
                    throw (new ArithmeticException("Negative underflow in constructor."));
            }
            else
            {
                if ((result.data[maxLength - 1] & 0x80000000) != 0)
                    throw (new ArithmeticException("Positive overflow in constructor."));
            }

            data = new uint[maxLength];
            for (int i = 0; i < result.dataLength; i++)
                data[i] = result.data[i];

            dataLength = result.dataLength;
        }

        public BigInteger(byte[] inData)
        {
            dataLength = inData.Length >> 2;

            int leftOver = inData.Length & 0x3;
            if (leftOver != 0)
                dataLength++;


            if (dataLength > maxLength)
                throw (new ArithmeticException("Byte overflow in constructor."));

            data = new uint[maxLength];

            for (int i = inData.Length - 1, j = 0; i >= 3; i -= 4, j++)
            {
                data[j] = (uint)((inData[i - 3] << 24) + (inData[i - 2] << 16) +
                                 (inData[i - 1] << 8) + inData[i]);
            }

            if (leftOver == 1)
                data[dataLength - 1] = inData[0];
            else if (leftOver == 2)
                data[dataLength - 1] = (uint)((inData[0] << 8) + inData[1]);
            else if (leftOver == 3)
                data[dataLength - 1] = (uint)((inData[0] << 16) + (inData[1] << 8) + inData[2]);


            while (dataLength > 1 && data[dataLength - 1] == 0)
                dataLength--;
        }

        public BigInteger(byte[] inData, int inLen)
        {
            dataLength = inLen >> 2;

            int leftOver = inLen & 0x3;
            if (leftOver != 0)
                dataLength++;

            if (dataLength > maxLength || inLen > inData.Length)
                throw (new ArithmeticException("Byte overflow in constructor."));


            data = new uint[maxLength];

            for (int i = inLen - 1, j = 0; i >= 3; i -= 4, j++)
            {
                data[j] = (uint)((inData[i - 3] << 24) + (inData[i - 2] << 16) +
                                 (inData[i - 1] << 8) + inData[i]);
            }

            if (leftOver == 1)
                data[dataLength - 1] = inData[0];
            else if (leftOver == 2)
                data[dataLength - 1] = (uint)((inData[0] << 8) + inData[1]);
            else if (leftOver == 3)
                data[dataLength - 1] = (uint)((inData[0] << 16) + (inData[1] << 8) + inData[2]);


            if (dataLength == 0)
                dataLength = 1;

            while (dataLength > 1 && data[dataLength - 1] == 0)
                dataLength--;
        }
        public BigInteger(uint[] inData)
        {
            dataLength = inData.Length;

            if (dataLength > maxLength)
                throw (new ArithmeticException("Byte overflow in constructor."));

            data = new uint[maxLength];

            for (int i = dataLength - 1, j = 0; i >= 0; i--, j++)
                data[j] = inData[i];

            while (dataLength > 1 && data[dataLength - 1] == 0)
                dataLength--;
        }

        public static implicit operator BigInteger(long value)
        {
            return (new BigInteger(value));
        }

        public static implicit operator BigInteger(ulong value)
        {
            return (new BigInteger(value));
        }

        public static implicit operator BigInteger(int value)
        {
            return (new BigInteger(value));
        }

        public static implicit operator BigInteger(uint value)
        {
            return (new BigInteger((ulong)value));
        }

        public static BigInteger operator +(BigInteger bi1, BigInteger bi2)
        {
            BigInteger result = new BigInteger();

            result.dataLength = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;

            long carry = 0;
            for (int i = 0; i < result.dataLength; i++)
            {
                long sum = bi1.data[i] + (long)bi2.data[i] + carry;
                carry = sum >> 32;
                result.data[i] = (uint)(sum & 0xFFFFFFFF);
            }

            if (carry != 0 && result.dataLength < maxLength)
            {
                result.data[result.dataLength] = (uint)(carry);
                result.dataLength++;
            }

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;

            int lastPos = maxLength - 1;
            if ((bi1.data[lastPos] & 0x80000000) == (bi2.data[lastPos] & 0x80000000) &&
               (result.data[lastPos] & 0x80000000) != (bi1.data[lastPos] & 0x80000000))
            {
                throw (new ArithmeticException());
            }

            return result;
        }

        public static BigInteger operator ++(BigInteger bi1)
        {
            BigInteger result = new BigInteger(bi1);

            long val, carry = 1;
            int index = 0;

            while (carry != 0 && index < maxLength)
            {
                val = result.data[index];
                val++;

                result.data[index] = (uint)(val & 0xFFFFFFFF);
                carry = val >> 32;

                index++;
            }

            if (index > result.dataLength)
                result.dataLength = index;
            else
            {
                while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                    result.dataLength--;
            }

            int lastPos = maxLength - 1;
            if ((bi1.data[lastPos] & 0x80000000) == 0 &&
               (result.data[lastPos] & 0x80000000) != (bi1.data[lastPos] & 0x80000000))
            {
                throw (new ArithmeticException("Overflow in ++."));
            }
            return result;
        }

        public static BigInteger operator -(BigInteger bi1, BigInteger bi2)
        {
            BigInteger result = new BigInteger();
            result.dataLength = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;

            long carryIn = 0;
            for (int i = 0; i < result.dataLength; i++)
            {
                long diff;

                diff = bi1.data[i] - (long)bi2.data[i] - carryIn;
                result.data[i] = (uint)(diff & 0xFFFFFFFF);

                if (diff < 0)
                    carryIn = 1;
                else
                    carryIn = 0;
            }

            if (carryIn != 0)
            {
                for (int i = result.dataLength; i < maxLength; i++)
                    result.data[i] = 0xFFFFFFFF;
                result.dataLength = maxLength;
            }

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;

            int lastPos = maxLength - 1;
            if ((bi1.data[lastPos] & 0x80000000) != (bi2.data[lastPos] & 0x80000000) &&
               (result.data[lastPos] & 0x80000000) != (bi1.data[lastPos] & 0x80000000))
            {
                throw (new ArithmeticException());
            }

            return result;
        }

        public static BigInteger operator --(BigInteger bi1)
        {
            BigInteger result = new BigInteger(bi1);

            long val;
            bool carryIn = true;
            int index = 0;

            while (carryIn && index < maxLength)
            {
                val = result.data[index];
                val--;

                result.data[index] = (uint)(val & 0xFFFFFFFF);

                if (val >= 0)
                    carryIn = false;

                index++;
            }

            if (index > result.dataLength)
                result.dataLength = index;

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;
            int lastPos = maxLength - 1;
            if ((bi1.data[lastPos] & 0x80000000) != 0 &&
               (result.data[lastPos] & 0x80000000) != (bi1.data[lastPos] & 0x80000000))
            {
                throw (new ArithmeticException("Underflow in --."));
            }

            return result;
        }

        public static BigInteger operator *(BigInteger bi1, BigInteger bi2)
        {
            int lastPos = maxLength - 1;
            bool bi1Neg = false, bi2Neg = false;
            try
            {
                if ((bi1.data[lastPos] & 0x80000000) != 0)
                {
                    bi1Neg = true; bi1 = -bi1;
                }
                if ((bi2.data[lastPos] & 0x80000000) != 0)
                {
                    bi2Neg = true; bi2 = -bi2;
                }
            }
            catch (Exception) { }

            BigInteger result = new BigInteger();
            try
            {
                for (int i = 0; i < bi1.dataLength; i++)
                {
                    if (bi1.data[i] == 0) continue;

                    ulong mcarry = 0;
                    for (int j = 0, k = i; j < bi2.dataLength; j++, k++)
                    {
                        ulong val = (bi1.data[i] * (ulong)bi2.data[j]) +
                                     result.data[k] + mcarry;

                        result.data[k] = (uint)(val & 0xFFFFFFFF);
                        mcarry = (val >> 32);
                    }

                    if (mcarry != 0)
                        result.data[i + bi2.dataLength] = (uint)mcarry;
                }
            }
            catch (Exception)
            {
                throw (new ArithmeticException("Multiplication overflow."));
            }


            result.dataLength = bi1.dataLength + bi2.dataLength;
            if (result.dataLength > maxLength)
                result.dataLength = maxLength;

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;
            if ((result.data[lastPos] & 0x80000000) != 0)
            {
                if (bi1Neg != bi2Neg && result.data[lastPos] == 0x80000000)    // different sign
                {
                    if (result.dataLength == 1)
                        return result;
                    else
                    {
                        bool isMaxNeg = true;
                        for (int i = 0; i < result.dataLength - 1 && isMaxNeg; i++)
                        {
                            if (result.data[i] != 0)
                                isMaxNeg = false;
                        }

                        if (isMaxNeg)
                            return result;
                    }
                }

                throw (new ArithmeticException("Multiplication overflow."));
            }
            if (bi1Neg != bi2Neg)
                return -result;

            return result;
        }

        public static BigInteger operator <<(BigInteger bi1, int shiftVal)
        {
            BigInteger result = new BigInteger(bi1);
            result.dataLength = shiftLeft(result.data, shiftVal);

            return result;
        }

        private static int shiftLeft(uint[] buffer, int shiftVal)
        {
            int shiftAmount = 32;
            int bufLen = buffer.Length;

            while (bufLen > 1 && buffer[bufLen - 1] == 0)
                bufLen--;

            for (int count = shiftVal; count > 0;)
            {
                if (count < shiftAmount)
                    shiftAmount = count;
                ulong carry = 0;
                for (int i = 0; i < bufLen; i++)
                {
                    ulong val = ((ulong)buffer[i]) << shiftAmount;
                    val |= carry;

                    buffer[i] = (uint)(val & 0xFFFFFFFF);
                    carry = val >> 32;
                }

                if (carry != 0)
                {
                    if (bufLen + 1 <= buffer.Length)
                    {
                        buffer[bufLen] = (uint)carry;
                        bufLen++;
                    }
                }
                count -= shiftAmount;
            }
            return bufLen;
        }
        public static BigInteger operator >>(BigInteger bi1, int shiftVal)
        {
            BigInteger result = new BigInteger(bi1);
            result.dataLength = shiftRight(result.data, shiftVal);
            if ((bi1.data[maxLength - 1] & 0x80000000) != 0)
            {
                for (int i = maxLength - 1; i >= result.dataLength; i--)
                    result.data[i] = 0xFFFFFFFF;

                uint mask = 0x80000000;
                for (int i = 0; i < 32; i++)
                {
                    if ((result.data[result.dataLength - 1] & mask) != 0)
                        break;

                    result.data[result.dataLength - 1] |= mask;
                    mask >>= 1;
                }
                result.dataLength = maxLength;
            }

            return result;
        }


        private static int shiftRight(uint[] buffer, int shiftVal)
        {
            int shiftAmount = 32;
            int invShift = 0;
            int bufLen = buffer.Length;

            while (bufLen > 1 && buffer[bufLen - 1] == 0)
                bufLen--;
            for (int count = shiftVal; count > 0;)
            {
                if (count < shiftAmount)
                {
                    shiftAmount = count;
                    invShift = 32 - shiftAmount;
                }
                ulong carry = 0;
                for (int i = bufLen - 1; i >= 0; i--)
                {
                    ulong val = ((ulong)buffer[i]) >> shiftAmount;
                    val |= carry;

                    carry = ((ulong)buffer[i]) << invShift;
                    buffer[i] = (uint)(val);
                }

                count -= shiftAmount;
            }

            while (bufLen > 1 && buffer[bufLen - 1] == 0)
                bufLen--;

            return bufLen;
        }

        public static BigInteger operator ~(BigInteger bi1)
        {
            BigInteger result = new BigInteger(bi1);

            for (int i = 0; i < maxLength; i++)
                result.data[i] = ~(bi1.data[i]);

            result.dataLength = maxLength;

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;

            return result;
        }

        public static BigInteger operator -(BigInteger bi1)
        {
            if (bi1.dataLength == 1 && bi1.data[0] == 0)
                return (new BigInteger());

            BigInteger result = new BigInteger(bi1);
            for (int i = 0; i < maxLength; i++)
                result.data[i] = ~(bi1.data[i]);
            long val, carry = 1;
            int index = 0;

            while (carry != 0 && index < maxLength)
            {
                val = result.data[index];
                val++;

                result.data[index] = (uint)(val & 0xFFFFFFFF);
                carry = val >> 32;

                index++;
            }

            if ((bi1.data[maxLength - 1] & 0x80000000) == (result.data[maxLength - 1] & 0x80000000))
                throw (new ArithmeticException("Overflow in negation.\n"));

            result.dataLength = maxLength;

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;
            return result;
        }

        public static bool operator ==(BigInteger bi1, BigInteger bi2)
        {
            return bi1.Equals(bi2);
        }

        public static bool operator !=(BigInteger bi1, BigInteger bi2)
        {
            return !(bi1.Equals(bi2));
        }

        public override bool Equals(object o)
        {
            BigInteger bi = (BigInteger)o;

            if (this.dataLength != bi.dataLength)
                return false;

            for (int i = 0; i < this.dataLength; i++)
            {
                if (this.data[i] != bi.data[i])
                    return false;
            }
            return true;
        }


        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static bool operator >(BigInteger bi1, BigInteger bi2)
        {
            int pos = maxLength - 1;
            if ((bi1.data[pos] & 0x80000000) != 0 && (bi2.data[pos] & 0x80000000) == 0)
                return false;
            else if ((bi1.data[pos] & 0x80000000) == 0 && (bi2.data[pos] & 0x80000000) != 0)
                return true;
            int len = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
            for (pos = len - 1; pos >= 0 && bi1.data[pos] == bi2.data[pos]; pos--) ;

            if (pos >= 0)
            {
                if (bi1.data[pos] > bi2.data[pos])
                    return true;
                return false;
            }
            return false;
        }


        public static bool operator <(BigInteger bi1, BigInteger bi2)
        {
            int pos = maxLength - 1;
            if ((bi1.data[pos] & 0x80000000) != 0 && (bi2.data[pos] & 0x80000000) == 0)
                return true;
            else if ((bi1.data[pos] & 0x80000000) == 0 && (bi2.data[pos] & 0x80000000) != 0)
                return false;
            int len = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;
            for (pos = len - 1; pos >= 0 && bi1.data[pos] == bi2.data[pos]; pos--) ;

            if (pos >= 0)
            {
                if (bi1.data[pos] < bi2.data[pos])
                    return true;
                return false;
            }
            return false;
        }


        public static bool operator >=(BigInteger bi1, BigInteger bi2)
        {
            return (bi1 == bi2 || bi1 > bi2);
        }


        public static bool operator <=(BigInteger bi1, BigInteger bi2)
        {
            return (bi1 == bi2 || bi1 < bi2);
        }

        private static void multiByteDivide(BigInteger bi1, BigInteger bi2,
                                            BigInteger outQuotient, BigInteger outRemainder)
        {
            uint[] result = new uint[maxLength];

            int remainderLen = bi1.dataLength + 1;
            uint[] remainder = new uint[remainderLen];

            uint mask = 0x80000000;
            uint val = bi2.data[bi2.dataLength - 1];
            int shift = 0, resultPos = 0;

            while (mask != 0 && (val & mask) == 0)
            {
                shift++; mask >>= 1;
            }
            for (int i = 0; i < bi1.dataLength; i++)
                remainder[i] = bi1.data[i];
            shiftLeft(remainder, shift);
            bi2 = bi2 << shift;
            int j = remainderLen - bi2.dataLength;
            int pos = remainderLen - 1;

            ulong firstDivisorByte = bi2.data[bi2.dataLength - 1];
            ulong secondDivisorByte = bi2.data[bi2.dataLength - 2];

            int divisorLen = bi2.dataLength + 1;
            uint[] dividendPart = new uint[divisorLen];

            while (j > 0)
            {
                ulong dividend = ((ulong)remainder[pos] << 32) + remainder[pos - 1];
                ulong q_hat = dividend / firstDivisorByte;
                ulong r_hat = dividend % firstDivisorByte;
                bool done = false;
                while (!done)
                {
                    done = true;

                    if (q_hat == 0x100000000 ||
                       (q_hat * secondDivisorByte) > ((r_hat << 32) + remainder[pos - 2]))
                    {
                        q_hat--;
                        r_hat += firstDivisorByte;

                        if (r_hat < 0x100000000)
                            done = false;
                    }
                }

                for (int h = 0; h < divisorLen; h++)
                    dividendPart[h] = remainder[pos - h];

                BigInteger kk = new BigInteger(dividendPart);
                BigInteger ss = bi2 * (long)q_hat;
                while (ss > kk)
                {
                    q_hat--;
                    ss -= bi2;
                }
                BigInteger yy = kk - ss;
                for (int h = 0; h < divisorLen; h++)
                    remainder[pos - h] = yy.data[bi2.dataLength - h];
                result[resultPos++] = (uint)q_hat;

                pos--;
                j--;
            }

            outQuotient.dataLength = resultPos;
            int y = 0;
            for (int x = outQuotient.dataLength - 1; x >= 0; x--, y++)
                outQuotient.data[y] = result[x];
            for (; y < maxLength; y++)
                outQuotient.data[y] = 0;

            while (outQuotient.dataLength > 1 && outQuotient.data[outQuotient.dataLength - 1] == 0)
                outQuotient.dataLength--;

            if (outQuotient.dataLength == 0)
                outQuotient.dataLength = 1;

            outRemainder.dataLength = shiftRight(remainder, shift);

            for (y = 0; y < outRemainder.dataLength; y++)
                outRemainder.data[y] = remainder[y];
            for (; y < maxLength; y++)
                outRemainder.data[y] = 0;
        }

        private static void singleByteDivide(BigInteger bi1, BigInteger bi2,
                                             BigInteger outQuotient, BigInteger outRemainder)
        {
            uint[] result = new uint[maxLength];
            int resultPos = 0;
            for (int i = 0; i < maxLength; i++)
                outRemainder.data[i] = bi1.data[i];
            outRemainder.dataLength = bi1.dataLength;

            while (outRemainder.dataLength > 1 && outRemainder.data[outRemainder.dataLength - 1] == 0)
                outRemainder.dataLength--;

            ulong divisor = bi2.data[0];
            int pos = outRemainder.dataLength - 1;
            ulong dividend = outRemainder.data[pos];
            if (dividend >= divisor)
            {
                ulong quotient = dividend / divisor;
                result[resultPos++] = (uint)quotient;

                outRemainder.data[pos] = (uint)(dividend % divisor);
            }
            pos--;

            while (pos >= 0)
            {
                dividend = ((ulong)outRemainder.data[pos + 1] << 32) + outRemainder.data[pos];
                ulong quotient = dividend / divisor;
                result[resultPos++] = (uint)quotient;

                outRemainder.data[pos + 1] = 0;
                outRemainder.data[pos--] = (uint)(dividend % divisor);
            }

            outQuotient.dataLength = resultPos;
            int j = 0;
            for (int i = outQuotient.dataLength - 1; i >= 0; i--, j++)
                outQuotient.data[j] = result[i];
            for (; j < maxLength; j++)
                outQuotient.data[j] = 0;

            while (outQuotient.dataLength > 1 && outQuotient.data[outQuotient.dataLength - 1] == 0)
                outQuotient.dataLength--;

            if (outQuotient.dataLength == 0)
                outQuotient.dataLength = 1;

            while (outRemainder.dataLength > 1 && outRemainder.data[outRemainder.dataLength - 1] == 0)
                outRemainder.dataLength--;
        }

        public static BigInteger operator /(BigInteger bi1, BigInteger bi2)
        {
            BigInteger quotient = new BigInteger();
            BigInteger remainder = new BigInteger();

            int lastPos = maxLength - 1;
            bool divisorNeg = false, dividendNeg = false;

            if ((bi1.data[lastPos] & 0x80000000) != 0)
            {
                bi1 = -bi1;
                dividendNeg = true;
            }
            if ((bi2.data[lastPos] & 0x80000000) != 0)
            {
                bi2 = -bi2;
                divisorNeg = true;
            }

            if (bi1 < bi2)
            {
                return quotient;
            }

            else
            {
                if (bi2.dataLength == 1)
                    singleByteDivide(bi1, bi2, quotient, remainder);
                else
                    multiByteDivide(bi1, bi2, quotient, remainder);

                if (dividendNeg != divisorNeg)
                    return -quotient;

                return quotient;
            }
        }

        public static BigInteger operator %(BigInteger bi1, BigInteger bi2)
        {
            BigInteger quotient = new BigInteger();
            BigInteger remainder = new BigInteger(bi1);

            int lastPos = maxLength - 1;
            bool dividendNeg = false;

            if ((bi1.data[lastPos] & 0x80000000) != 0)
            {
                bi1 = -bi1;
                dividendNeg = true;
            }
            if ((bi2.data[lastPos] & 0x80000000) != 0)
                bi2 = -bi2;

            if (bi1 < bi2)
            {
                return remainder;
            }

            else
            {
                if (bi2.dataLength == 1)
                    singleByteDivide(bi1, bi2, quotient, remainder);
                else
                    multiByteDivide(bi1, bi2, quotient, remainder);

                if (dividendNeg)
                    return -remainder;

                return remainder;
            }
        }

        public static BigInteger operator &(BigInteger bi1, BigInteger bi2)
        {
            BigInteger result = new BigInteger();

            int len = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;

            for (int i = 0; i < len; i++)
            {
                uint sum = bi1.data[i] & bi2.data[i];
                result.data[i] = sum;
            }

            result.dataLength = maxLength;

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;

            return result;
        }

        public static BigInteger operator |(BigInteger bi1, BigInteger bi2)
        {
            BigInteger result = new BigInteger();

            int len = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;

            for (int i = 0; i < len; i++)
            {
                uint sum = bi1.data[i] | bi2.data[i];
                result.data[i] = sum;
            }

            result.dataLength = maxLength;

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;

            return result;
        }

        public static BigInteger operator ^(BigInteger bi1, BigInteger bi2)
        {
            BigInteger result = new BigInteger();

            int len = (bi1.dataLength > bi2.dataLength) ? bi1.dataLength : bi2.dataLength;

            for (int i = 0; i < len; i++)
            {
                uint sum = bi1.data[i] ^ bi2.data[i];
                result.data[i] = sum;
            }

            result.dataLength = maxLength;

            while (result.dataLength > 1 && result.data[result.dataLength - 1] == 0)
                result.dataLength--;

            return result;
        }

        public BigInteger max(BigInteger bi)
        {
            if (this > bi)
                return (new BigInteger(this));
            else
                return (new BigInteger(bi));
        }

        public BigInteger min(BigInteger bi)
        {
            if (this < bi)
                return (new BigInteger(this));
            else
                return (new BigInteger(bi));

        }

        public BigInteger abs()
        {
            if ((this.data[maxLength - 1] & 0x80000000) != 0)
                return (-this);
            else
                return (new BigInteger(this));
        }

        public override string ToString()
        {
            return ToString(10);
        }

        public string ToString(int radix)
        {
            if (radix < 2 || radix > 36)
                throw (new ArgumentException("Radix must be >= 2 and <= 36"));

            string charSet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = "";

            BigInteger a = this;

            bool negative = false;
            if ((a.data[maxLength - 1] & 0x80000000) != 0)
            {
                negative = true;
                try
                {
                    a = -a;
                }
                catch (Exception) { }
            }

            BigInteger quotient = new BigInteger();
            BigInteger remainder = new BigInteger();
            BigInteger biRadix = new BigInteger(radix);

            if (a.dataLength == 1 && a.data[0] == 0)
                result = "0";
            else
            {
                while (a.dataLength > 1 || (a.dataLength == 1 && a.data[0] != 0))
                {
                    singleByteDivide(a, biRadix, quotient, remainder);

                    if (remainder.data[0] < 10)
                        result = remainder.data[0] + result;
                    else
                        result = charSet[(int)remainder.data[0] - 10] + result;

                    a = quotient;
                }
                if (negative)
                    result = "-" + result;
            }

            return result;
        }

        public string ToHexString()
        {
            string result = data[dataLength - 1].ToString("X");

            for (int i = dataLength - 2; i >= 0; i--)
            {
                result += data[i].ToString("X8");
            }

            return result;
        }

        public BigInteger modPow(BigInteger exp, BigInteger n)
        {
            if ((exp.data[maxLength - 1] & 0x80000000) != 0)
                throw (new ArithmeticException("Positive exponents only."));

            BigInteger resultNum = 1;
            BigInteger tempNum;
            bool thisNegative = false;

            if ((this.data[maxLength - 1] & 0x80000000) != 0)
            {
                tempNum = -this % n;
                thisNegative = true;
            }
            else
                tempNum = this % n;

            if ((n.data[maxLength - 1] & 0x80000000) != 0)
                n = -n;
            BigInteger constant = new BigInteger();

            int i = n.dataLength << 1;
            constant.data[i] = 0x00000001;
            constant.dataLength = i + 1;

            constant = constant / n;
            int totalBits = exp.bitCount();
            int count = 0;

            for (int pos = 0; pos < exp.dataLength; pos++)
            {
                uint mask = 0x01;

                for (int index = 0; index < 32; index++)
                {
                    if ((exp.data[pos] & mask) != 0)
                        resultNum = BarrettReduction(resultNum * tempNum, n, constant);

                    mask <<= 1;

                    tempNum = BarrettReduction(tempNum * tempNum, n, constant);


                    if (tempNum.dataLength == 1 && tempNum.data[0] == 1)
                    {
                        if (thisNegative && (exp.data[0] & 0x1) != 0)
                            return -resultNum;
                        return resultNum;
                    }
                    count++;
                    if (count == totalBits)
                        break;
                }
            }

            if (thisNegative && (exp.data[0] & 0x1) != 0)
                return -resultNum;

            return resultNum;
        }

        private BigInteger BarrettReduction(BigInteger x, BigInteger n, BigInteger constant)
        {
            int k = n.dataLength,
                kPlusOne = k + 1,
                kMinusOne = k - 1;

            BigInteger q1 = new BigInteger();
            for (int i = kMinusOne, j = 0; i < x.dataLength; i++, j++)
                q1.data[j] = x.data[i];
            q1.dataLength = x.dataLength - kMinusOne;
            if (q1.dataLength <= 0)
                q1.dataLength = 1;


            BigInteger q2 = q1 * constant;
            BigInteger q3 = new BigInteger();
            for (int i = kPlusOne, j = 0; i < q2.dataLength; i++, j++)
                q3.data[j] = q2.data[i];
            q3.dataLength = q2.dataLength - kPlusOne;
            if (q3.dataLength <= 0)
                q3.dataLength = 1;

            BigInteger r1 = new BigInteger();
            int lengthToCopy = (x.dataLength > kPlusOne) ? kPlusOne : x.dataLength;
            for (int i = 0; i < lengthToCopy; i++)
                r1.data[i] = x.data[i];
            r1.dataLength = lengthToCopy;

            BigInteger r2 = new BigInteger();
            for (int i = 0; i < q3.dataLength; i++)
            {
                if (q3.data[i] == 0) continue;

                ulong mcarry = 0;
                int t = i;
                for (int j = 0; j < n.dataLength && t < kPlusOne; j++, t++)
                {
                    // t = i + j
                    ulong val = (q3.data[i] * (ulong)n.data[j]) +
                                 r2.data[t] + mcarry;

                    r2.data[t] = (uint)(val & 0xFFFFFFFF);
                    mcarry = (val >> 32);
                }

                if (t < kPlusOne)
                    r2.data[t] = (uint)mcarry;
            }
            r2.dataLength = kPlusOne;
            while (r2.dataLength > 1 && r2.data[r2.dataLength - 1] == 0)
                r2.dataLength--;

            r1 -= r2;
            if ((r1.data[maxLength - 1] & 0x80000000) != 0)
            {
                BigInteger val = new BigInteger();
                val.data[kPlusOne] = 0x00000001;
                val.dataLength = kPlusOne + 1;
                r1 += val;
            }

            while (r1 >= n)
                r1 -= n;

            return r1;
        }

        public BigInteger gcd(BigInteger bi)
        {
            BigInteger x;
            BigInteger y;

            if ((data[maxLength - 1] & 0x80000000) != 0)
                x = -this;
            else
                x = this;

            if ((bi.data[maxLength - 1] & 0x80000000) != 0)
                y = -bi;
            else
                y = bi;

            BigInteger g = y;

            while (x.dataLength > 1 || (x.dataLength == 1 && x.data[0] != 0))
            {
                g = x;
                x = y % x;
                y = g;
            }

            return g;
        }

        public void genRandomBits(int bits, Random rand)
        {
            int dwords = bits >> 5;
            int remBits = bits & 0x1F;

            if (remBits != 0)
                dwords++;

            if (dwords > maxLength)
                throw (new ArithmeticException("Number of required bits > maxLength."));

            for (int i = 0; i < dwords; i++)
                data[i] = (uint)(rand.NextDouble() * 0x100000000);

            for (int i = dwords; i < maxLength; i++)
                data[i] = 0;

            if (remBits != 0)
            {
                uint mask = (uint)(0x01 << (remBits - 1));
                data[dwords - 1] |= mask;

                mask = 0xFFFFFFFF >> (32 - remBits);
                data[dwords - 1] &= mask;
            }
            else
                data[dwords - 1] |= 0x80000000;

            dataLength = dwords;

            if (dataLength == 0)
                dataLength = 1;
        }

        public int bitCount()
        {
            while (dataLength > 1 && data[dataLength - 1] == 0)
                dataLength--;

            uint value = data[dataLength - 1];
            uint mask = 0x80000000;
            int bits = 32;

            while (bits > 0 && (value & mask) == 0)
            {
                bits--;
                mask >>= 1;
            }
            bits += ((dataLength - 1) << 5);

            return bits;
        }

        public bool FermatLittleTest(int confidence)
        {
            BigInteger thisVal;
            if ((this.data[maxLength - 1] & 0x80000000) != 0)
                thisVal = -this;
            else
                thisVal = this;

            if (thisVal.dataLength == 1)
            {
                if (thisVal.data[0] == 0 || thisVal.data[0] == 1)
                    return false;
                else if (thisVal.data[0] == 2 || thisVal.data[0] == 3)
                    return true;
            }

            if ((thisVal.data[0] & 0x1) == 0)
                return false;

            int bits = thisVal.bitCount();
            BigInteger a = new BigInteger();
            BigInteger p_sub1 = thisVal - (new BigInteger(1));
            Random rand = new Random();

            for (int round = 0; round < confidence; round++)
            {
                bool done = false;

                while (!done)
                {
                    int testBits = 0;

                    while (testBits < 2)
                        testBits = (int)(rand.NextDouble() * bits);

                    a.genRandomBits(testBits, rand);

                    int byteLen = a.dataLength;

                    if (byteLen > 1 || (byteLen == 1 && a.data[0] != 1))
                        done = true;
                }

                BigInteger gcdTest = a.gcd(thisVal);
                if (gcdTest.dataLength == 1 && gcdTest.data[0] != 1)
                    return false;

                BigInteger expResult = a.modPow(p_sub1, thisVal);

                int resultLen = expResult.dataLength;

                if (resultLen > 1 || (resultLen == 1 && expResult.data[0] != 1))
                {
                    return false;
                }
            }

            return true;
        }

        public bool RabinMillerTest(int confidence)
        {
            BigInteger thisVal;
            if ((this.data[maxLength - 1] & 0x80000000) != 0)
                thisVal = -this;
            else
                thisVal = this;

            if (thisVal.dataLength == 1)
            {
                if (thisVal.data[0] == 0 || thisVal.data[0] == 1)
                    return false;
                else if (thisVal.data[0] == 2 || thisVal.data[0] == 3)
                    return true;
            }

            if ((thisVal.data[0] & 0x1) == 0)
                return false;

            BigInteger p_sub1 = thisVal - (new BigInteger(1));
            int s = 0;

            for (int index = 0; index < p_sub1.dataLength; index++)
            {
                uint mask = 0x01;

                for (int i = 0; i < 32; i++)
                {
                    if ((p_sub1.data[index] & mask) != 0)
                    {
                        index = p_sub1.dataLength;
                        break;
                    }
                    mask <<= 1;
                    s++;
                }
            }

            BigInteger t = p_sub1 >> s;

            int bits = thisVal.bitCount();
            BigInteger a = new BigInteger();
            Random rand = new Random();

            for (int round = 0; round < confidence; round++)
            {
                bool done = false;

                while (!done)
                {
                    int testBits = 0;

                    while (testBits < 2)
                        testBits = (int)(rand.NextDouble() * bits);

                    a.genRandomBits(testBits, rand);

                    int byteLen = a.dataLength;

                    if (byteLen > 1 || (byteLen == 1 && a.data[0] != 1))
                        done = true;
                }

                BigInteger gcdTest = a.gcd(thisVal);
                if (gcdTest.dataLength == 1 && gcdTest.data[0] != 1)
                    return false;

                BigInteger b = a.modPow(t, thisVal);

                bool result = false;

                if (b.dataLength == 1 && b.data[0] == 1)
                    result = true;

                for (int j = 0; result == false && j < s; j++)
                {
                    if (b == p_sub1)
                    {
                        result = true;
                        break;
                    }

                    b = (b * b) % thisVal;
                }

                if (result == false)
                    return false;
            }
            return true;
        }

        public bool SolovayStrassenTest(int confidence)
        {
            BigInteger thisVal;
            if ((this.data[maxLength - 1] & 0x80000000) != 0)
                thisVal = -this;
            else
                thisVal = this;

            if (thisVal.dataLength == 1)
            {
                // test small numbers
                if (thisVal.data[0] == 0 || thisVal.data[0] == 1)
                    return false;
                else if (thisVal.data[0] == 2 || thisVal.data[0] == 3)
                    return true;
            }

            if ((thisVal.data[0] & 0x1) == 0)
                return false;


            int bits = thisVal.bitCount();
            BigInteger a = new BigInteger();
            BigInteger p_sub1 = thisVal - 1;
            BigInteger p_sub1_shift = p_sub1 >> 1;

            Random rand = new Random();

            for (int round = 0; round < confidence; round++)
            {
                bool done = false;

                while (!done)
                {
                    int testBits = 0;
                    while (testBits < 2)
                        testBits = (int)(rand.NextDouble() * bits);

                    a.genRandomBits(testBits, rand);

                    int byteLen = a.dataLength;
                    if (byteLen > 1 || (byteLen == 1 && a.data[0] != 1))
                        done = true;
                }
                BigInteger gcdTest = a.gcd(thisVal);
                if (gcdTest.dataLength == 1 && gcdTest.data[0] != 1)
                    return false;
                BigInteger expResult = a.modPow(p_sub1_shift, thisVal);
                if (expResult == p_sub1)
                    expResult = -1;
                BigInteger jacob = Jacobi(a, thisVal);
                if (expResult != jacob)
                    return false;
            }

            return true;
        }
        public bool LucasStrongTest()
        {
            BigInteger thisVal;
            if ((this.data[maxLength - 1] & 0x80000000) != 0)
                thisVal = -this;
            else
                thisVal = this;

            if (thisVal.dataLength == 1)
            {
                if (thisVal.data[0] == 0 || thisVal.data[0] == 1)
                    return false;
                else if (thisVal.data[0] == 2 || thisVal.data[0] == 3)
                    return true;
            }

            if ((thisVal.data[0] & 0x1) == 0)
                return false;

            return LucasStrongTestHelper(thisVal);
        }


        private bool LucasStrongTestHelper(BigInteger thisVal)
        {
            long D = 5, sign = -1, dCount = 0;
            bool done = false;

            while (!done)
            {
                int Jresult = BigInteger.Jacobi(D, thisVal);

                if (Jresult == -1)
                    done = true;
                else
                {
                    if (Jresult == 0 && Math.Abs(D) < thisVal)
                        return false;

                    if (dCount == 20)
                    {
                        BigInteger root = thisVal.sqrt();
                        if (root * root == thisVal)
                            return false;
                    }

                    D = (Math.Abs(D) + 2) * sign;
                    sign = -sign;
                }
                dCount++;
            }

            long Q = (1 - D) >> 2;

            BigInteger p_add1 = thisVal + 1;
            int s = 0;

            for (int index = 0; index < p_add1.dataLength; index++)
            {
                uint mask = 0x01;

                for (int i = 0; i < 32; i++)
                {
                    if ((p_add1.data[index] & mask) != 0)
                    {
                        index = p_add1.dataLength;
                        break;
                    }
                    mask <<= 1;
                    s++;
                }
            }

            BigInteger t = p_add1 >> s;
            BigInteger constant = new BigInteger();

            int nLen = thisVal.dataLength << 1;
            constant.data[nLen] = 0x00000001;
            constant.dataLength = nLen + 1;

            constant = constant / thisVal;

            BigInteger[] lucas = LucasSequenceHelper(1, Q, t, thisVal, constant, 0);
            bool isPrime = false;

            if ((lucas[0].dataLength == 1 && lucas[0].data[0] == 0) ||
               (lucas[1].dataLength == 1 && lucas[1].data[0] == 0))
            {
                isPrime = true;
            }

            for (int i = 1; i < s; i++)
            {
                if (!isPrime)
                {
                    lucas[1] = thisVal.BarrettReduction(lucas[1] * lucas[1], thisVal, constant);
                    lucas[1] = (lucas[1] - (lucas[2] << 1)) % thisVal;

                    if ((lucas[1].dataLength == 1 && lucas[1].data[0] == 0))
                        isPrime = true;
                }

                lucas[2] = thisVal.BarrettReduction(lucas[2] * lucas[2], thisVal, constant);
            }


            if (isPrime)
            {
                BigInteger g = thisVal.gcd(Q);
                if (g.dataLength == 1 && g.data[0] == 1)
                {
                    if ((lucas[2].data[maxLength - 1] & 0x80000000) != 0)
                        lucas[2] += thisVal;

                    BigInteger temp = (Q * BigInteger.Jacobi(Q, thisVal)) % thisVal;
                    if ((temp.data[maxLength - 1] & 0x80000000) != 0)
                        temp += thisVal;

                    if (lucas[2] != temp)
                        isPrime = false;
                }
            }

            return isPrime;
        }

        public bool isProbablePrime(int confidence)
        {
            BigInteger thisVal;
            if ((this.data[maxLength - 1] & 0x80000000) != 0)        // negative
                thisVal = -this;
            else
                thisVal = this;

            for (int p = 0; p < primesBelow2000.Length; p++)
            {
                BigInteger divisor = primesBelow2000[p];

                if (divisor >= thisVal)
                    break;

                BigInteger resultNum = thisVal % divisor;
                if (resultNum.IntValue() == 0)
                {
                    return false;
                }
            }

            if (thisVal.RabinMillerTest(confidence))
                return true;
            else
            {
                return false;
            }
        }

        public bool isProbablePrime()
        {
            BigInteger thisVal;
            if ((this.data[maxLength - 1] & 0x80000000) != 0)
                thisVal = -this;
            else
                thisVal = this;

            if (thisVal.dataLength == 1)
            {
                if (thisVal.data[0] == 0 || thisVal.data[0] == 1)
                    return false;
                else if (thisVal.data[0] == 2 || thisVal.data[0] == 3)
                    return true;
            }

            if ((thisVal.data[0] & 0x1) == 0)
                return false;

            for (int p = 0; p < primesBelow2000.Length; p++)
            {
                BigInteger divisor = primesBelow2000[p];

                if (divisor >= thisVal)
                    break;

                BigInteger resultNum = thisVal % divisor;
                if (resultNum.IntValue() == 0)
                {
                    return false;
                }
            }
            BigInteger p_sub1 = thisVal - (new BigInteger(1));
            int s = 0;

            for (int index = 0; index < p_sub1.dataLength; index++)
            {
                uint mask = 0x01;

                for (int i = 0; i < 32; i++)
                {
                    if ((p_sub1.data[index] & mask) != 0)
                    {
                        index = p_sub1.dataLength;
                        break;
                    }
                    mask <<= 1;
                    s++;
                }
            }

            BigInteger t = p_sub1 >> s;

            int bits = thisVal.bitCount();
            BigInteger a = 2;

            BigInteger b = a.modPow(t, thisVal);
            bool result = false;

            if (b.dataLength == 1 && b.data[0] == 1)
                result = true;

            for (int j = 0; result == false && j < s; j++)
            {
                if (b == p_sub1)
                {
                    result = true;
                    break;
                }

                b = (b * b) % thisVal;
            }

            if (result)
                result = LucasStrongTestHelper(thisVal);

            return result;
        }

        public int IntValue()
        {
            return (int)data[0];
        }

        public long LongValue()
        {
            long val = 0;

            val = data[0];
            try
            {
                val |= (long)data[1] << 32;
            }
            catch (Exception)
            {
                if ((data[0] & 0x80000000) != 0)
                    val = (int)data[0];
            }

            return val;
        }

        public static int Jacobi(BigInteger a, BigInteger b)
        {

            if ((b.data[0] & 0x1) == 0)
                throw (new ArgumentException("Jacobi defined only for odd integers."));

            if (a >= b) a %= b;
            if (a.dataLength == 1 && a.data[0] == 0) return 0;
            if (a.dataLength == 1 && a.data[0] == 1) return 1;

            if (a < 0)
            {
                if ((((b - 1).data[0]) & 0x2) == 0)
                    return Jacobi(-a, b);
                else
                    return -Jacobi(-a, b);
            }

            int e = 0;
            for (int index = 0; index < a.dataLength; index++)
            {
                uint mask = 0x01;

                for (int i = 0; i < 32; i++)
                {
                    if ((a.data[index] & mask) != 0)
                    {
                        index = a.dataLength;
                        break;
                    }
                    mask <<= 1;
                    e++;
                }
            }

            BigInteger a1 = a >> e;

            int s = 1;
            if ((e & 0x1) != 0 && ((b.data[0] & 0x7) == 3 || (b.data[0] & 0x7) == 5))
                s = -1;

            if ((b.data[0] & 0x3) == 3 && (a1.data[0] & 0x3) == 3)
                s = -s;

            if (a1.dataLength == 1 && a1.data[0] == 1)
                return s;
            else
                return (s * Jacobi(b % a1, a1));
        }

        public static BigInteger genPseudoPrime(int bits, int confidence, Random rand)
        {
            BigInteger result = new BigInteger();
            bool done = false;

            while (!done)
            {
                result.genRandomBits(bits, rand);
                result.data[0] |= 0x01;

                done = result.isProbablePrime(confidence);
            }
            return result;
        }

        public BigInteger genCoPrime(int bits, Random rand)
        {
            bool done = false;
            BigInteger result = new BigInteger();

            while (!done)
            {
                result.genRandomBits(bits, rand);
                BigInteger g = result.gcd(this);
                if (g.dataLength == 1 && g.data[0] == 1)
                    done = true;
            }

            return result;
        }

        public BigInteger modInverse(BigInteger modulus)
        {
            BigInteger[] p = { 0, 1 };
            BigInteger[] q = new BigInteger[2];
            BigInteger[] r = { 0, 0 };

            int step = 0;

            BigInteger a = modulus;
            BigInteger b = this;

            while (b.dataLength > 1 || (b.dataLength == 1 && b.data[0] != 0))
            {
                BigInteger quotient = new BigInteger();
                BigInteger remainder = new BigInteger();

                if (step > 1)
                {
                    BigInteger pval = (p[0] - (p[1] * q[0])) % modulus;
                    p[0] = p[1];
                    p[1] = pval;
                }

                if (b.dataLength == 1)
                    singleByteDivide(a, b, quotient, remainder);
                else
                    multiByteDivide(a, b, quotient, remainder);
                q[0] = q[1];
                r[0] = r[1];
                q[1] = quotient; r[1] = remainder;

                a = b;
                b = remainder;

                step++;
            }

            if (r[0].dataLength > 1 || (r[0].dataLength == 1 && r[0].data[0] != 1))
                throw (new ArithmeticException("No inverse!"));

            BigInteger result = ((p[0] - (p[1] * q[0])) % modulus);

            if ((result.data[maxLength - 1] & 0x80000000) != 0)
                result += modulus;

            return result;
        }

        public byte[] getBytes()
        {
            int numBits = bitCount();

            int numBytes = numBits >> 3;
            if ((numBits & 0x7) != 0)
                numBytes++;

            byte[] result = new byte[numBytes];

            int pos = 0;
            uint tempVal, val = data[dataLength - 1];

            if ((tempVal = (val >> 24 & 0xFF)) != 0)
                result[pos++] = (byte)tempVal;
            if ((tempVal = (val >> 16 & 0xFF)) != 0)
                result[pos++] = (byte)tempVal;
            if ((tempVal = (val >> 8 & 0xFF)) != 0)
                result[pos++] = (byte)tempVal;
            if ((tempVal = (val & 0xFF)) != 0)
                result[pos++] = (byte)tempVal;

            for (int i = dataLength - 2; i >= 0; i--, pos += 4)
            {
                val = data[i];
                result[pos + 3] = (byte)(val & 0xFF);
                val >>= 8;
                result[pos + 2] = (byte)(val & 0xFF);
                val >>= 8;
                result[pos + 1] = (byte)(val & 0xFF);
                val >>= 8;
                result[pos] = (byte)(val & 0xFF);
            }

            return result;
        }

        public void setBit(uint bitNum)
        {
            uint bytePos = bitNum >> 5;
            byte bitPos = (byte)(bitNum & 0x1F);

            uint mask = (uint)1 << bitPos;
            this.data[bytePos] |= mask;

            if (bytePos >= this.dataLength)
                this.dataLength = (int)bytePos + 1;
        }

        public void unsetBit(uint bitNum)
        {
            uint bytePos = bitNum >> 5;

            if (bytePos < this.dataLength)
            {
                byte bitPos = (byte)(bitNum & 0x1F);

                uint mask = (uint)1 << bitPos;
                uint mask2 = 0xFFFFFFFF ^ mask;

                this.data[bytePos] &= mask2;

                if (this.dataLength > 1 && this.data[this.dataLength - 1] == 0)
                    this.dataLength--;
            }
        }

        public BigInteger sqrt()
        {
            uint numBits = (uint)this.bitCount();

            if ((numBits & 0x1) != 0)
                numBits = (numBits >> 1) + 1;
            else
                numBits = (numBits >> 1);

            uint bytePos = numBits >> 5;
            byte bitPos = (byte)(numBits & 0x1F);

            uint mask;

            BigInteger result = new BigInteger();
            if (bitPos == 0)
                mask = 0x80000000;
            else
            {
                mask = (uint)1 << bitPos;
                bytePos++;
            }
            result.dataLength = (int)bytePos;

            for (int i = (int)bytePos - 1; i >= 0; i--)
            {
                while (mask != 0)
                {
                    result.data[i] ^= mask;

                    if ((result * result) > this)
                        result.data[i] ^= mask;

                    mask >>= 1;
                }
                mask = 0x80000000;
            }
            return result;
        }

        public static BigInteger[] LucasSequence(BigInteger P, BigInteger Q,
                                                 BigInteger k, BigInteger n)
        {
            if (k.dataLength == 1 && k.data[0] == 0)
            {
                BigInteger[] result = new BigInteger[3];

                result[0] = 0; result[1] = 2 % n; result[2] = 1 % n;
                return result;
            }
            BigInteger constant = new BigInteger();

            int nLen = n.dataLength << 1;
            constant.data[nLen] = 0x00000001;
            constant.dataLength = nLen + 1;

            constant = constant / n;
            int s = 0;

            for (int index = 0; index < k.dataLength; index++)
            {
                uint mask = 0x01;

                for (int i = 0; i < 32; i++)
                {
                    if ((k.data[index] & mask) != 0)
                    {
                        index = k.dataLength;
                        break;
                    }
                    mask <<= 1;
                    s++;
                }
            }

            BigInteger t = k >> s;
            return LucasSequenceHelper(P, Q, t, n, constant, s);
        }

        private static BigInteger[] LucasSequenceHelper(BigInteger P, BigInteger Q,
                                                        BigInteger k, BigInteger n,
                                                        BigInteger constant, int s)
        {
            BigInteger[] result = new BigInteger[3];

            if ((k.data[0] & 0x00000001) == 0)
                throw (new ArgumentException("Argument k must be odd."));

            int numbits = k.bitCount();
            uint mask = (uint)0x1 << ((numbits & 0x1F) - 1);

            BigInteger v = 2 % n, Q_k = 1 % n,
                       v1 = P % n, u1 = Q_k;
            bool flag = true;

            for (int i = k.dataLength - 1; i >= 0; i--)
            {
                while (mask != 0)
                {
                    if (i == 0 && mask == 0x00000001)
                        break;

                    if ((k.data[i] & mask) != 0)
                    {
                        u1 = (u1 * v1) % n;

                        v = ((v * v1) - (P * Q_k)) % n;
                        v1 = n.BarrettReduction(v1 * v1, n, constant);
                        v1 = (v1 - ((Q_k * Q) << 1)) % n;

                        if (flag)
                            flag = false;
                        else
                            Q_k = n.BarrettReduction(Q_k * Q_k, n, constant);

                        Q_k = (Q_k * Q) % n;
                    }
                    else
                    {
                        u1 = ((u1 * v) - Q_k) % n;

                        v1 = ((v * v1) - (P * Q_k)) % n;
                        v = n.BarrettReduction(v * v, n, constant);
                        v = (v - (Q_k << 1)) % n;

                        if (flag)
                        {
                            Q_k = Q % n;
                            flag = false;
                        }
                        else
                            Q_k = n.BarrettReduction(Q_k * Q_k, n, constant);
                    }

                    mask >>= 1;
                }
                mask = 0x80000000;
            }

            u1 = ((u1 * v) - Q_k) % n;
            v = ((v * v1) - (P * Q_k)) % n;
            if (flag)
                flag = false;
            else
                Q_k = n.BarrettReduction(Q_k * Q_k, n, constant);

            Q_k = (Q_k * Q) % n;


            for (int i = 0; i < s; i++)
            {
                u1 = (u1 * v) % n;
                v = ((v * v) - (Q_k << 1)) % n;

                if (flag)
                {
                    Q_k = Q % n;
                    flag = false;
                }
                else
                    Q_k = n.BarrettReduction(Q_k * Q_k, n, constant);
            }

            result[0] = u1;
            result[1] = v;
            result[2] = Q_k;

            return result;
        }

        public static void MulDivTest(int rounds)
        {
            Random rand = new Random();
            byte[] val = new byte[64];
            byte[] val2 = new byte[64];

            for (int count = 0; count < rounds; count++)
            {
                int t1 = 0;
                while (t1 == 0)
                    t1 = (int)(rand.NextDouble() * 65);

                int t2 = 0;
                while (t2 == 0)
                    t2 = (int)(rand.NextDouble() * 65);

                bool done = false;
                while (!done)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (i < t1)
                            val[i] = (byte)(rand.NextDouble() * 256);
                        else
                            val[i] = 0;

                        if (val[i] != 0)
                            done = true;
                    }
                }

                done = false;
                while (!done)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (i < t2)
                            val2[i] = (byte)(rand.NextDouble() * 256);
                        else
                            val2[i] = 0;

                        if (val2[i] != 0)
                            done = true;
                    }
                }

                while (val[0] == 0)
                    val[0] = (byte)(rand.NextDouble() * 256);
                while (val2[0] == 0)
                    val2[0] = (byte)(rand.NextDouble() * 256);

                Console.WriteLine(count);
                BigInteger bn1 = new BigInteger(val, t1);
                BigInteger bn2 = new BigInteger(val2, t2);
                BigInteger bn3 = bn1 / bn2;
                BigInteger bn4 = bn1 % bn2;

                BigInteger bn5 = (bn3 * bn2) + bn4;
                if (bn5 != bn1)
                {
                    Console.WriteLine("Error at " + count);
                    Console.WriteLine(bn1 + "\n");
                    Console.WriteLine(bn2 + "\n");
                    Console.WriteLine(bn3 + "\n");
                    Console.WriteLine(bn4 + "\n");
                    Console.WriteLine(bn5 + "\n");
                    return;
                }
            }
        }

        public static void RSATest(int rounds)
        {
            Random rand = new Random(1);
            byte[] val = new byte[64];

            BigInteger bi_e = new BigInteger("a932b948feed4fb2b692609bd22164fc9edb59fae7880cc1eaff7b3c9626b7e5b241c27a974833b2622ebe09beb451917663d47232488f23a117fc97720f1e7", 16);
            BigInteger bi_d = new BigInteger("4adf2f7a89da93248509347d2ae506d683dd3a16357e859a980c4f77a4e2f7a01fae289f13a851df6e9db5adaa60bfd2b162bbbe31f7c8f828261a6839311929d2cef4f864dde65e556ce43c89bbbf9f1ac5511315847ce9cc8dc92470a747b8792d6a83b0092d2e5ebaf852c85cacf34278efa99160f2f8aa7ee7214de07b7", 16);
            BigInteger bi_n = new BigInteger("e8e77781f36a7b3188d711c2190b560f205a52391b3479cdb99fa010745cbeba5f2adc08e1de6bf38398a0487c4a73610d94ec36f17f3f46ad75e17bc1adfec99839589f45f95ccc94cb2a5c500b477eb3323d8cfab0c8458c96f0147a45d27e45a4d11d54d77684f65d48f15fafcc1ba208e71e921b9bd9017c16a5231af7f", 16);

            Console.WriteLine("e =\n" + bi_e.ToString(10));
            Console.WriteLine("\nd =\n" + bi_d.ToString(10));
            Console.WriteLine("\nn =\n" + bi_n.ToString(10) + "\n");

            for (int count = 0; count < rounds; count++)
            {
                int t1 = 0;
                while (t1 == 0)
                    t1 = (int)(rand.NextDouble() * 65);

                bool done = false;
                while (!done)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (i < t1)
                            val[i] = (byte)(rand.NextDouble() * 256);
                        else
                            val[i] = 0;

                        if (val[i] != 0)
                            done = true;
                    }
                }

                while (val[0] == 0)
                    val[0] = (byte)(rand.NextDouble() * 256);

                Console.Write("Round = " + count);

                BigInteger bi_data = new BigInteger(val, t1);
                BigInteger bi_encrypted = bi_data.modPow(bi_e, bi_n);
                BigInteger bi_decrypted = bi_encrypted.modPow(bi_d, bi_n);

                if (bi_decrypted != bi_data)
                {
                    Console.WriteLine("\nError at round " + count);
                    Console.WriteLine(bi_data + "\n");
                    return;
                }
                Console.WriteLine(" <PASSED>.");
            }

        }



        public static void RSATest2(int rounds)
        {
            Random rand = new Random();
            byte[] val = new byte[64];

            byte[] pseudoPrime1 = {
                        0x85, 0x84, 0x64, 0xFD, 0x70, 0x6A,
                        0x9F, 0xF0, 0x94, 0x0C, 0x3E, 0x2C,
                        0x74, 0x34, 0x05, 0xC9, 0x55, 0xB3,
                        0x85, 0x32, 0x98, 0x71, 0xF9, 0x41,
                        0x21, 0x5F, 0x02, 0x9E, 0xEA, 0x56,
                        0x8D, 0x8C, 0x44, 0xCC, 0xEE, 0xEE,
                        0x3D, 0x2C, 0x9D, 0x2C, 0x12, 0x41,
                        0x1E, 0xF1, 0xC5, 0x32, 0xC3, 0xAA,
                        0x31, 0x4A, 0x52, 0xD8, 0xE8, 0xAF,
                        0x42, 0xF4, 0x72, 0xA1, 0x2A, 0x0D,
                        0x97, 0xB1, 0x31, 0xB3,
                };

            byte[] pseudoPrime2 = {
                        0x99, 0x98, 0xCA, 0xB8, 0x5E, 0xD7,
                        0xE5, 0xDC, 0x28, 0x5C, 0x6F, 0x0E,
                        0x15, 0x09, 0x59, 0x6E, 0x84, 0xF3,
                        0x81, 0xCD, 0xDE, 0x42, 0xDC, 0x93,
                        0xC2, 0x7A, 0x62, 0xAC, 0x6C, 0xAF,
                        0xDE, 0x74, 0xE3, 0xCB, 0x60, 0x20,
                        0x38, 0x9C, 0x21, 0xC3, 0xDC, 0xC8,
                        0xA2, 0x4D, 0xC6, 0x2A, 0x35, 0x7F,
                        0xF3, 0xA9, 0xE8, 0x1D, 0x7B, 0x2C,
                        0x78, 0xFA, 0xB8, 0x02, 0x55, 0x80,
                        0x9B, 0xC2, 0xA5, 0xCB,
                };


            BigInteger bi_p = new BigInteger(pseudoPrime1);
            BigInteger bi_q = new BigInteger(pseudoPrime2);
            BigInteger bi_pq = (bi_p - 1) * (bi_q - 1);
            BigInteger bi_n = bi_p * bi_q;

            for (int count = 0; count < rounds; count++)
            {
                BigInteger bi_e = bi_pq.genCoPrime(512, rand);
                BigInteger bi_d = bi_e.modInverse(bi_pq);

                Console.WriteLine("\ne =\n" + bi_e.ToString(10));
                Console.WriteLine("\nd =\n" + bi_d.ToString(10));
                Console.WriteLine("\nn =\n" + bi_n.ToString(10) + "\n");

                int t1 = 0;
                while (t1 == 0)
                    t1 = (int)(rand.NextDouble() * 65);

                bool done = false;
                while (!done)
                {
                    for (int i = 0; i < 64; i++)
                    {
                        if (i < t1)
                            val[i] = (byte)(rand.NextDouble() * 256);
                        else
                            val[i] = 0;

                        if (val[i] != 0)
                            done = true;
                    }
                }

                while (val[0] == 0)
                    val[0] = (byte)(rand.NextDouble() * 256);

                Console.Write("Round = " + count);

                BigInteger bi_data = new BigInteger(val, t1);
                BigInteger bi_encrypted = bi_data.modPow(bi_e, bi_n);
                BigInteger bi_decrypted = bi_encrypted.modPow(bi_d, bi_n);

                // compare
                if (bi_decrypted != bi_data)
                {
                    Console.WriteLine("\nError at round " + count);
                    Console.WriteLine(bi_data + "\n");
                    return;
                }
                Console.WriteLine(" <PASSED>.");
            }

        }

        public static void SqrtTest(int rounds)
        {
            Random rand = new Random();
            for (int count = 0; count < rounds; count++)
            {
                int t1 = 0;
                while (t1 == 0)
                    t1 = (int)(rand.NextDouble() * 1024);

                Console.Write("Round = " + count);

                BigInteger a = new BigInteger();
                a.genRandomBits(t1, rand);

                BigInteger b = a.sqrt();
                BigInteger c = (b + 1) * (b + 1);

                if (c <= a)
                {
                    Console.WriteLine("\nError at round " + count);
                    Console.WriteLine(a + "\n");
                    return;
                }
                Console.WriteLine(" <PASSED>.");
            }
        }





    }
}
