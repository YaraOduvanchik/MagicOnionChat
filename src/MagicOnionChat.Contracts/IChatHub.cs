using MagicOnion;

namespace MagicOnionChat.Contracts;

public interface IChatHub : IStreamingHub<IChatHub, IChatReceiver>
{
    ValueTask SendAsync(string message);
    ValueTask JoinAsync(string userName);
}