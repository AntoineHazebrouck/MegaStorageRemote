using System;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using MegaStorageRemote.Code.Utils;

namespace MegaStorageRemote.Code.Presentation;

public class CdCard : FrameLayout
{
    public CdCard(Context context, Cd cd)
        : base(context)
    {
        int padding = 30;
        SetPadding(padding, padding, padding, padding);

        var bg = new GradientDrawable();
        bg.SetColor(Color.White);
        Background = bg;

        var container = new LinearLayout(context)
        {
            Orientation = Orientation.Horizontal,
            LayoutParameters = new LayoutParams(
                ViewGroup.LayoutParams.MatchParent,
                ViewGroup.LayoutParams.WrapContent
            ),
        };

        var col = new LinearLayout(context)
        {
            Orientation = Orientation.Vertical,
            LayoutParameters = new LinearLayout.LayoutParams(0, ViewGroup.LayoutParams.WrapContent)
            {
                Weight = 1f,
            },
        };

        var play = PlayButton(context, cd);
        container.AddView(play);

        TextView artistLabel = ArtistLabel(context, cd);
        col.AddView(artistLabel);

        TextView albumLabel = AlbumLabel(context, cd);
        col.AddView(albumLabel);

        container.AddView(col);

        TextView positionLabel = PositionLabel(context, cd);
        container.AddView(positionLabel);

        AddView(container);
    }

    private ImageButton PlayButton(Context context, Cd cd)
    {
        var button = new ImageButton(context);
        var layoutParams = new LayoutParams(
            ViewGroup.LayoutParams.WrapContent,
            ViewGroup.LayoutParams.WrapContent
        )
        {
            Gravity = GravityFlags.CenterVertical,
        };
        layoutParams.SetMargins(20, 0, 20, 0);
        button.LayoutParameters = layoutParams;

        button.SetImageResource(Android.Resource.Drawable.IcMediaPlay);
        button.SetScaleType(ImageView.ScaleType.CenterInside);

        button.Click += (s, e) =>
        {
            var infrared = AndroidUtils.Infrared(context);
            infrared.PlayCd(cd.Position);
        };

        return button;
    }

    private static TextView ArtistLabel(Context context, Cd cd)
    {
        var artistLabel = new TextView(context) { Text = cd.Artist };
        artistLabel.SetTextSize(ComplexUnitType.Sp, 16);
        artistLabel.SetTypeface(artistLabel.Typeface, TypefaceStyle.Bold);
        artistLabel.SetTextColor(Color.Black);
        return artistLabel;
    }

    private static TextView AlbumLabel(Context context, Cd cd)
    {
        var albumLabel = new TextView(context) { Text = cd.Album };
        albumLabel.SetTextSize(ComplexUnitType.Sp, 14);
        albumLabel.SetTextColor(Color.ParseColor("#FF666666"));
        return albumLabel;
    }

    private static TextView PositionLabel(Context context, Cd cd)
    {
        var positionLabel = new TextView(context) { Text = cd.Position.ToString() };
        positionLabel.SetTextSize(ComplexUnitType.Sp, 14);
        positionLabel.SetTextColor(Color.ParseColor("#FF666666"));
        positionLabel.LayoutParameters = new LinearLayout.LayoutParams(
            ViewGroup.LayoutParams.WrapContent,
            ViewGroup.LayoutParams.WrapContent
        )
        {
            Gravity = GravityFlags.CenterVertical,
        };
        return positionLabel;
    }
}
