using Bookstore.Domain;
using Xunit.Abstractions;

namespace TestProject1;

public class UnitTest1
{
    private readonly ITestOutputHelper _testOutputHelper;

    public UnitTest1(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }

    [Fact]
    public void Test1()
    {
        var result = PrimeGenerator.CoPilotGeneratesPrimes();

        foreach (var prime in result)
            _testOutputHelper.WriteLine(prime.ToString());
    }
    
    [Fact]
    public void TwoIsPrime()
    {
        var result = PrimeGenerator.MyGeneratesPrimes(2);

        Assert.Equal(2, result.First());
    }
    
    [Fact]
    public void ThreeIsPrime()
    {
        var result = PrimeGenerator.MyGeneratesPrimes(3);

        Assert.Equal(3, result.Last());
    }
}