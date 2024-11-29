using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using ServerApp;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;



namespace Tests {
    public class WebSocketTests : IClassFixture<WebApplicationFactory<ServerApp.Startup>> {
        private readonly HttpClient _client;

        public WebSocketTests(WebApplicationFactory<ServerApp.Startup> factory) {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Test_WebSocketConnection() {
            using (var webSocket = new ClientWebSocket()) {
                try {
                    var uri = new Uri("ws://server:80/ws");
                    Console.WriteLine("Попытка подключения к WebSocket...");
                    await webSocket.ConnectAsync(uri, CancellationToken.None);
                    Console.WriteLine("WebSocket подключен.");

                    var buffer = new byte[1024];
                    var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                    var message = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    Console.WriteLine($"Получено сообщение: {message}");

                    // Проверка, что сообщение соответствует ожиданиям
                    Assert.True(message == "чет!" || message == "нечет!" || message == "равно!", "Полученное сообщение не соответствует ожиданиям.");
                    Console.WriteLine("Сообщение соответствует ожиданиям.");
                } catch (Exception ex) {
                    Assert.True(false, $"Произошла ошибка: {ex.Message}");
                } finally {
                    if (webSocket.State == WebSocketState.Open) {
                        await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                        Console.WriteLine("WebSocket закрыт.");
                    }
                }
            }
        }
    }
}
