namespace PlServer.Domain.Nodes;

public readonly record struct NodePinId(Guid Id)
{
    public static NodePinId New() => new(Guid.NewGuid());

    public static bool TryParse(string str, out NodePinId pin)
    {
        pin = default;

        if (Guid.TryParse(str, out var guid) == false)
            return false;

        pin = new NodePinId(guid);
        return true;
    }
}