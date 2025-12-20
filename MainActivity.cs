using Android.Content;
using Android.Hardware;
using Android.Views;
using MegaStorageRemote.Code;

namespace MegaStorageRemote;

[Activity(Label = "@string/app_name", MainLauncher = true)]
public class MainActivity : Activity
{
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

        SetContentView(layout);
    }
}
