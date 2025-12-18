using PatternsDemo.Patterns;
using System;
using System.Collections.Generic;
using static PatternsDemo.Patterns.CommandPatternDemo;

namespace PatternsDemo
{
    class Program
    {
        static void Main()
        {
            bool finished = false;
            while (!finished)
            {
                Console.WriteLine("Выберите паттерн для демонстрации:\n1.Адаптер;\n2.Команда;\n3.Прототип;\n0.Выход");
                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        AdapterPatternDemo.AdapterMain();
                        finished = true;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        break;

                    case "2":
                        Console.Clear();
                        CommandPatternDemo.CommandMain();
                        finished = true;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        break;

                    case "3":
                        Console.Clear();
                        PrototypePatternDemo.PrototypeMain();
                        finished = true;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        break;

                    case "0":
                        finished = true;
                        break;

                    default:
                        Console.Clear();
                        Console.WriteLine("Некорректный выбор. Нажмите Enter чтобы продолжить...");
                        Console.ReadLine();
                        Console.Clear();
                        break;

                }
            }
        }
    }
}


