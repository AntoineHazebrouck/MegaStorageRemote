using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using MegaStorageRemote.Code.Data;

namespace MegaStorageRemote.Code.Presentation;

[Activity(Label = "Add a new CD")]
public class AddCdForm : Activity
{
    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        string artist = "";
        string album = "";
        string position = "";

        var layout = new LayoutBuilder(this)
            .AddText("Artist")
            .AddEditText("Artist name", value => artist = value)
            .AddText("Album")
            .AddEditText("Album name", value => album = value)
            .AddText("Position")
            .AddEditText("Cd position", value => position = value)
            .AddButton(
                "Submit",
                (s, e) =>
                {
                    if (
                        artist.IsWhiteSpace()
                        || album.IsWhiteSpace()
                        || int.Parse(position) < 1
                        || int.Parse(position) > 300
                    )
                    {
                        Toast.MakeText(this, $"Data was not valid", ToastLength.Long)?.Show();
                    }
                    else
                    {
                        var saved = SaveCd.Save(
                            new Cd(Artist: artist, Album: album, Position: int.Parse(position))
                        );
                        Toast
                            .MakeText(
                                this,
                                $"Saved : '{saved.Artist}' / '{saved.Album}' at position {saved.Position}",
                                ToastLength.Long
                            )
                            ?.Show();
                        Finish();
                    }
                }
            )
            .build();

        SetContentView(layout);

        ActionBar?.SetDisplayHomeAsUpEnabled(true);
    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
        if (item.ItemId == Android.Resource.Id.Home)
        {
            Finish();
            return true;
        }
        return base.OnOptionsItemSelected(item);
    }
}
