using System;
using System.Collections.Generic;

namespace LanguageTask
{
    class Program
    {
        public static void Task1()
        {
            Automatic automatic = new Automatic();
            Console.WriteLine("Введите имя переменной");
            string input = Console.ReadLine();

            Token token = automatic.IsInt(input, 0);

            Console.WriteLine("Является ли эта строчка грамматически корректным int значением?");
            Console.WriteLine(token.isGrammar);
            Console.WriteLine("Последний корректный символ");
            Console.WriteLine(token.lastNumber);
            Console.WriteLine();
        }

        public static void Task2()
        {
            Automatic automatic = new Automatic();
            Console.WriteLine("Введите входную строку");
            string input = Console.ReadLine();
            Console.WriteLine("Название лексемы - лексема");
            List<KeyValuePair<string, string>> result = automatic.IsCorrect(input);

            foreach (KeyValuePair<string, string> r in result)
            {
                Console.WriteLine(r.Key + " - " + r.Value);
            }
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            Task1();
            Task2();
        }
    }
}
