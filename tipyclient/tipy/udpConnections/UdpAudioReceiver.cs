using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace KomunikatorTIP
{
    class UdpAudioReceiver : IAudioReceiver
    {
        private Action<byte[]> handler;
        private readonly UdpClient udpListener;
        public const int SIO_UDP_CONNRESET = -1744830452;
        private bool listening;
        IPEndPoint enP;

        public UdpAudioReceiver(IPEndPoint endPt)
        {
            udpListener = new UdpClient();
            udpListener.ExclusiveAddressUse = false;
            udpListener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            udpListener.Client.Bind(new IPEndPoint(IPAddress.Any, 8080));
            enP = endPt;

            udpListener.Client.IOControl(
            (IOControlCode)SIO_UDP_CONNRESET,
            new byte[] { 0, 0, 0, 0 },
             null
               );
            // To allow us to talk to ourselves for test purposes:
            // http://stackoverflow.com/questions/687868/sending-and-receiving-udp-packets-between-two-programs-on-the-same-computer
            //udpListener.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            //udpListener.Client.Bind(RemoteIpEndPoint);

            ThreadStart threadDelegate = new ThreadStart(ListenerThread);
            Thread newThread = new Thread(threadDelegate);
            newThread.Start();
            //ThreadPool.QueueUserWorkItem(ListenerThread, endPt);
            listening = true;
        }

        private void ListenerThread()
        {
            var endPoint = enP;
            Console.WriteLine("Listening... : " +endPoint );
            try
            {
                while (listening)
                {
                    Console.WriteLine("Listening...");
                    byte[] b = udpListener.Receive(ref endPoint);
                    handler?.Invoke(b);
                }
            }
            catch (SocketException ex)
            {
                udpListener.Dispose();
                Dispose();

            }
        }

        public void Dispose()
        {
            listening = false;
            udpListener.Close();
            udpListener?.Close();
            
        }

        public void OnReceived(Action<byte[]> onAudioReceivedAction)
        {
            Console.WriteLine("Revieced");
            handler = onAudioReceivedAction;
            Console.WriteLine("Revieced");
        }
    }
}