using System.Text.Json.Serialization;

public enum MessageType : int
{
    Hello = 0,
    Update = 1,
    Clear = 2,
    Chat = 3,
}

public class Message
{
    [JsonInclude, JsonPropertyName("type")]
    public MessageType messageType;
    [JsonInclude, JsonPropertyName("data")]
    public ObjectData? data;
    [JsonInclude, JsonPropertyName("message")]
    public string? chatMessage;
}