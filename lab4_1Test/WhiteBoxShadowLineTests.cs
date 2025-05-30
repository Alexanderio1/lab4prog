using Lab4;

namespace lab4_1Test
{
    [TestClass]
    public class WhiteBoxShadowLineTests
    {
        // Путь A  – пустой массив
        [TestMethod]
        public void EmptyArray()
        {
            long r = new ShadowLine(new int[0, 2]).CalculateSum();
            Assert.AreEqual(0L, r);
        }

        // B-C-F – два непересекающихся
        [TestMethod]
        public void Disjoint()
        {
            var c = new int[,] { { 1, 3 }, { 5, 7 } };
            Assert.AreEqual(4L, new ShadowLine(c).CalculateSum());
        }

        // B-D-F – перекрытие + расширение
        [TestMethod]
        public void OverlapWithExtend()
        {
            var c = new int[,] { { 1, 3 }, { 2, 5 } };
            Assert.AreEqual(4L, new ShadowLine(c).CalculateSum());
        }

        // B-E-F – вложенный сегмент
        [TestMethod]
        public void Contained()
        {
            var c = new int[,] { { 1, 5 }, { 2, 3 } };
            Assert.AreEqual(4L, new ShadowLine(c).CalculateSum());
        }

        // B-D-F (грань) – касание концами
        [TestMethod]
        public void Touching()
        {
            var c = new int[,] { { 1, 3 }, { 3, 5 } };
            Assert.AreEqual(4L, new ShadowLine(c).CalculateSum());
        }
    }
}
