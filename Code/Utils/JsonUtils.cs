using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MegaStorageRemote.Code;

public class JsonUtils
{
    public static Cd Parse(string cd)
    {
        return JsonSerializer.Deserialize(cd, JsonContext.Default.Cd)
            ?? throw new InvalidOperationException("Could not deserialize the CD");
    }
}

[JsonSerializable(typeof(Cd))]
internal partial class JsonContext : JsonSerializerContext { }
