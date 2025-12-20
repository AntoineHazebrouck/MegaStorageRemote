using System;
using Android.Text;
using Android.Views;

namespace MegaStorageRemote.Code;

public class LayoutBuilder
{
    private readonly Activity CurrentActivity;
    private readonly LinearLayout Layout;

    public LayoutBuilder(Activity current)
    {
        var layout = new LinearLayout(current)
        {
            Orientation = Orientation.Vertical,
            LayoutParameters = new LinearLayout.LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.MatchParent
            ),
        };
        layout.SetPadding(40, 40, 40, 40);

        Layout = layout;
        CurrentActivity = current;
    }

    public LinearLayout build()
    {
        return Layout;
    }

    public LayoutBuilder AddButton(string text, EventHandler callback)
    {
        Button power = new(CurrentActivity) { Text = text };
        power.Click += callback;
        Layout.AddView(power);
        return this;
    }

    public LayoutBuilder AddText(string label)
    {
        Layout.AddView(new TextView(CurrentActivity) { Text = label });
        return this;
    }

    public LayoutBuilder AddEditText(string hint, Action<string> onTextChanged)
    {
        EditText edit = new(CurrentActivity) { Hint = hint };
        edit.TextChanged += (sender, e) =>
        {
            string currentValue = new(e.Text?.ToArray());

            onTextChanged(currentValue);

            Console.WriteLine($"Text changed to: {currentValue}");
        };
        Layout.AddView(edit);
        return this;
    }
}
