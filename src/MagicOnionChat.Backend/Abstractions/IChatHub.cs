using MagicOnion;

namespace MagicOnionChat.Backend.Abstractions;

public interface IChatHub : IStreamingHub<IChatHub, IChatReceiver>
{
    ValueTask SendAsync(string message);
    ValueTask JoinAsync(string userName);
}