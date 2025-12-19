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

        var layout = new LinearLayout(this)
        {
            Orientation = Orientation.Vertical,
            LayoutParameters = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent
            ),
        };
        layout.SetPadding(40, 40, 40, 40);

        // layout.AddView(new TextView(this) { Text = "Hello from C#!", TextSize = 24 });

        AddButton(
            text: "Power",
            layout: layout,
            callback: (sender, e) =>
            {
                irManager.Power();
            }
        );
        // AddButton(
        //     text: "Disc",
        //     layout: layout,
        //     callback: (sender, e) =>
        //     {
        //         irManager.Disc();
        //     }
        // );
        AddButton(
            text: "Play disc 12",
            layout: layout,
            callback: (sender, e) =>
            {
                irManager.PlayDisc(12);
            }
        );

        SetContentView(layout);
    }

    private void AddButton(LinearLayout layout, string text, EventHandler callback)
    {
        Button power = new(this) { Text = text };
        power.Click += callback;
        layout.AddView(power);
    }
}
