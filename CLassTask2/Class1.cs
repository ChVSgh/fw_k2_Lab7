using System;
using System.Globalization;

namespace ClassTask2
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

        private static double ReadDouble(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                Console.Write("> ");

                string s = ((Console.ReadLine() ?? "").Trim()).Replace(',', '.');
                if (!double.TryParse(s, NumberStyles.Any, CultureInfo.InvariantCulture, out double x))
                {
                    SetColor("ERROR: Введите число", ConsoleColor.Red);
                    continue;
                }

                return x;
            }
        }

        public static void CalcU(in double a, in double b, out double u)
        {
            u = Math.Max(a, b - a);
        }

        public static void CalcV(in double a, in double b, out double v)
        {
            v = Math.Min(a * b, a + b);
        }

        public static void CalcK(in double u, in double v, ref double k)
        {
            k = Math.Min(u + v * 2.0, 3.14);
        }

        public static void Run()
        {
            SetColor(
                "Задание 2. k = min(u + v*2, 3.14), u = max(a, b-a), v = min(ab, a+b)\n",
                ConsoleColor.DarkGray
            );

            double a = ReadDouble("Введите a:");
            double b = ReadDouble("Введите b:");

            CalcU(in a, in b, out double u);
            CalcV(in a, in b, out double v);

            double k = 0;
            CalcK(in u, in v, ref k);

            SetColor($"a = {a}, b = {b}", ConsoleColor.DarkGray);
            SetColor($"b - a = {b - a}", ConsoleColor.DarkGray);
            SetColor($"a*b = {a * b}", ConsoleColor.DarkGray);
            SetColor($"a + b = {a + b}\n", ConsoleColor.DarkGray);

            SetColor($"u = max(a, b-a) = {u}", ConsoleColor.DarkGray);
            SetColor($"v = min(a*b, a+b) = {v}", ConsoleColor.DarkGray);
            SetColor($"u + 2*v = {u + 2 * v}", ConsoleColor.DarkGray);
            SetColor($"k = min(u + 2*v, 3.14) = {k}\n", ConsoleColor.DarkGray);

            SetColor($"RESULT: u = {u}", ConsoleColor.Green);
            SetColor($"RESULT: v = {v}", ConsoleColor.Green);
            SetColor($"RESULT: k = {k}", ConsoleColor.Green);
        }
    }
}