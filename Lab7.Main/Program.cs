using System;
using ClassTask1;
using ClassTask2;
using ClassTask3;
using ClassTask4;

namespace Lab7.Main
{
    internal class Program
    {
        private static void SetColor(string text, ConsoleColor color)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = old;
        }

        private static void Pause()
        {
            Console.WriteLine("\nНажмите Enter...");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            while (true)
            {
                Console.ResetColor();
                Console.Clear();

                SetColor("Лабораторная работа №7, вариант №6\nЧумачек Владислав Сергеевич, бИЦ-252\n", ConsoleColor.DarkGray);

                Console.WriteLine("|Меню:");
                Console.WriteLine("|1. Задание 1.1 (2D массив: строки по убыванию)");
                Console.WriteLine("|2. Задание 1.2 (математические формулы)");
                Console.WriteLine("|3. Задание 1.3 (1D массив: кол-во отрицательных)");
                Console.WriteLine("|4. Задание 2 (u, v, k + ref/out/in)");
                Console.WriteLine("|5. Задание 3 (числа с заданной суммой цифр)");
                Console.WriteLine("|6. Задание 4* (треугольник)");
                Console.WriteLine("|0. Выход");
                Console.Write("> ");

                int num;

                while (!int.TryParse((Console.ReadLine() ?? "").Trim(), out num) || num < 0 || num > 6)
                {
                    SetColor("ERROR: Введите пункт меню от 0 до 6", ConsoleColor.Red);
                    Console.Write("> ");
                }

                Console.WriteLine();

                switch (num)
                {
                    case 1:
                        ClassTask1.Class1.RunTask1();
                        Pause();
                        break;

                    case 2:
                        ClassTask1.Class1.RunTask2();
                        Pause();
                        break;

                    case 3:
                        ClassTask1.Class1.RunTask3();
                        Pause();
                        break;

                    case 4:
                        ClassTask2.Class1.Run();
                        Pause();
                        break;

                    case 5:
                        ClassTask3.Class1.Run();
                        Pause();
                        break;

                    case 6:
                        ClassTask4.Class1.Run();
                        Pause();
                        break;

                    case 0:
                        return;
                }
            }
        }
    }
}