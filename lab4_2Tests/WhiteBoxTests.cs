using Lab4_2;

namespace Lab4_2Tests
{
    [TestClass]
    public sealed class WhiteBoxPolizTests
    {
        /*  Алгоритм делится на 5 крупных ветвей:
         *  1) токен – операнд            → output
         *  2) токен – оператор           → сравнение precedence, push/pop
         *  3) токен – '('                → push
         *  4) токен – ')'                → pop до '('
         *  5) финальный сброс стека      → pop оставшихся
         *  Плюс ветвь исключения (скобки). Всё это мы проверяем.
         */

        [TestMethod]  // ветвь 1
        public void WB_Operand()
            => Assert.AreEqual("x", Poliz.ConvertToPolishNotation("x"));

        [TestMethod]  // ветвь 2 (+ precedence pop)
        public void WB_OperatorPop()
            => Assert.AreEqual("a b + c -", Poliz.ConvertToPolishNotation("a+b-c"));

        [TestMethod]  // ветвь 3→4 (скобки)
        public void WB_Parentheses()
            => Assert.AreEqual("a b + c *", Poliz.ConvertToPolishNotation("(a+b)*c"));

        [TestMethod]  // ветвь 5 (финальный flush)
        public void WB_FinalFlush()
            => Assert.AreEqual("a b c * +", Poliz.ConvertToPolishNotation("a+b*c"));

        [TestMethod]                                         // ветвь исключения
        [ExpectedException(typeof(InvalidOperationException))]
        public void WB_BadBrackets()
            => Poliz.ConvertToPolishNotation("a+(b*c");
    }
}
