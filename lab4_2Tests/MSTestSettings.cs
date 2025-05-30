using Microsoft.VisualStudio.TestTools.UnitTesting;

// Параллелим каждый [TestMethod] в своём рабочем потоке.
[assembly: Parallelize(
    Workers = 0,                         // 0 → «по числу ядер»
    Scope = ExecutionScope.MethodLevel)]
