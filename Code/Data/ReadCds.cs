using System;
using MegaStorageRemote.Code.Utils;

namespace MegaStorageRemote.Code.Data;

public class ReadCds
{
    public static List<Cd> Read()
    {
        var prefs = AndroidUtils.Preferences();

        List<Cd> data = [];
        for (int index = 1; index <= 300; index++)
        {
            var record = prefs.GetString(index.ToString(), null);

            if (record is null)
            {
                data.Add(new Cd("...", "...", index));
            }
            else
            {
                var parsed = JsonUtils.Parse(record);
                data.Add(parsed);
            }
        }
        return data;
    }
}
