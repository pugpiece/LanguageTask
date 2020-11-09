using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LanguageTask
{
    class Automatic //Класс хранящий в себе все автоматы
    {
        private Tree intTree; //автомат для слова int

        private Tree valueTree; //автомат для целочисленных значений

        private Tree nameTree; //автомат для имен переменных

        private Tree symbolTree; // автомат для символов, например ";", "-", "+", "=" и так далее

        public Automatic() //при создании экземпляра класса все автоматы считываются с JSON файлов
        {
            string jsonString = File.ReadAllText(@"..\\..\\..\\int.json");
            intTree = JsonSerializer.Deserialize<Tree>(jsonString);

            jsonString = File.ReadAllText(@"..\\..\\..\\value.json");
            valueTree = JsonSerializer.Deserialize<Tree>(jsonString);

            jsonString = File.ReadAllText(@"..\\..\\..\\name.json");
            nameTree = JsonSerializer.Deserialize<Tree>(jsonString);

            jsonString = File.ReadAllText(@"..\\..\\..\\symbol.json");
            symbolTree = JsonSerializer.Deserialize<Tree>(jsonString);
        }

        // функции по проверке строчки через автомат
        public Token IsInt(string input, int start)
        {
            return intTree.Try(input, start);
        }

        public Token IsValue(string input, int start)
        {
            return valueTree.Try(input, start);
        }

        public Token IsName(string input, int start)
        {
            return nameTree.Try(input, start);
        }

        //функция по проверке строчки на все автоматы сразу, возвращает набор пар <название лексемы, сама лексема>
        public List<KeyValuePair<string, string>> IsCorrect(string input)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            List<Tree> trees = new List<Tree>() { intTree, valueTree, nameTree, symbolTree };
            int i = 0;

            while (i <= input.Length)
            {
                int priority = -1;
                int maxLength = -1;
                Tree rightTree = null;

                foreach (Tree tree in trees)
                {
                    if (tree.Try(input, i).isGrammar == true && tree.priority >= priority && tree.Try(input, i).lastNumber >= maxLength)
                    {
                        priority = tree.priority;
                        maxLength = tree.Try(input, i).lastNumber;
                        rightTree = tree;
                    }
                }

                if (rightTree == null)
                {
                    i++;
                }
                else
                {
                    Token temp = rightTree.Try(input, i);
                    string name = input.Substring(i, temp.lastNumber);
                    result.Add(new KeyValuePair<string, string>(rightTree.name, name));
                    i += temp.lastNumber;
                }
            }

            return result;
        }
    }
}
