using MagicOnionChat.Client;

var ui = new ConsoleUI();

await ui.PrintHeaderAsync();

var serverUrl = $"http://localhost:{GetServerPort()}";
await using var client = new ChatClient(serverUrl, ui);

try
{
    await client.RunAsync();
}
catch (Exception ex)
{
    await ui.PrintErrorAsync($"Критическая ошибка: {ex.Message}");
}
finally
{
    await ui.PrintGoodbyeAsync();
}

static string GetServerPort() => Environment.GetEnvironmentVariable("CHAT_PORT") ?? "8081";