using Microsoft.VisualStudio.TestTools.UnitTesting;
using LanguageTask;
using System.Collections.Generic;

namespace LanguageTaskTest
{
    [TestClass]
    public class LanguageTest
    {
        [DataTestMethod]
        [DataRow("int", true, 3, "int")]
        [DataRow(" int", false, 0, "int")]
        [DataRow("int ", true, 3, "int")]
        [DataRow("int1", true, 3, "int")]
        [DataRow("1int", false, 0, "int")]
        [DataRow("in", false, 2, "int")]
        [DataRow("i", false, 1, "int")]
        [DataRow("", false, 0, "int")]

        [DataRow("a", true, 1, "name")]
        [DataRow("aa", true, 2, "name")]
        [DataRow("aaa", true, 3, "name")]
        [DataRow("1aaa", false, 0, "name")]
        [DataRow("_1aaa", true, 5, "name")]
        [DataRow("int", true, 3, "name")]
        [DataRow("123", false, 0, "name")]
        [DataRow("_", true, 1, "name")]

        [DataRow("5", true, 1, "value")]
        [DataRow("-5", true, 2, "value")]
        [DataRow("+15", true, 3, "value")]
        [DataRow("-", false, 1, "value")]
        [DataRow("+1500", true, 5, "value")]
        [DataRow("015", false, 0, "value")]
        [DataRow("150", true, 3, "value")]
        [DataRow("_", false, 0, "value")]

        [DataRow("-", true, 1, "symbol")]
        [DataRow("+-", true, 1, "symbol")]
        [DataRow("+;;", true, 1, "symbol")]
        [DataRow(" -", false, 0, "symbol")]
        [DataRow("+1500", true, 1, "symbol")]
        [DataRow("0", false, 0, "symbol")]
        [DataRow("+qqdveqegqeg", true, 1, "symbol")]
        [DataRow("_", true, 1, "symbol")]

        public void TryTreeTest(string input, bool grammar, int number, string tree)
        {
            Automatic automatic = new Automatic();
            Token expected = new Token(grammar, number);
            Token result = automatic.TryTree(input, 0, tree);
            Assert.AreEqual(expected.isGrammar, result.isGrammar, "Wrong answer!");
            Assert.AreEqual(expected.lastNumber, result.lastNumber, "Wrong last number!");
        }

        [TestMethod]
        public void TryAllTreesTest()
        {
            string input = "int a = +150; i n t -05 qweg12 +012";
            List<KeyValuePair<string, string>> expected = new List<KeyValuePair<string, string>>();
            expected.Add(new KeyValuePair<string, string>("int","int"));
            expected.Add(new KeyValuePair<string, string>("name", "a"));
            expected.Add(new KeyValuePair<string, string>("symbol", "="));
            expected.Add(new KeyValuePair<string, string>("value", "+150"));
            expected.Add(new KeyValuePair<string, string>("symbol", ";"));
            expected.Add(new KeyValuePair<string, string>("name", "i"));
            expected.Add(new KeyValuePair<string, string>("name", "n"));
            expected.Add(new KeyValuePair<string, string>("name", "t"));
            expected.Add(new KeyValuePair<string, string>("symbol", "-"));
            expected.Add(new KeyValuePair<string, string>("value", "5"));
            expected.Add(new KeyValuePair<string, string>("name", "qweg12"));
            expected.Add(new KeyValuePair<string, string>("symbol", "+"));
            expected.Add(new KeyValuePair<string, string>("value", "12"));

            Automatic automatic = new Automatic();
            List<KeyValuePair<string, string>> result = automatic.TryAllTrees(input);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expected[i].Key, result[i].Key, "Wrong lexema!");
                Assert.AreEqual(expected[i].Value, result[i].Value, "Wrong substring!");
            }
        }
    }
}
