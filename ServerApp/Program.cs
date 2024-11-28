using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseWebSockets();

app.Map("/ws", async context => CreateWebSocketClientAsync(context));

app.Run();

async Task CreateWebSocketClientAsync(HttpContext context)
{
    if (context.WebSockets.IsWebSocketRequest) {
        Console.WriteLine("WebSocket request received");
        var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        Console.WriteLine("Client connected");
        await SendMessageAsync(context, webSocket);
    }
    else {
        context.Response.StatusCode = 400;
        Console.WriteLine("Not a WebSocket request");
    }
}

async Task SendMessageAsync(HttpContext context, WebSocket webSocket) {
    var buffer = new byte[1024 * 4];
    Console.WriteLine("Entering SendMessageAsync method");

    var random = new Random();
    while (webSocket.State == WebSocketState.Open) {
        Console.WriteLine("Sending message to client");
        var message = GetMessage();
        var messageBytes = Encoding.UTF8.GetBytes(message);
        await webSocket.SendAsync(messageBytes, WebSocketMessageType.Text, true, CancellationToken.None);
        Console.WriteLine($"Sent message: {message}");

        var delay = random.Next(1000, 4000);
        await Task.Delay(delay);
    }
    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
}


string GetMessage() {
    var now = DateTime.Now;
    var numbers = new[] { now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second };
    int evenCount = 0;
    int oddCount = 0;

    Console.WriteLine($"Time: {string.Join(", ", numbers)}");
    foreach (var number in numbers) {
        if (number % 2 == 0)
            evenCount++;
        else
            oddCount++;
    }

    if (evenCount > oddCount)
        return "чет!";
    else if (oddCount > evenCount)
        return "нечет!";
    else
        return "равно!";
}
