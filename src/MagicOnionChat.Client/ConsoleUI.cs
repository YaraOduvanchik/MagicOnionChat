namespace MagicOnionChat.Client;

public class ConsoleUI
{
    private const string HeaderTitle = "╔════════════════════════════════════╗";
    private const string HeaderMid = "║       MagicOnion Chat Client       ║";
    private const string HeaderEnd = "╚════════════════════════════════════╝";

    private readonly SemaphoreSlim _consoleLock = new(1, 1);

    public void UpdateConsoleTitle(string username)
    {
        Console.Title = $"MagicOnion Chat - {username}";
    }

    public async ValueTask PrintHeaderAsync()
    {
        await _consoleLock.WaitAsync();
        try
        {
            Console.Clear();
            WriteColoredLine(HeaderTitle, ConsoleColor.Cyan);
            WriteColoredLine(HeaderMid, ConsoleColor.Cyan);
            WriteColoredLine(HeaderEnd, ConsoleColor.Cyan);
            Console.WriteLine();
        }
        finally
        {
            _consoleLock.Release();
        }
    }

    public async ValueTask PrintMessageAsync(string message)
    {
        await _consoleLock.WaitAsync();
        try
        {
            Console.Write("\r");
            Console.Write(new string(' ', Console.BufferWidth - 1));
            Console.Write("\r");

            var isServerMessage = message.StartsWith("[SERVER]:");
            var color = isServerMessage ? ConsoleColor.Yellow : ConsoleColor.Green;

            WriteColoredLine($"  {message}", color);
            
            Console.Write("> ");
        }
        finally
        {
            _consoleLock.Release();
        }
    }

    public async ValueTask PrintWelcomeAsync(string userName)
    {
        await _consoleLock.WaitAsync();
        try
        {
            Console.Clear();
            WriteColoredLine(HeaderTitle, ConsoleColor.Cyan);
            WriteColoredLine(HeaderMid, ConsoleColor.Cyan);
            WriteColoredLine(HeaderEnd, ConsoleColor.Cyan);
            Console.WriteLine();
            WriteColoredLine($"Подключены как: {userName}\n", ConsoleColor.Green);
            WriteColoredLine("Введите сообщение (или 'exit' для выхода):\n", ConsoleColor.Gray);
        }
        finally
        {
            _consoleLock.Release();
        }
    }

    public async ValueTask PrintErrorAsync(string message)
    {
        await _consoleLock.WaitAsync();
        try
        {
            Console.Write("\r");
            Console.Write(new string(' ', Console.BufferWidth - 1));
            Console.Write("\r");
            
            WriteColoredLine($"{message}", ConsoleColor.Red);
            Console.Write("> ");
        }
        finally
        {
            _consoleLock.Release();
        }
    }

    public async ValueTask PrintExitAsync()
    {
        await _consoleLock.WaitAsync();
        try
        {
            WriteColoredLine("\nВыход из чата...", ConsoleColor.Yellow);
        }
        finally
        {
            _consoleLock.Release();
        }
    }

    public async ValueTask PrintGoodbyeAsync()
    {
        await _consoleLock.WaitAsync();
        try
        {
            WriteColoredLine("\nСпасибо за использование MagicOnion Chat!", ConsoleColor.Cyan);
        }
        finally
        {
            _consoleLock.Release();
        }
    }

    public async ValueTask<string> ReadUserNameAsync()
    {
        await _consoleLock.WaitAsync();
        try
        {
            Console.Write("Введите ваше имя: ");
            var userName = Console.ReadLine()?.Trim();
            return string.IsNullOrWhiteSpace(userName) ? "Anonymous" : userName;
        }
        finally
        {
            _consoleLock.Release();
        }
    }

    public async ValueTask<string?> ReadMessageAsync()
    {
        await _consoleLock.WaitAsync();
        try
        {
            Console.Write("> ");
        }
        finally
        {
            _consoleLock.Release();
        }
        
        return await Task.Run(Console.ReadLine);
    }

    private static void WriteColoredLine(string text, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(text);
        Console.ResetColor();
    }
}
