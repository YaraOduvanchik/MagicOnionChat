using Cysharp.Runtime.Multicast;

namespace MagicOnionChat.Backend.Infrastructure.Chat;

public class ChatContextRepository
{
    private readonly IMulticastGroupProvider _groupProvider;
    private ChatContext _chatContext;

    public ChatContextRepository(IMulticastGroupProvider groupProvider)
    {
        _groupProvider = groupProvider;
        _chatContext = new ChatContext(_groupProvider);
    }

    public ChatContext Chat => _chatContext;
}
