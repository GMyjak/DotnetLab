//#define VERBOSE

using System;
using System.Text;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            // Change values in if statements to true to perform tests

            if (true)
            {
                var res = GetFromConsoleXY("First num", "Second num");
                Console.WriteLine($"N1: {res.Item1}, N2: {res.Item2}");

                GetFromConsoleXY("Num 1", "Num 2", out int x, out int y);
                Console.WriteLine($"N1: {x}, N2: {y}");
            }

            if (false)
            {
                // Standard call
                DrawCard("Dzień dobry", "Wizytówka", 'O', 1, 25);
                Console.WriteLine();

                // Only obligatory params
                DrawCard("Dzień dobry");
                Console.WriteLine();

                // First three params
                DrawCard("Dzień dobry", "Wizytóweczka", 'H');
                Console.WriteLine();

                // Last two params swapped place
                DrawCard("Dzień dobry", "Wizytówka", frameWidth: 1, frame: 'O');
                Console.WriteLine();

                // All non obligatory params in random order
                DrawCard("Dzień dobry", minCardWidth: 30, frame: 'X', secondLine: "String", frameWidth: 1);
                Console.WriteLine();
            }

            if (false)
            {
                CountMyTypes(out int ints, out int reals, out int strings, out int others, 2, 4, 25, 2.0, -2.0, "XD",
                    "KOT", "BATERIA");
                Console.WriteLine("Even integers: " + ints);
                Console.WriteLine("Positive real numbers: " + reals);
                Console.WriteLine("5 or more length strings: " + strings);
                Console.WriteLine("Other values: " + others);
            }
        }

        // TASK 1a
        static (int, int) GetFromConsoleXY(string com1, string com2)
        {
            Console.WriteLine(com1);
            int x = int.Parse(Console.ReadLine());
            Console.WriteLine(com2);
            int y = int.Parse(Console.ReadLine());
            #if VERBOSE
            Console.WriteLine("Returning values in tuple");
            #endif
            return (x, y);
        }

        // TASK 1b
        static void GetFromConsoleXY(string com1, string com2, out int x, out int y)
        {
            Console.WriteLine(com1);
            x = int.Parse(Console.ReadLine());
            #if VERBOSE
            Console.WriteLine("Saved first value in out x");
            #endif
            Console.WriteLine(com2);
            y = int.Parse(Console.ReadLine());
            #if VERBOSE
            Console.WriteLine("Saved second value in out y");
            #endif
        }

        // TASK 2
        static void DrawCard(string firstLine, string secondLine = "Nowak", char frame = 'X', int frameWidth = 2, int minCardWidth = 20)
        {
            // Calculate cardWidth
            int cardWidth = Math.Max(minCardWidth, frameWidth * 2 + Math.Max(firstLine.Length, secondLine.Length) + 2);

            // Generate upper bounds
            var bounds = DrawBounds(frameWidth, cardWidth, frame).ToString();

            // Generate first line
            var line1 = DrawCardLine(firstLine, cardWidth, frameWidth, frame).ToString();

            // Generate second line
            var line2 = DrawCardLine(secondLine, cardWidth, frameWidth, frame).ToString();

            #if VERBOSE
            Console.WriteLine("Drawing card:");
            #endif
            Console.Write(bounds);
            Console.Write(line1);
            Console.Write(line2);
            Console.Write(bounds);
        }

        // HELPER FOR TASK 2
        static StringBuilder DrawBounds(int width, int cardWidth, char frame)
        {
            #if VERBOSE
            Console.WriteLine("Generating card bounds");
            #endif
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < cardWidth; j++)
                {
                    result.Append(frame);
                }
                result.Append("\n");
            }
            return result;
        }

        // HELPER FOR TASK 2
        static StringBuilder DrawCardLine(string message, int cardWidth, int frameWidth, char frame)
        {
            #if VERBOSE
            Console.WriteLine("Generating card line");
            #endif
            int firstStrSpacing = cardWidth - frameWidth - frameWidth - message.Length;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < frameWidth; i++)
            {
                result.Append(frame);
            }
            for (int i = 0; i < firstStrSpacing / 2; i++)
            {
                result.Append(' ');
            }
            result.Append(message);
            firstStrSpacing = firstStrSpacing % 2 == 0 ? firstStrSpacing / 2 : firstStrSpacing / 2 + 1;
            for (int i = 0; i < firstStrSpacing; i++)
            {
                result.Append(' ');
            }
            for (int i = 0; i < frameWidth; i++)
            {
                result.Append(frame);
            }
            result.Append("\n");
            return result;
        }

        //TASK 3
        static void CountMyTypes(out int ints, out int reals, out int strings, out int others, params object[] arguments)
        {
            ints = 0;
            reals = 0;
            strings = 0;
            others = 0;

            foreach (object obj in arguments)
            {
                switch (obj)
                {
                    case int integer when (integer % 2 == 0):
                        #if VERBOSE
                        Console.WriteLine("CountMyTypes: integer detected");
                        #endif
                        ints++;
                        break;
                    case double real when (real > 0):
                        #if VERBOSE
                        Console.WriteLine("CountMyTypes: double detected");
                        #endif
                        reals++;
                        break;
                    case string str when (str.Length >= 5):
                        #if VERBOSE
                        Console.WriteLine("CountMyTypes: string detected");
                        #endif
                        strings++;
                        break;
                    default:
                        others++;
                        break;
                }
            }
        }

    }
}
