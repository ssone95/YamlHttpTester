using ApiTestRunner.TestModels;

namespace ApiTestRunner.Services;

public interface ITestManager
{
    Task<List<TestContainer>> LoadTests(string? customPath = null);
}