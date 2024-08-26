using System.Reflection;
using ApiTestRunner.Parsers;
using ApiTestRunner.Services;
using ApiTestRunner.TestModels;

namespace ApiTestRunner;

class Program
{
    static async Task Main(string[] args)
    {
        ITestManager manager = new TestManager(new YamlTestParser());
        var tests = await manager.LoadTests();
        foreach (var test in tests)
        {
            Console.WriteLine($"Test {test.Name} ({test?.Description}) loaded!");
        }

        // Console.ReadKey();
    }
}