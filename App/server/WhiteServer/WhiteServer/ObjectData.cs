using System.Text.Json.Serialization;

public class ObjectData
{
    [JsonInclude, JsonPropertyName("x")]
    public int x;
    [JsonInclude, JsonPropertyName("y")]
    public int y;
    [JsonInclude, JsonPropertyName("col")]
    public string c;
    [JsonInclude, JsonPropertyName("shape")]
    public int? selectedShape;
    [JsonInclude, JsonPropertyName("size")]
    public int? size;
}