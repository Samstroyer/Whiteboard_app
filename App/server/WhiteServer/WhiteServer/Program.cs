using WebSocketSharp.Server;

WebSocketServer socketServer = new WebSocketServer(42069);
socketServer.AddWebSocketService<Connect>("/connect");
socketServer.Start();

Console.WriteLine("Started server!\nListening for connections!");

Console.ReadLine();
socketServer.Stop();