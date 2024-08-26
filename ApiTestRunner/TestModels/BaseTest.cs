using YamlDotNet.Serialization;

namespace ApiTestRunner.TestModels;

[YamlSerializable]
public class BaseTest
{
    public int Priority { get; set; } = 1;
    public required string Name { get; set; }
    public required string Endpoint { get; set; }
    public string? OverrideUrl { get; set; }
    public required string Method { get; set; } = "GET";
    public List<HeaderEntry>? Headers { get; set; }
}