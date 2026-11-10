using System;
using System.Collections.Generic;

namespace ClassTask3
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

        private static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                Console.Write("> ");

                if (!int.TryParse((Console.ReadLine() ?? "").Trim(), out int x))
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

        private static int DigitSum(int x)
        {
            x = Math.Abs(x);
            int s = 0;
            while (x > 0)
            {
                s += x % 10;
                x /= 10;
            }
            return s;
        }

        public static void BuildNumbers(ref int a, ref int b, in int sumDigits, out int[] result)
        {
            if (a > b)
            {
                int t = a; a = b; b = t;
            }

            var list = new List<int>();

            for (int x = b; x >= a; x--) 
            {
                if (DigitSum(x) == sumDigits)
                    list.Add(x);
            }

            result = list.ToArray();
        }

        public static void Run()
        {
            SetColor("Задание 3. Числа из [a,b] с заданной суммой цифр (по убыванию)\n", ConsoleColor.DarkGray);

            int a = ReadInt("Введите a:", -1000000, 1000000);
            int b = ReadInt("Введите b:", -1000000, 1000000);
            int s = ReadInt("Введите сумму цифр:", 0, 200);

            BuildNumbers(ref a, ref b, in s, out int[] arr);

            SetColor($"Диапазон: [{a}; {b}]", ConsoleColor.DarkGray);

            if (arr.Length == 0)
            {
                SetColor("\nRESULT: NO", ConsoleColor.Green);
                return;
            }

            SetColor("\nRESULT:", ConsoleColor.Green);
            for (int i = 0; i < arr.Length; i++)
                Console.Write(arr[i] + (i + 1 < arr.Length ? " " : ""));
            Console.WriteLine();
        }
    }
}