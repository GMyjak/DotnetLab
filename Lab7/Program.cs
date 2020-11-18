using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace Lab7
{
    class Program
    {
        private static readonly string pathBig = @"../../../big.txt";
        private static readonly string pathCc = @"../../../ChristmasCarol.txt";
        private static readonly string pathIliad = @"../../../Iliad.txt";
        // Path to file that user has no permissions for
        private static readonly string pathForbidden = @"../../../forbidden.txt";
        // Ordinary file for small tests
        private static readonly string pathOrdinary = @"../../../ordinary.txt";

        static void Main()
        {
            // Default is 9000, which is not enough to display whole Dict
            Console.BufferHeight = 30000;
            MainMenu();
        }

        static void MainMenu()
        {
            // Display main menu
            Console.WriteLine("1. Type in custom file path: ");
            Console.WriteLine("2. big.txt");
            Console.WriteLine("3. ChristmasCarol.txt");
            Console.WriteLine("4. Iliad.txt");
            Console.WriteLine("5. Provoke UnauthorizedAccessException");
            Console.WriteLine("6. Provoke IOException exception");

            // Process chosen option
            var input = Console.ReadLine();
            Dictionary<string, int> result = null;
            switch (input)
            {
                case "1":
                    Console.Write("File path: ");
                    result = ProcessFile(Console.ReadLine());
                    break;
                case "2":
                    result = ProcessFile(pathBig);
                    break;
                case "3":
                    result = ProcessFile(pathCc);
                    break;
                case "4":
                    result = ProcessFile(pathIliad);
                    break;
                case "5":
                    result = ProcessFile(pathForbidden);
                    break;
                case "6":
                    var blocker = GetMaliciousThread();
                    blocker.Start();
                    blocker.IsBackground = true;
                    result = ProcessFile(pathOrdinary);
                    break;
                default:
                    Console.WriteLine("Incorrect option chosen");
                    return;
            }

            // Display results in console
            if (result != null)
            {
                // This is how to display whole dict more effectively
                //StringBuilder sb = new StringBuilder();
                //foreach (var kvPair in result)
                //{
                //    sb.Append($"{kvPair.Key} => {kvPair.Value}\n");
                //}
                //Console.WriteLine(sb);
                var kvPairList = result.ToList();
                int numOfItemsToDisplay = Math.Min(10, result.Count);
                for (int i = 0; i < numOfItemsToDisplay; i++)
                {
                    Console.WriteLine($"{kvPairList[i].Key} => {kvPairList[i].Value}");
                }
            }
        }

        static Dictionary<string, int> ProcessFile(string filePath)
        {
            StreamReader reader;

            // Without exception catching
            //reader = new StreamReader(filePath);

            // With exception catching
            reader = OpenStreamExceptionCatching(filePath);
            if (reader == null)
            {
                return null;
            }

            var result = new Dictionary<string, int>();

            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                Match match = Regex.Match(line, "([a-zA-Z])+");
                while (match.Success)
                {
                    string lineLower = match.Value.ToLower();

                    if (result.ContainsKey(lineLower))
                        result[lineLower]++;
                    else
                        result.Add(lineLower, 1);

                    match = match.NextMatch();
                }
            }
            reader.Close();

            var dictList = result.ToList();
            dictList.Sort((kv1, kv2) => kv2.Value - kv1.Value);
            result = new Dictionary<string, int>(dictList);

            return result;
        }

        static StreamReader OpenStreamExceptionCatching(string filePath)
        {
            if (!File.Exists(filePath))
            {
                // Now program will never throw FileNotFoundException and DirectoryNotFoundException
                Console.WriteLine("Cannot find specified file");
                return null;
            }

            try
            {
                var result = new StreamReader(filePath);
                return result;
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("Cannot find specified file");
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (DirectoryNotFoundException ex)
            {
                Console.WriteLine("Cannot find specified directory");
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (UnauthorizedAccessException ex)
            {
                Console.WriteLine("Cannot access specified file");
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (IOException ex)
            {
                Console.WriteLine("Cannot open specified file");
                Console.WriteLine(ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Other exception occurred");
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        static Thread GetMaliciousThread()
        {
            return new Thread(() =>
            {
                try
                {
                    StreamWriter writer = File.AppendText(pathOrdinary);
                    while (true) { }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            });
        }
    }
}
