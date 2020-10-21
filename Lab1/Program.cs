using System;

namespace Lab1
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Incorrect number of arguments");
                return;
            }

            if (int.TryParse(args[0], out int triangleSize))
            {
                for (int i = 1; i <= triangleSize; i++)
                {
                    int spaces = triangleSize - i;
                    string line = string.Empty;
                    for (int j = 0; j < spaces; j++)
                    {
                        line += " ";
                    }
                    for (int j = 0; j < i; j++)
                    {
                        line += "X";
                    }
                    Console.WriteLine(line);
                }
            }
            else
            {
                Console.WriteLine("Provided argument is not a number");
            }
        }
    }
}
