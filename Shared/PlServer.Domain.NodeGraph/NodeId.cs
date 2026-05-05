
namespace PlServer.Domain.Nodes;

public readonly record struct NodeId(Guid Id)
{
    public static NodeId New() => new(Guid.NewGuid());

    public static bool TryParse(string str, out NodeId node)
    {
        node = default;

        if (Guid.TryParse(str, out var guid) == false)
            return false;

        node = new NodeId(guid);
        return true;
    }
}