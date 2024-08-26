using System.Text.Json.Serialization;
using YamlDotNet.Serialization;

namespace ApiTestRunner.TestModels;

[YamlSerializable]
public class TestContainer
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string BaseUrl { get; set; }
    
    public List<HeaderEntry>? BaseHeaders { get; set; }
    public List<BaseTest>? Tests { get; set; }
}