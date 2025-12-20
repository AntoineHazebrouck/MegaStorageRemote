using System;
using Android.Content;

namespace MegaStorageRemote.Code.Utils;

public class AndroidUtils
{
    public static ISharedPreferences Preferences()
    {
        return Application.Context.GetSharedPreferences(
                "MegaStorageRemoteData",
                FileCreationMode.Private
            ) ?? throw new InvalidOperationException("Could not retrieve ISharedPreferences");
    }
}
