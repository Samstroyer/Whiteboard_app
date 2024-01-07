using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

public class Connect : WebSocketBehavior
{
    protected override void OnOpen()
    {
        Message response = new Message() { messageType = MessageType.Hello, message = "Hello from server!\nConnected successfully!" };
        Send(JsonSerializer.Serialize(response));
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        Message message = JsonSerializer.Deserialize<Message>(e.Data);

        switch (message.messageType)
        {
            case MessageType.Update:
                {
                    Message responseObject = new Message() { data = message.data, messageType = MessageType.Update };
                    string responseText = JsonSerializer.Serialize(responseObject);
                    Sessions.Broadcast(responseText);
                }
                break;
            case MessageType.Clear:
                {
                    Message responseObject = new Message() { messageType = MessageType.Clear };
                    string responseText = JsonSerializer.Serialize(responseObject);
                    Sessions.Broadcast(responseText);
                }
                break;
            default:
                break;
        }
    }
}