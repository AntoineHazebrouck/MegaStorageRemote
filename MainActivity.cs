using Android.Content;
using Android.Hardware;
using Android.Views;
using Android.Widget;
using MegaStorageRemote.Code;
using MegaStorageRemote.Code.Data;
using MegaStorageRemote.Code.Presentation;

namespace MegaStorageRemote;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
    private ListView? cds;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        if (GetSystemService(ConsumerIrService) is not ConsumerIrManager ir)
            throw new InvalidOperationException("Could not retrieve the infrared emmitter");
        var irManager = new IrManager(ir);

        var layout = new LayoutBuilder(this)
            .AddButton(
                "Power",
                (sender, e) =>
                {
                    irManager.Power();
                }
            )
            .AddButton(
                "Play cd 12",
                (sender, e) =>
                {
                    irManager.PlayCd(12);
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
        cds.ItemClick += (s, e) =>
        {
            Toast.MakeText(this, $"Selected : {data[e.Position]}", ToastLength.Short)?.Show();
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
