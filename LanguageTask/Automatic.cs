using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LanguageTask
{
    public class Automatic //Класс хранящий в себе все автоматы
    {
        private List<Tree> allTrees = new List<Tree>(); //список всех автоматов

        public Automatic() //при создании экземпляра класса все автоматы считываются с JSON файлов
        {
            string[] files = Directory.GetFiles(@"..\\..\\..\\..\\Trees");
            foreach (string file in files)
            {
                allTrees.Add(JsonSerializer.Deserialize<Tree>(File.ReadAllText(@file)));
            }
        }

        // функции по проверке строчки через автомат
        public Token TryTree(string input, int start, string treeName)
        {
            Tree tree = allTrees.Find(x => x.name == treeName);
            return tree.Try(input, start);
        }

        //функция по проверке строчки на все автоматы сразу, возвращает набор пар <название лексемы, сама лексема>
        public List<KeyValuePair<string, string>> TryAllTrees(string input)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();
            int i = 0;

            while (i <= input.Length)
            {
                int priority = -1;
                int maxLength = -1;
                Tree rightTree = null;
                Token rightToken = null;

                foreach (Tree tree in allTrees)
                {
                    Token temp = tree.Try(input, i);
                    if (temp.isGrammar == true && tree.priority >= priority && temp.lastNumber >= maxLength)
                    {
                        priority = tree.priority;
                        maxLength = temp.lastNumber;
                        rightTree = tree;
                        rightToken = temp;
                    }
                }

                if (rightTree == null)
                {
                    i++;
                }
                else
                {
                    string name = input.Substring(i, rightToken.lastNumber);
                    result.Add(new KeyValuePair<string, string>(rightTree.name, name));
                    i += rightToken.lastNumber;
                }
            }

            return result;
        }
    }
}
