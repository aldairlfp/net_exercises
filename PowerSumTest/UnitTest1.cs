using PowerSum;

namespace PowerSumTest;

[TestClass]
public class ProgramTests
{
    [TestMethod]
    public void TestCountPowerSetsWithValidInput()
    {
        int x = 16;
        int n = 2;
        int expectedCount = 1;
        int actualCount = Program.powerSum(x, n);
        Assert.AreEqual(expectedCount, actualCount);
    }

    [TestMethod]
    public void TestCountPowerSetsWithNoValidSubsets()
    {
        int x = 19;
        int n = 2;
        int expectedCount = 0;
        int actualCount = Program.powerSum(x, n);
        Assert.AreEqual(expectedCount, actualCount);
    }

    [TestMethod]
    public void TestCountPowerSetsWithMultipleSubsets()
    {
        int x = 100;
        int n = 2;
        int expectedCount = 3;
        int actualCount = Program.powerSum(x, n);
        Assert.AreEqual(expectedCount, actualCount);
    }

    // Add more test cases as needed...
}