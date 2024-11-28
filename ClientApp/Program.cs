using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


class Program {
    static async Task Main(string[] args) {
        var uri = new Uri("ws://server:80/ws");
        var client = new ClientWebSocket();
        await client.ConnectAsync(uri, CancellationToken.None);

        Console.WriteLine("Connected to server");

        var buffer = new byte[1024 * 4];
        while (client.State == WebSocketState.Open) {
            var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
            Console.WriteLine(message);
        }

        Console.WriteLine("End connection");
        await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
    }
}
