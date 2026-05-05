using PlServer.Domain.Nodes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlServer.Server.API.Converters;

public class NodeIdConverter : JsonConverter<NodeId>
{
    public override NodeId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();

        if (str == null)
            return default;

        NodeId.TryParse(str, out var node);
        return node;
    }

    public override void Write(Utf8JsonWriter writer, NodeId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id.ToString());
    }
}
