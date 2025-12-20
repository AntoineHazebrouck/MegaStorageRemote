using System;
using Android.Content;
using Android.Views;

namespace MegaStorageRemote.Code.Presentation;

public class CdAdapter(Context context, List<Cd> data) : BaseAdapter<Cd>
{
    public override Cd this[int position] => data[position];

    public override int Count => data.Count;

    public override long GetItemId(int position) => this[position].Position;

    public override View? GetView(int position, View? convertView, ViewGroup? parent)
    {
        return new CdCard(context, this[position]);
    }
}
