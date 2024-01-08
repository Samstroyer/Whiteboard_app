using System.Text.Json;
using WebSocketSharp;
using WebSocketSharp.Server;

public class Connect : WebSocketBehavior
{
    protected override void OnOpen()
    {
        // Send hello / that a user entered!
        Message response = new Message() { messageType = MessageType.Hello, chatMessage = "Hello from server!\nConnected successfully!" };
        Send(JsonSerializer.Serialize(response));
        Message joinNotification = new Message() { messageType = MessageType.Chat, chatMessage = "A user joined the chat!" };
        Sessions.Broadcast(JsonSerializer.Serialize(joinNotification));
    }

    protected override void OnMessage(MessageEventArgs e)
    {
        // Can't serialize this
        if (e.Data == "") return;

        // Serialize and return if it is null
        Message? message = JsonSerializer.Deserialize<Message>(e.Data);
        if (message == null) return;

        // Check type and act on it
        switch (message.messageType)
        {
            // Someone drew
            case MessageType.Update:
                {
                    Message responseObject = new Message() { data = message.data, messageType = MessageType.Update };
                    string responseText = JsonSerializer.Serialize(responseObject);
                    Sessions.Broadcast(responseText);
                }
                break;
            // Someone wants to clear the screen
            case MessageType.Clear:
                {
                    Message responseObject = new Message() { messageType = MessageType.Clear };
                    string responseText = JsonSerializer.Serialize(responseObject);
                    Sessions.Broadcast(responseText);
                }
                break;
            // Someone typed in chat
            case MessageType.Chat:
                {
                    Message responseObject = new Message() { messageType = MessageType.Chat, chatMessage = message.chatMessage };
                    string responseText = JsonSerializer.Serialize(responseObject);
                    Sessions.Broadcast(responseText);
                }
                break;
            // Undefined behaviour
            default:
                Console.WriteLine("Error in default switch! Not valid");
                break;
        }
    }

    protected override void OnClose(CloseEventArgs e)
    {
        if (e.Reason != string.Empty)
        {
            Console.WriteLine($"Disconnected client, reason: {e.Reason}");
        }
        base.OnClose(e);
    }

    protected override void OnError(WebSocketSharp.ErrorEventArgs e)
    {
        if (e.Message != string.Empty)
        {
            Console.WriteLine($"Error: {e.Exception}");
        }
        base.OnError(e);
    }
}