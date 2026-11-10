using System;
using System.Globalization;

namespace ClassTask4
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


        public static void RAST(in double x1, in double y1, in double x2, in double y2, out double r)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            r = Math.Sqrt(dx * dx + dy * dy);
        }


        public static double PERIMETR(double x1 = 0, double y1 = 0,double x2 = 2, double y2 = 0, double x3 = 1, double y3 = 2)
        {
            RAST(in x1, in y1, in x2, in y2, out double a);
            RAST(in x2, in y2, in x3, in y3, out double b);
            RAST(in x3, in y3, in x1, in y1, out double c);
            return a + b + c;
        }

        public static double TriangleArea(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            RAST(in x1, in y1, in x2, in y2, out double a);
            RAST(in x2, in y2, in x3, in y3, out double b);
            RAST(in x3, in y3, in x1, in y1, out double c);

            double p = (a + b + c) / 2.0;
            double s2 = p * (p - a) * (p - b) * (p - c);
            if (s2 <= 0) return 0;
            return Math.Sqrt(s2);
        }

        public static void Run()
        {
            SetColor("Задание 4*. Площадь треугольника по 3 точкам\nRAST - процедура расстояния (in/out)\nPERIMETR - метод с необязательными параметрами", ConsoleColor.DarkGray);

            Console.WriteLine("1 - координаты по умолчанию: (0,0), (2,0), (1,2)");
            Console.WriteLine("2 - ввести координаты вручную");
            Console.Write("> ");

            int mode;
            while (!int.TryParse((Console.ReadLine() ?? "").Trim(), out mode) || (mode != 1 && mode != 2))
            {
                SetColor("ERROR: Введите 1 или 2", ConsoleColor.Red);
                Console.Write("> ");
            }

            double x1 = 0, y1 = 0, x2 = 2, y2 = 0, x3 = 1, y3 = 2;

            if (mode == 2)
            {
                x1 = ReadDouble("Введите x1:"); y1 = ReadDouble("Введите y1:");
                x2 = ReadDouble("Введите x2:"); y2 = ReadDouble("Введите y2:");
                x3 = ReadDouble("Введите x3:"); y3 = ReadDouble("Введите y3:");
            }

            double per = (mode == 1) ? PERIMETR() : PERIMETR(x1, y1, x2, y2, x3, y3);
            double area = TriangleArea(x1, y1, x2, y2, x3, y3);

            if (area <= 0)
            {
                SetColor("ERROR: Треугольник вырожденный (площадь = 0).", ConsoleColor.Red);
                return;
            }

            SetColor($"RESULT: Периметр = {per:F6}", ConsoleColor.Green);
            SetColor($"RESULT: Площадь = {area:F6}", ConsoleColor.Green);
        }
    }
}