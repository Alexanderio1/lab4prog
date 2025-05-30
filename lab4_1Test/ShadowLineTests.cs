using Lab4;

namespace lab4_1Test
{
    [TestClass]
    public class ShadowLineTests
    {
        // ─────────── Black-box ───────────

        [TestMethod]
        public void SingleSegment()                                   // (5-1)=4
        {
            var line = new ShadowLine(new int[,] { { 1, 5 } });
            long r = line.CalculateSum();
            Assert.AreEqual(4L, r);
        }

        [TestMethod]
        public void MultipleNonOverlapping()
        {
            var coords = new int[,] { { 1, 3 }, { 5, 7 }, { 9, 12 } };
            long r = new ShadowLine(coords).CalculateSum();
            Assert.AreEqual(7L, r);                                   // 2+2+3
        }

        [TestMethod]
        public void CrossingZeroSegment_ReturnsCorrectSum()
        {
            var coords = new int[,] { { -2, 2 } };
            long r = new ShadowLine(coords).CalculateSum();
            Assert.AreEqual(4L, r);  // (2 - (-2)) = 4
        }


        [TestMethod]
        public void PartiallyOverlapping()
        {
            var coords = new int[,] { { 1, 5 }, { 3, 7 }, { 6, 10 } };
            long r = new ShadowLine(coords).CalculateSum();
            Assert.AreEqual(9L, r);
        }

        [TestMethod]
        public void CompletelyOverlapping()
        {
            var coords = new int[,] { { 1, 10 }, { 2, 5 }, { 6, 8 } };
            long r = new ShadowLine(coords).CalculateSum();
            Assert.AreEqual(9L, r);
        }

        [TestMethod]
        public void CommonEndpoints()
        {
            var coords = new int[,] { { 1, 3 }, { 3, 5 }, { 5, 7 } };
            long r = new ShadowLine(coords).CalculateSum();
            Assert.AreEqual(6L, r);
        }

        [TestMethod]
        public void EmptyArray_ReturnsZero()
        {
            var line = new ShadowLine(new int[0, 2]);
            Assert.AreEqual(0L, line.CalculateSum());
        }

        [TestMethod]
        public void PointSegments()
        {
            var coords = new int[,] { { 1, 1 }, { 3, 3 }, { 5, 5 } };
            long r = new ShadowLine(coords).CalculateSum();
            Assert.AreEqual(0L, r);
        }

        [TestMethod]
        public void UnsortedInput()
        {
            var coords = new int[,] { { 9, 12 }, { 1, 3 }, { 5, 7 } };
            long r = new ShadowLine(coords).CalculateSum();
            Assert.AreEqual(7L, r);
        }

        [TestMethod]
        public void NegativeValues()
        {
            var coords = new int[,] { { -5, -2 }, { 1, 4 } };
            long r = new ShadowLine(coords).CalculateSum();
            Assert.AreEqual(6L, r);
        }

        [TestMethod]
        public void MixedOverlap()
        {
            var coords = new int[,] {
                { 1, 5 }, { 3, 8 }, { 7, 10 }, { 12, 15 }, { 2, 4 }
            };
            long r = new ShadowLine(coords).CalculateSum();
            Assert.AreEqual(12L, r);
        }

        [TestMethod]
        public void LargeValues_NoOverflow()
        {
            var coords = new int[,] { { -1_000_000_000, -1 }, { 0, 1_000_000_000 } };
            long expected = 999_999_999L + 1_000_000_000L;
            Assert.AreEqual(expected, new ShadowLine(coords).CalculateSum());
        }

        [TestMethod]
        public void MinBoundaryOnly()
        {
            var coords = new int[,] { { int.MinValue, -1_000_000_000 } };
            long expected = -1_000_000_000L - int.MinValue;
            Assert.AreEqual(expected, new ShadowLine(coords).CalculateSum());
        }

        [TestMethod]
        public void MaxBoundaryOnly()
        {
            var coords = new int[,] { { 1_000_000_000, int.MaxValue } };
            long expected = (long)int.MaxValue - 1_000_000_000L;
            Assert.AreEqual(expected, new ShadowLine(coords).CalculateSum());
        }

        [TestMethod]
        public void HundredDisjointSegments()
        {
            const int N = 100;
            var coords = new int[N, 2];
            for (int i = 0; i < N; i++) { coords[i, 0] = i * 3; coords[i, 1] = i * 3 + 2; }
            long r = new ShadowLine(coords).CalculateSum();
            Assert.AreEqual(N * 2L, r);
        }

        // ─────────── Negative input ───────────

        [TestMethod]
        public void Constructor_ThirdColumn_Throws()
        {
            var bad = new int[,] { { 1, 2, 3 } };
            Assert.ThrowsException<ArgumentException>(() => new ShadowLine(bad));
        }

        [TestMethod]
        public void Constructor_LeftGreaterThanRight_Throws()
        {
            var bad = new int[,] { { 5, 1 } };
            Assert.ThrowsException<ArgumentException>(() => new ShadowLine(bad));
        }

        [TestMethod]
        public void Constructor_ValidInput_CreatesObject()
        {
            var line = new ShadowLine(new int[,] { { 1, 5 } });
            Assert.IsNotNull(line);
        }

    }
}
