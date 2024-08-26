using ApiTestRunner.TestModels;

namespace ApiTestRunner.Parsers;

public interface ITestParser
{
    public Task<TestContainer?> Parse(string filePath);
}