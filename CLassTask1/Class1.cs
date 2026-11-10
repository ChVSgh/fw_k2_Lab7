using System;
using System.Globalization;

namespace ClassTask1
{
    public static class Class1
    {
        private static void SetColor(string text, ConsoleColor color)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = old;
        }

        private static void WriteColor(string text, ConsoleColor color)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = old;
        }

        private static int ReadInt(string prompt, int min, int max)
        {
            int x;
            while (true)
            {
                Console.WriteLine(prompt);
                Console.Write("> ");

                if (!int.TryParse((Console.ReadLine() ?? "").Trim(), out x))
                {
                    SetColor("ERROR: Введите целое число", ConsoleColor.Red);
                    continue;
                }

                if (x < min || x > max)
                {
                    SetColor($"ERROR: Допустимо [{min}; {max}]", ConsoleColor.Red);
                    continue;
                }

                return x;
            }
        }

        private static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                Console.Write("> ");

                string s = ((Console.ReadLine() ?? "").Trim()).Replace(',', '.');

                if (!double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out double x)
                    || double.IsNaN(x) || double.IsInfinity(x))
                {
                    SetColor("ERROR: Введите действительное число", ConsoleColor.Red);
                    continue;
                }

                return x;
            }
        }


        private static bool IsRowSortedDesc(int[,] a, int i)
        {
            int n = a.GetLength(1);

            for (int j = 0; j < n - 1; j++)
            {
                if (a[i, j] < a[i, j + 1])
                    return false;
            }

            return true;
        }

        public static int CountRowsSortedDesc(int[,] a)
        {
            int m = a.GetLength(0);
            int cnt = 0;

            for (int i = 0; i < m; i++)
            {
                if (IsRowSortedDesc(a, i))
                    cnt++;
            }

            return cnt;
        }

        private static void PrintMatrix(int[,] a)
        {
            int m = a.GetLength(0);
            int n = a.GetLength(1);

            for (int i = 0; i < m; i++)
            {
                bool ok = IsRowSortedDesc(a, i);

                for (int j = 0; j < n; j++)
                {
                    string cell = $"{a[i, j],6}";

                    if (ok)
                        WriteColor(cell, ConsoleColor.Green);
                    else
                        Console.Write(cell);
                }

                Console.WriteLine();
            }
        }

        public static void RunTask1()
        {
            SetColor("Задание 1.1. Кол-во строк 2D массива, где элементы по убыванию\n", ConsoleColor.DarkGray);

            int m = ReadInt("Введите кол-во строк m:", 1, 30);
            int n = ReadInt("Введите кол-во столбцов n:", 1, 30);

            int minV = ReadInt("Введите min значение (целое):", -1000, 1000);
            int maxV = ReadInt("Введите max значение (целое):", minV, 1000);

            var rnd = new Random();
            int[,] a = new int[m, n];

            for (int i = 0; i < m; i++)
                for (int j = 0; j < n; j++)
                    a[i, j] = rnd.Next(minV, maxV + 1);

            Console.WriteLine("\nМатрица:");
            PrintMatrix(a);

            int ans = CountRowsSortedDesc(a);

            SetColor($"\nRESULT: Кол-во строк = {ans}", ConsoleColor.Green);
        }


        public static double CalcA(double x, double y)
        {
            double numerator = Math.Sqrt(Math.Abs(x - 1)) - Math.Sqrt(Math.Abs(y));
            double denominator = 1 + Math.Pow(x, 2) / 2 + Math.Pow(y, 2) / 4;

            return numerator / denominator;
        }

        public static double CalcB(double x, double z)
        {
            return Math.Pow(x, 3) * (Math.Pow(Math.Atan(z), 3) + Math.E);
        }

        public static void RunTask2()
        {
            SetColor("Задание 1.2. Вычисление математических выражений (вариант №6)", ConsoleColor.DarkGray);

            SetColor($"Числитель a = Math.Sqrt(Math.Abs(x - 1)) - Math.Sqrt(Math.Abs(y))", ConsoleColor.DarkGray);
            SetColor($"Знаменатель a = 1 + Math.Pow(x, 2) / 2 + Math.Pow(y, 2) / 4", ConsoleColor.DarkGray);
            SetColor($"b = Math.Pow(x, 3)*(arctg^3(z)+e)\n", ConsoleColor.DarkGray);
            double x = ReadDouble("Введите x:");
            double y = ReadDouble("Введите y:");
            double z = ReadDouble("Введите z:");

            double a = CalcA(x, y);
            double b = CalcB(x, z);

            SetColor($"\nx = {x}, y = {y}, z = {z}", ConsoleColor.DarkGray);

            SetColor($"Числитель a = {Math.Sqrt(Math.Abs(x - 1)) - Math.Sqrt(Math.Abs(y)):F6}", ConsoleColor.DarkGray);
            SetColor($"Знаменатель a = {1 + Math.Pow(x, 2) / 2 + Math.Pow(y, 2) / 4:F6}", ConsoleColor.DarkGray);

            SetColor($"arctg(z) = {Math.Atan(z):F6}", ConsoleColor.DarkGray);
            SetColor($"arctg^3(z) = {Math.Pow(Math.Atan(z), 3):F6}", ConsoleColor.DarkGray);
            SetColor($"e = {Math.E:F6}\n", ConsoleColor.DarkGray);

            SetColor($"RESULT: a = {a:F6}", ConsoleColor.Green);
            SetColor($"RESULT: b = {b:F6}", ConsoleColor.Green);
        }


        public static int CountNegative(int[] a)
        {
            int cnt = 0;

            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] < 0)
                    cnt++;
            }

            return cnt;
        }

        private static void PrintArray(int[] a)
        {
            for (int i = 0; i < a.Length; i++)
            {
                string cell = $"{a[i],6}";

                if (a[i] < 0)
                    WriteColor(cell, ConsoleColor.Green);
                else
                    Console.Write(cell);
            }

            Console.WriteLine();
        }

        public static void RunTask3()
        {
            SetColor("Задание 1.3. Кол-во отрицательных элементов 1D массива\n", ConsoleColor.DarkGray);

            int n = ReadInt("Введите размер массива n:", 1, 100);

            int minV = ReadInt("Введите min значение (целое):", -1000, 1000);
            int maxV = ReadInt("Введите max значение (целое):", minV, 1000);

            var rnd = new Random();
            int[] a = new int[n];

            for (int i = 0; i < n; i++)
                a[i] = rnd.Next(minV, maxV + 1);

            Console.WriteLine("\nМассив:");
            PrintArray(a);

            int ans = CountNegative(a);

            SetColor($"\nRESULT: Кол-во отрицательных элементов = {ans}", ConsoleColor.Green);
        }


        public static void Run()
        {
            RunTask1();
        }
    }
}