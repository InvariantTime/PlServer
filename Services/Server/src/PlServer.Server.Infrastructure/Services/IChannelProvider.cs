using Grpc.Core;

namespace PlServer.Server.Infrastructure.Services;

public interface IChannelProvider
{
    ChannelBase GetGrpcChannel();
}
