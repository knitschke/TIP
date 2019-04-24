using System;
using System.Net;
using System.Net.Sockets;

namespace KomunikatorTIP
{
    class UdpAudioSender : IAudioSender
    {
        private readonly UdpClient udpSender;
        public UdpAudioSender(IPEndPoint endPoint)
        {
            udpSender = new UdpClient();
            udpSender.ExclusiveAddressUse = false;
            udpSender.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpSender.Client.Bind(new IPEndPoint(IPAddress.Any, 8080));
            udpSender.Connect(new IPEndPoint (endPoint.Address, 8080));
        }

        public void Send(byte[] payload)
        {
            Console.WriteLine("Sending...");
            udpSender.Send(payload, payload.Length);
            Console.WriteLine("Sent.");
        }

        public void Dispose()
        {
            udpSender?.Close();
        }
    }
}