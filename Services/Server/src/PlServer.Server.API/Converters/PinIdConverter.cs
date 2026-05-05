using PlServer.Domain.Nodes;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PlServer.Server.API.Converters;

public class PinIdConverter : JsonConverter<NodePinId>
{
    public override NodePinId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString();

        if (str == null)
            return default;

        NodePinId.TryParse(str, out var pin);
        return pin;
    }

    public override void Write(Utf8JsonWriter writer, NodePinId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Id.ToString());
    }
}
