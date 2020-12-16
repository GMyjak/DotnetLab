using System;
using System.Linq;


namespace Lab2
{
    public class Program
    {
        private static bool prompt = true;
        private static void Main(string[] args)
        {
            if (prompt)
            {
                Console.WriteLine("\n\nChoose program:");
                Console.WriteLine("1 Quadratic equation");
                Console.WriteLine("2 Number conversion");
                Console.WriteLine("3 Second biggest number\n\n");
            }
            else
            {
                prompt = true;
            }

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                switch (option)
                {
                    case 1:
                        Task1();
                        break;
                    case 2:
                        Task2();
                        break;
                    case 3:
                        Task3();
                        break;
                    default:
                        Console.WriteLine("Try again");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Try again");
                prompt = false;
            }
            Main(null);
        }

        private static void Task1()
        {
            Console.WriteLine("ax² + bx + c = 0");

            Console.Write("a = ");
            if (!double.TryParse(Console.ReadLine(), out double a))
            {
                Console.WriteLine("Incorrect format");
                return;
            }

            Console.Write("b = ");
            if (!double.TryParse(Console.ReadLine(), out double b))
            {
                Console.WriteLine("Incorrect format");
                return;
            }

            Console.Write("c = ");
            if (!double.TryParse(Console.ReadLine(), out double c))
            {
                Console.WriteLine("Incorrect format");
                return;
            }
            //Console.WriteLine(SolveEquation(a,b,c,out _));
        }

        public static string SolveEquation(double a, double b, double c, out int numberOfSolutions)
        {
            if (a != 0)
            {
                // Quadratic equation
                double delta = b * b - 4 * a * c;

                if (delta < 0)
                {
                    Console.WriteLine("No solutions found");
                    numberOfSolutions = 0;
                    return "No solutions found";
                }
                else if (delta == 0)
                {
                    double solution = -b / 2 / a;
                    Console.WriteLine("Solution: x = {0:0.#####}", solution);
                    numberOfSolutions = 1;
                    return string.Format("x = {0:0.#####}", solution);
                }
                else
                {
                    double x1 = -b, x2 = -b;
                    x1 += Math.Sqrt(delta);
                    x2 -= Math.Sqrt(delta);
                    x1 /= (2 * a);
                    x2 /= (2 * a);
                    Console.WriteLine($"Solution: x1 = {x1:0.#####} x2 = {x2:0.#####}");
                    numberOfSolutions = 2;
                    return $"x1 = {x1:0.#####} x2 = {x2:0.#####}";
                }
            }
            else if (b != 0)
            {
                // Linear equation
                double solution = -c / b;
                Console.WriteLine("Solution: x = {0:0.#####}", solution);
                numberOfSolutions = 1;
                return $"x = {solution:0.#####}";
            }
            else
            {
                // Horizontal line
                Console.WriteLine(c == 0 ? "Solution: x = {R}" : "No solutions found");
                numberOfSolutions = c == 0 ? -1 : 0;
                return c == 0 ? "x = {R}" : "No solutions found";
            }
        }

        private static void Task2()
        {
            Console.Write("Type in first number: ");
            if (!int.TryParse(Console.ReadLine(), out int a))
            {
                Console.WriteLine("Incorrect format");
                return;
            }

            Console.Write("Type in second number: ");
            if (!int.TryParse(Console.ReadLine(), out int b))
            {
                Console.WriteLine("Incorrect format");
                return;
            }

            Console.WriteLine($"\nA: {a}");
            Console.WriteLine($"B: {b}\n");

            Console.WriteLine($"A binary: {Convert.ToString(a, 2)}");
            Console.WriteLine($"B binary: {Convert.ToString(b, 2)}\n");

            Console.WriteLine($"A negated: {~a:X}");
            Console.WriteLine($"B negated: {~b:X}\n");

            Console.WriteLine($"A AND B: {a & b:X}\n");
            Console.WriteLine($"A OR B: {a | b:X}\n");
        }

        private static void Task3()
        {
            Console.Write("How many integers: ");
            if (!int.TryParse(Console.ReadLine(), out int howManyIntegers))
            {
                Console.WriteLine("Incorrect format");
                return;
            }

            if (howManyIntegers < 1)
            {
                Console.WriteLine("Number of integers must be greater than 0");
                return;
            }


            int? builder = null;
            int numberCounter = 0;
            int? biggestValue = null;
            int? secondBiggestValue = null;

            bool positiveSign = true;
            
            while (numberCounter < howManyIntegers)
            {
                char curr = (char)Console.Read();

                if (char.IsWhiteSpace(curr) && builder != null) 
                {
                    // Analyze builder
                    builder *= positiveSign ? 1 : -1;

                    if (biggestValue == null)
                    {
                        biggestValue = builder;
                    }
                    else if (builder > biggestValue)
                    {
                        secondBiggestValue = biggestValue;
                        biggestValue = builder;
                    }
                    else if (builder < biggestValue && (secondBiggestValue == null || builder > secondBiggestValue))
                    {
                        secondBiggestValue = builder;
                    }

                    builder = null;
                    positiveSign = true;
                    numberCounter++;
                }
                else if (curr == '-')
                {
                    positiveSign = false;
                }
                else if (curr >= '0' && curr <= '9')
                {
                    if (builder == null)
                    {
                        builder = 0;
                    }
                    int digit = curr - 48;
                    builder *= 10;
                    builder += digit;
                }
            }

            Console.WriteLine(secondBiggestValue != null ? $"Second biggest value: {secondBiggestValue}" : "No solutions");
        }
    }
}
