using System.Linq;

namespace LanguageTask
{
    class Tree
    {
        public int Q { get; set; }
        public string[][] map { get; set; }
        public int priority { get; set; }
        public string name { get; set; }

        public Tree()
        {
            Q = 0;
            priority = -1;
            name = "unknown";
        }
        public Tree(int QNew, string[][] mapNew, int priorityNew, string nameNew)
        {
            Q = QNew;
            map = mapNew;
            priority = priorityNew;
            name = nameNew;
        }

        public Token Try(string input, int start)
        {
            Token token = new Token();
            int q = 0;
            for (int i = start; i < input.Length; i++)
            {
                for (int k = 0; k < Q; k++)
                {
                    if (map[q][k].Contains(input[i]))
                    {
                        q = k;
                        token.lastNumber++;
                        break;
                    }
                    else if (k == Q - 1)
                    {
                        return token;
                    }
                }
                if (q == Q - 1)
                {
                    token.isGrammar = true;
                }
            }

            return token;
        }
    }
}
