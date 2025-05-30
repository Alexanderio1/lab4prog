using Microsoft.VisualStudio.TestTools.UnitTesting;

// Параллелим каждый [TestMethod].
// Workers = 0  →  MSTest сам возьмёт количество ядер процессора.
[assembly: Parallelize(
    Workers = 0,
    Scope = ExecutionScope.MethodLevel)]
