using Android.Content;
using Android.Hardware;
using Android.Views;
using Android.Widget;
using MegaStorageRemote.Code;
using MegaStorageRemote.Code.Data;
using MegaStorageRemote.Code.Presentation;
using MegaStorageRemote.Code.Utils;

namespace MegaStorageRemote;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    private ListView? cds;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        var irManager = AndroidUtils.Infrared(this);

        var layout = new LayoutBuilder(this)
            .AddButton(
                "Power",
                (sender, e) =>
                {
                    irManager.Power();
                }
            )
            .AddButton(
                "Add a cd",
                (sender, e) =>
                {
                    StartActivity(new Intent(this, typeof(AddCdForm)));
                }
            )
            .build();

        var data = ReadCds.Read();
        cds = new ListView(this)
        {
            Adapter = new CdAdapter(this, data),
            LayoutParameters = new LinearLayout.LayoutParams(ViewGroup.LayoutParams.MatchParent, 0)
            {
                Weight = 1f,
            },
            Divider = null,
        };

        layout.AddView(cds);
        SetContentView(layout);
    }

    protected override void OnResume()
    {
        base.OnResume();

        var fresh = ReadCds.Read();
        cds?.Adapter = new CdAdapter(this, fresh);
    }
}
