using MagicOnionChat.Client;

Console.WriteLine("=== MagicOnion Chat Client ===");
Console.WriteLine("Подключение к серверу...\n");

var serverPort = args.Length > 0 ? args[0] : "8081";

var serverUrl = $"http://localhost:{serverPort}";
Console.WriteLine($"Используем адрес: {serverUrl}");

var client = new ChatClient(serverUrl);

try
{
    await client.RunAsync();
}
catch (Exception ex)
{
    Console.WriteLine($"Ошибка: {ex.Message}");
}