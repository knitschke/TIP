using System;

namespace KomunikatorTIP
{
    interface IAudioSender : IDisposable
    {
        void Send(byte[] payload);
    }
}