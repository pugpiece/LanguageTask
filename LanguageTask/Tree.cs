using System.Linq;

namespace LanguageTask
{
    class Tree
    {
        public int Q { get; set; } // сколько состояний q есть у данного автомата
        public string[][] map { get; set; } // "карта" Q на Q, по которой можно понять, из какого состояния
                                            // q в какое можно попасть и как (строчка хранит все символы, по
                                            // которым можно совершить переход)
        public int priority { get; set; } // приоритет автомата
        public string name { get; set; } // название автомата, нужно чтобы выводить его во втором задании

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

        public Token Try(string input, int start) // функция, которая проверяет подходит ли входная строка
                                                  // (с уточнением начала) по автомату
        {
            Token token = new Token();
            int q = 0;
            for (int i = start; i < input.Length; i++)
            {
                for (int k = 0; k < Q; k++)
                {
                    if (map[q][k].Contains(input[i])) //если текущий символ есть в списке переходов из q в k
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
