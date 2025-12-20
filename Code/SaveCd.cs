using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Android.Content;

namespace MegaStorageRemote.Code;

public class SaveCd
{
    public static Cd Save(Cd cd)
    {
        var prefs =
            Application.Context.GetSharedPreferences(
                "MegaStorageRemoteData",
                FileCreationMode.Private
            ) ?? throw new InvalidOperationException("Could not retrieve ISharedPreferences");

        var editor =
            prefs.Edit()
            ?? throw new InvalidOperationException("Could not retrieve ISharedPreferencesEditor");

        editor.PutString(
            cd.Position.ToString(),
            JsonSerializer.Serialize(cd, JsonContext.Default.Cd)
        );

        editor.Commit();

        string saved =
            prefs.GetString(cd.Position.ToString(), null)
            ?? throw new InvalidOperationException("Could not save the CD");

        return JsonSerializer.Deserialize(saved, JsonContext.Default.Cd)
            ?? throw new InvalidOperationException("Could not deserialize the CD");
    }
}

[JsonSerializable(typeof(Cd))]
internal partial class JsonContext : JsonSerializerContext { }
