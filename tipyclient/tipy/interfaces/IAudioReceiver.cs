using System;

namespace KomunikatorTIP
{
    interface IAudioReceiver : IDisposable
    {
        void OnReceived(Action<byte[]> handler);
    }
}