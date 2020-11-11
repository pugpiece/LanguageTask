namespace LanguageTask
{
    public class Token //класс токен, элемент этого класса возвращает автомат по завершению работы
    {
        public bool isGrammar;
        public int lastNumber;

        public Token()
        {
            isGrammar = false;
            lastNumber = 0;
        }

        public Token(bool grammar, int number)
        {
            isGrammar = grammar;
            lastNumber = number;
        }
    }
}
