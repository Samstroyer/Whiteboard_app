using System.Text.Json;
using WebSocketSharp.Server;

WebSocketServer socketServer = new WebSocketServer(42069);


socketServer.AddWebSocketService<Connect>("/connect");

Message mms = new Message();
mms.messageType = MessageType.Hello;
mms.data = new ObjectData() { x = 10, y = 20 };
mms.message = "test";


string test = JsonSerializer.Serialize<Message>(mms);
Console.WriteLine(test);

socketServer.Start();
Console.ReadLine();
socketServer.Stop();