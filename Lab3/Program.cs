using System;
using System.Linq;

namespace Lab3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("ZABAWA TABLICAMI (ZAD1):\n\n");
            Task1();
            Console.ReadKey();
            Console.WriteLine("\n\nZABAWA KROTKAMI (ZAD2):\n\n");
            Task2();
            Console.ReadKey();
            Console.WriteLine("\n\nZMIENNA O NAZWIE CLASS (ZAD3):\n\n");
            Task3();
            Console.ReadKey();
            Console.WriteLine("\n\nZABAWA SYSTEM.ARRAY (ZAD4):\n\n");
            Task4();
            Console.ReadKey();
            Console.WriteLine("\n\nTYPY ANONIMOWE SĄ BEZNADZIEJNE (ZAD5):\n\n");
            Task5();
            Console.ReadKey();
        }

        static void Task1()
        {
            Random rng = new Random();

            Console.Write("N: ");
            int n = int.Parse(Console.ReadLine());
            Console.Write("M: ");
            int m = int.Parse(Console.ReadLine());

            // 2 dimensional table
            int[,] matrix1 = new int[n,m];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    matrix1[i, j] = rng.Next(100);
                }
            }
            Console.WriteLine("Matrix 1:");
            PrintMatrix(matrix1);
            // Reverse matrix 1
            // swap action is obsolete ;)
            Action<int, int, int, int, int[,]> swap = (a, b, x, y, mtx) =>
            {
                int temp = mtx[a, b];
                mtx[a, b] = mtx[x, y];
                mtx[x, y] = temp;
            };
            for (int i = 0; i < n/2; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    swap(i, j, matrix1.GetLength(0) - i - 1, j, matrix1);
                    int temp = matrix1[i, j];
                    matrix1[i, j] = matrix1[matrix1.GetLength(0) - i - 1, j];
                    matrix1[matrix1.GetLength(0) - i - 1, j] = temp;
                }
            }
            Console.WriteLine("Matrix 1 reversed:");
            PrintMatrix(matrix1);

            // Table of tables
            int[][] matrix2 = new int[n][];
            for (int i = 0; i < n; i++)
            {
                matrix2[i] = new int[m];
                for (int j = 0; j < m; j++)
                {
                    matrix2[i][j] = rng.Next(100);
                }
            }
            Console.WriteLine("Matrix 2:");
            PrintMatrix(matrix2);
            // Reverse matrix 2
            for (int i = 0; i < n/2; i++)
            {
                int[] temp = matrix2[i];
                matrix2[i] = matrix2[matrix2.GetLength(0) - i - 1];
                matrix2[matrix2.GetLength(0) - i - 1] = temp;
            }
            Console.WriteLine("Matrix 2 reversed:");
            PrintMatrix(matrix2);
        }

        static void PrintMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    Console.Write($"{matrix[i,j]:00} ");
                }
                Console.WriteLine();
            }
        }

        static void PrintMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[0].Length; j++)
                {
                    Console.Write($"{matrix[i][j]:00} ");
                }
                Console.WriteLine();
            }
        }

        static void Task2()
        {
            Console.Write("NAME: ");
            string name = Console.ReadLine();
            Console.Write("SURNAME: ");
            string surname = Console.ReadLine();
            Console.Write("AGE: ");
            int age = int.Parse(Console.ReadLine());
            Console.Write("SALARY: ");
            int salary = int.Parse(Console.ReadLine());

            var tuple = (name, surname, age, salary);

            Helper(tuple);

        }

        static void Helper((string val1, string val2, int val3, int val4) tuple)
        {
            Console.WriteLine("Access through tuple.name");
            Console.WriteLine($"NAME: {tuple.val1}, SURNAME {tuple.val2}, AGE: {tuple.val3}, SALARY: {tuple.val4}\n");

            Console.WriteLine("Access through tuple.ItemX");
            Console.WriteLine($"NAME: {tuple.Item1}, SURNAME {tuple.Item2}, AGE: {tuple.Item3}, SALARY: {tuple.Item4}\n");

            Console.WriteLine("Access through tuple.ToString()");
            Console.WriteLine("TUPLE: " + tuple);
        }

        static void Task3()
        {
            int @class = 17;
            Console.WriteLine($"Variable named {nameof(@class)}: {@class}");
        }

        static void Task4()
        {
            // System.Array methods

            var rng = new Random();
            var arr = Enumerable.Repeat(0, 20).Select((v) => rng.Next(100)).ToArray();

            // FOREACH
            Console.WriteLine("\nForEach method that prints array \"arr\"");
            Array.ForEach(arr, (v) => Console.Write(v + " "));
            Console.WriteLine();

            // COPY, SORT
            Console.WriteLine("\nCopy \"arr\" to \"arr1\", sort \"arr1\", print \"arr1\"");
            int[] arr1 = new int[20];
            Array.Copy(arr, arr1, 20);
            Array.Sort(arr1, (a, b) => a - b);
            Array.ForEach(arr1, (v) => Console.Write(v + " "));
            Console.WriteLine();

            // REVERSE
            Console.WriteLine("\nReverse \"arr1\", print \"arr1\"");
            Array.Reverse(arr1);
            Array.ForEach(arr1, (v) => Console.Write(v + " "));
            Console.WriteLine();

            // BINARY SEARCH
            Console.WriteLine("\nReverse back \"arr1\", binary search in \"arr1\" for first item from \"arr\"");
            Array.Reverse(arr1);
            int index = Array.BinarySearch(arr1, arr[0]);
            Console.WriteLine($"Index of first element from first array in second array: {index}, value: {arr1[index]}\n");
        }

        static void Task5()
        {
            var anonymous = new { name = "EXAMPLE", descr = "EXAMPLE OF ANONYMOUS TYPE" };

            Console.WriteLine("anonymous: " + anonymous);
            Console.WriteLine("anonymous.name: " + anonymous.name);
            Console.WriteLine("anonymous.descr: " + anonymous.descr);
            //Console.WriteLine(anonymous.Item1); // Compiler error

            HelperAnonymous(anonymous);
            HelperAnonymousObject(anonymous);
        }

        
        static void HelperAnonymous(dynamic dyn)
        {
            Console.WriteLine("dyn.name: " + dyn.name); // prints: EXAMPLE
            //Console.WriteLine(list.err); // This line is accepted by compiler but throws exception
        }

        static void HelperAnonymousObject(object obj)
        {
            Console.WriteLine("obj: " + obj); // prints: { name = EXAMPLE, descr = EXAMPLE OF ANONYMOUS TYPE }
            //string s = obj.name; // Compiler error
        }
    }
}
