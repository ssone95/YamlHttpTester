using System.Reflection;
using ApiTestRunner.Parsers;
using ApiTestRunner.TestModels;

namespace ApiTestRunner.Services;

public class TestManager(ITestParser parser) : ITestManager
{
    public async Task<List<TestContainer>> LoadTests(string? customPath = null)
    {
        string testsPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        testsPath = Path.Combine(testsPath, "Tests");
        if (!string.IsNullOrEmpty(customPath) && Path.Exists(customPath))
        {
            testsPath = customPath!;
        }

        string[] filters = [".yml", ".yaml"];
        if (File.Exists(testsPath) && (testsPath.EndsWith(".yml") || testsPath.EndsWith(".yaml")))
        {
            string basePath = Path.GetDirectoryName(testsPath)!;
            string fileName = testsPath.Replace(basePath, "");
            filters = [fileName];
            testsPath = basePath;
        }
        
        List<string> files = Directory.GetFiles(testsPath)
            .Where(x => filters.Any(y => x.EndsWith(y, StringComparison.OrdinalIgnoreCase)))
            .ToList();

        List<TestContainer> loadedTests = [];

        foreach (var file in files)
        {
            var testContainer = await parser.Parse(file);
            if (testContainer is null)
            {
                Console.WriteLine($"Test File {file} couldn't be loaded!");
                continue;
            }
            loadedTests.Add(testContainer);
        }

        return loadedTests;
    }
}