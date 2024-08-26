using System.Text;
using ApiTestRunner.TestModels;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ApiTestRunner.Parsers;

public class YamlTestParser : ITestParser
{
    public async Task<TestContainer?> Parse(string filePath)
    {
        try
        {

            await using var inStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var streamReader = new StreamReader(inStream);
            await Task.Delay(1000);
            var sb = new StringBuilder();
            while (!streamReader.EndOfStream)
            {
                var line = await streamReader.ReadLineAsync();
                sb.AppendLine(line);
            }
            await Task.Delay(1000);

            string yamlContent = sb.ToString();

            if (string.IsNullOrEmpty(yamlContent)) return null;

            var deserializer = new DeserializerBuilder()
                .WithNamingConvention(PascalCaseNamingConvention.Instance)
                .WithCaseInsensitivePropertyMatching()
                .Build();

            var deserialized = deserializer.Deserialize<TestContainer>(yamlContent);
            deserialized.Description = deserialized.Description?.Replace("\n", "")
                .Replace("\r\n", "");
            return deserialized;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex}");
            return null;
        }
    }
}