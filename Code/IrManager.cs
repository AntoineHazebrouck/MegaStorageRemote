using Android.Hardware;
using Android.Net.Wifi.P2p;

namespace MegaStorageRemote.Code;

public class IrManager(ConsumerIrManager ir)
{
    private void Send(IrCode signal)
    {
        const int frequency = 38000;

        ir.Transmit(frequency, signal);
    }

    public void Power()
    {
        // Pattern: [Pulse ON, Pulse OFF, Pulse ON, Pulse OFF...] in microseconds
        IrCode pattern =
        [
            2400,
            600, // Header
            1200,
            600, // Bit 1
            600,
            600, // Bit 0
            1200,
            600, // Bit 1
            600,
            600, // Bit 0
            1200,
            600, // Bit 1
            600,
            600, // Bit 0
            600,
            600, // Bit 0
            1200,
            600, // Bit 1
            600,
            600, // Bit 0
            600,
            600, // Bit 0
            600,
            600, // Bit 0
            1200, // Dernier pulse (fin du 12ème bit)
        ];

        Send(pattern);
    }

    public void PlayDisc(int number)
    {
        if (number < 1 || number > 300)
            throw new ArgumentException("number has to be between 1 and 300");

        var mappings = new Dictionary<int, IrCode>
        {
            [0] = IrCodes.Zero,
            [1] = IrCodes.One,
            [2] = IrCodes.Two,
            [3] = IrCodes.Three,
            [4] = IrCodes.Four,
            [5] = IrCodes.Five,
            [6] = IrCodes.Six,
            [7] = IrCodes.Seven,
            [8] = IrCodes.Eight,
            [9] = IrCodes.Nine,
        };

        var codesChain = number
            .ToString()
            .Select(character => mappings[int.Parse(character.ToString())])
            .Prepend(IrCodes.Disc)
            .Append(IrCodes.Enter)
            .Append(IrCodes.Play)
            .ToList();

        foreach (var code in codesChain)
        {
            Send(code);
            Thread.Sleep(500);
        }
    }
}
