using System;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace KomunikatorTIP
{
    class TcpSender : IDisposable
    {
        private readonly TcpClient tcpSender;
        private BinaryFormatter bFormatter;
        public TcpSender(IPEndPoint endPoint)
        {
            tcpSender = new TcpClient();
            bFormatter = new BinaryFormatter();
            tcpSender.Connect(endPoint);
        }

        public void Send(IComunicates comunicate)
        {
            bFormatter.Serialize(tcpSender.GetStream(), comunicate);
        }
        public void Dispose()
        {
            tcpSender?.Close();
        }
    }
}