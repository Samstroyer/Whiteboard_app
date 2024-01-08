using System.Text.Json.Serialization;

public class ObjectData
{
    [JsonInclude, JsonPropertyName("x")]
    public double x;
    [JsonInclude, JsonPropertyName("y")]
    public double y;
    [JsonInclude, JsonPropertyName("col")]
    public string? c;
    [JsonInclude, JsonPropertyName("shape")]
    public int? selectedShape;
    [JsonInclude, JsonPropertyName("size")]
    public int? size;
}