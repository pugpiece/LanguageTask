using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LanguageTask
{
    class Automatic
    {
        private Tree intTree;

        private Tree valueTree;

        private Tree nameTree;

        private Tree symbolTree;

        public Automatic()
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
