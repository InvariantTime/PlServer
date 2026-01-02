using Grpc.Core;
using Grpc.Net.Client;
using System.Collections.Immutable;

namespace PlServer.Server.Infrastructure.Services;

public class ChannelProvider : IChannelProvider
{
    private readonly ImmutableArray<ChannelBase> _channels;

    public ChannelProvider(IEnumerable<string> hosts)
    {
        _channels = hosts.Select<string, ChannelBase>(GrpcChannel.ForAddress).ToImmutableArray();
    }

    public ChannelBase GetGrpcChannel()
    {
        int index = Random.Shared.Next(_channels.Length);
        return _channels[index];
    }
}
