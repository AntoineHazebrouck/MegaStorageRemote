using System;
using Android.Content;
using Android.Hardware;
using Android.Views;
using Android.Widget;
using MegaStorageRemote.Code;
using MegaStorageRemote.Code.Data;
using MegaStorageRemote.Code.Presentation;

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

    public static IrManager Infrared(Context context)
    {
        if (context.GetSystemService(Context.ConsumerIrService) is not ConsumerIrManager ir)
            throw new InvalidOperationException("Could not retrieve the infrared emmitter");
        var irManager = new IrManager(ir);

        return irManager;
    }
}
