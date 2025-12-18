using static PatternsDemo.Patterns.CommandPatternDemo;
using static PatternsDemo.Patterns.AdapterPatternDemo;
using static PatternsDemo.Patterns.PrototypePatternDemo;

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
                        Console.Clear();  //надо сделать более наглядно(продемоснтрировать конкретную работу адаптера)
                        AdapterMain();
                        finished = true;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        break;

                    case "2":
                        Console.Clear(); // добавить коментарии действий
                        CommandMain();
                        finished = true;
                        Console.WriteLine("Нажмите Enter чтобы продолжить...");
                        break;

                    case "3":
                        Console.Clear();
                        PrototypeMain();
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


