using Lab4_2;

namespace Lab4_2Tests
{
    [TestClass]
    public sealed class PostfixNotationTests
    {
        // ───── 1. Базовые приоритеты (+, -, *, /, ^) ─────
        [DataTestMethod]
        [DataRow("1+2*3", "1 2 3 * +")]
        [DataRow("a+b*c", "a b c * +")]
        [DataRow("a^b^c", "a b c ^ ^")]          // ^ – правоассоциативный
        public void Priority(string infix, string expected)
            => Assert.AreEqual(expected, Poliz.ConvertToPolishNotation(infix));

        // ───── 2. Скобки переопределяют приоритет ─────  
        [DataTestMethod]
        [DataRow("(a+b)*c", "a b + c *")]
        [DataRow("((a+b)*c)/d", "a b + c * d /")]
        [DataRow("a+(b*(c+d))", "a b c d + * +")]
        public void Parentheses(string infix, string expected)
            => Assert.AreEqual(expected, Poliz.ConvertToPolishNotation(infix));

        // ───── 3. Граничные ввода ─────
        [DataTestMethod]
        [DataRow("", "")]
        [DataRow("   ", "")]
        [DataRow("x", "x")]
        [DataRow("  42  ", "42")]
        public void Trivial(string infix, string expected)
            => Assert.AreEqual(expected, Poliz.ConvertToPolishNotation(infix));

        // ───── 4. Длинные идентификаторы и цифры ─────
        [TestMethod]
        public void LongIdentifiers()
        {
            string infix = "counter1+value2*Sum3";
            string postfix = "counter1 value2 Sum3 * +";
            Assert.AreEqual(postfix, Poliz.ConvertToPolishNotation(infix));
        }

        // ───── 5. Ошибки ─────
        [TestMethod]                                         // несбалансированные скобки
        [ExpectedException(typeof(InvalidOperationException))]
        public void UnbalancedLeft() => Poliz.ConvertToPolishNotation("(a+b");

        [TestMethod]                                         // лишняя правая скобка
        [ExpectedException(typeof(InvalidOperationException))]
        public void UnbalancedRight() => Poliz.ConvertToPolishNotation("a+b)");

        [TestMethod]                                         // недопустимый символ
        [ExpectedException(typeof(ArgumentException))]
        public void InvalidChar() => Poliz.ConvertToPolishNotation("a+#b");
    }
}
