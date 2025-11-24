using MagicOnion.Server.Hubs;
using MagicOnionChat.Backend.Abstractions;
using MagicOnionChat.Backend.Repositories;

namespace MagicOnionChat.Backend.Core;

public class ChatHub(ChatContextRepository chatRepository) : StreamingHubBase<IChatHub, IChatReceiver>, IChatHub
{
    private ChatContext _chat = default!;
    private string _userName = string.Empty;

    protected override ValueTask OnConnected()
    {
        _chat = chatRepository.Chat;
        _chat.Group.Add(Context.ContextId, Client);
        return default;
    }

    protected override ValueTask OnDisconnected()
    {
        _chat.Group.Remove(Context.ContextId);
        return default;
    }

    public ValueTask JoinAsync(string userName)
    {
        _userName = ValidateName(userName);
        _chat.CommandQueue.Enqueue(
            new ChatMessageCommand("[SERVER]", $"{_userName} присоединился к чату"));
        return default;
    }

    public ValueTask SendAsync(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
            return default;

        _chat.CommandQueue.Enqueue(
            new ChatMessageCommand(_userName, message));
        return default;
    }

    private static string ValidateName(string userName) =>
        string.IsNullOrWhiteSpace(userName) ? "Anonymous" : userName;
}

