using System;

namespace Lab5
{
    class Program
    {
        static void Main()
        {
            // TASK 1 TESTS
            Console.WriteLine($"Simplify counter: {MixedNumber.SimplifierCounter}");
            MixedNumber a = new MixedNumber();
            Console.WriteLine($"A: {a}");
            a.Whole = 12;
            // Here number will be simplified from 12+0/6 to 12+0/1 ...
            a.Denominator = 6;
            // ... so this line will add 10 to whole
            a.Numerator = 10;
            Console.WriteLine($"A after setting values: {a}");

            MixedNumber b = new MixedNumber(12, 5);
            MixedNumber c = new MixedNumber(7, 3, 10);
            Console.WriteLine($"B: {b}");
            Console.WriteLine($"C: {c}");
            Console.WriteLine($"B fraction as double: {b.FractionAsDouble}");
            Console.WriteLine($"C fraction as double: {c.FractionAsDouble}");
            Console.WriteLine($"B as double: {b.NumberAsDouble}");
            Console.WriteLine($"C as double: {c.NumberAsDouble}");
            MixedNumber d = b + c;
            Console.WriteLine($"D = B + C: {d}");
            Console.WriteLine($"B + D: {b + d}");
            Console.WriteLine($"Simplify counter after operations: {MixedNumber.SimplifierCounter}");

            // TASK 2 TEST
            Console.WriteLine("ASDFQWER!@#$%^&*".ModifyString());
        }
    }

    // TASK 1
    class MixedNumber
    {
        // DEFINE PROPERTIES
        public int Whole { get; set; }
        
        private int numerator;
        public int Numerator
        {
            get { return numerator; }
            set { numerator = Math.Abs(value); SimplifyFraction(); }
        }

        private int denominator;
        public int Denominator
        {
            get { return denominator; }
            set 
            {
                if (value == 0)
                    throw new DivideByZeroException();
                denominator = Math.Abs(value); 
                SimplifyFraction();
            } 
        }

        public double FractionAsDouble { get { return (double)numerator / denominator; } }
        public double NumberAsDouble { get { return FractionAsDouble + Whole; } }
        public static int SimplifierCounter { get; private set; }

        // CONSTRUCTORS
        public MixedNumber(int whole, int numerator, int denominator)
        {
            Whole = whole;
            this.numerator = numerator;
            this.denominator = denominator;
            SimplifyFraction();
        }

        public MixedNumber(int numerator, int denominator) 
            : this(0, numerator, denominator) {}

        public MixedNumber() : this(0, 1) {}

        // OPERATORS
        public static MixedNumber operator +(MixedNumber a, MixedNumber b)
        {
            int lcm = LeastCommonMultiple(a.Denominator, b.Denominator);
            int num1 = a.numerator * (lcm / a.Denominator);
            int num2 = b.numerator * (lcm / b.Denominator);
            MixedNumber res = new MixedNumber(a.Whole + b.Whole, num1 + num2, lcm);
            res.SimplifyFraction();
            return res;
        }

        // HELPER METHODS
        private static int GreatestCommonDivisor(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a | b;
        }

        private static int LeastCommonMultiple(int a, int b)
        {
            int baseA = a, baseB = b;
            while (a!=b)
            {
                if (a > b)
                {
                    b += baseB;
                }
                else
                {
                    a += baseA;
                }
            }
            return a;
        }

        private void SimplifyFraction()
        {
            int gcd = GreatestCommonDivisor(numerator, denominator);
            if (gcd > 1 || numerator >= denominator)
                SimplifierCounter++;
            Whole += numerator / denominator;
            numerator %= denominator;
            numerator /= gcd;
            denominator /= gcd;
        }

        public override string ToString()
        {
            return $"{Whole}+{numerator}/{denominator}";
        }
    }

    static class StringExtensions
    {
        // TASK 2
        public static string ModifyString(this string source)
        {
            char[] result = source.ToCharArray();

            for (int i = 0; i < result.Length; i++)
            {
                if (i % 2 == 0)
                {
                    if (char.IsLower(result[i]))
                    {
                        result[i] = char.ToUpper(result[i]);
                    }
                    else if (!char.IsUpper(result[i]))
                    {
                        result[i] = '.';
                    }
                }
                else
                {
                    if (char.IsUpper(result[i]))
                    {
                        result[i] = char.ToLower(result[i]);
                    }
                    else if (!char.IsLower(result[i]))
                    {
                        result[i] = '.';
                    }
                }
            }

            return new string(result);
        }
    }
}
