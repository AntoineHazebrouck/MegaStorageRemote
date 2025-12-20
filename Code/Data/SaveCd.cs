using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using Android.Content;
using MegaStorageRemote.Code.Utils;

namespace MegaStorageRemote.Code.Data;

public class SaveCd
{
    public static Cd Save(Cd cd)
    {
        var prefs = AndroidUtils.Preferences();

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

        return JsonUtils.Parse(saved);
    }
}
