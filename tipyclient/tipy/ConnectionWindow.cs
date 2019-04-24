using KomunikatorTIP;
using KomunikatorTIP.comunicates;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace tipy

{
    public partial class ConnectionWindow : Form
    {

        public ConnectionWindow()
        {
            TcpReceiver(8082);
            InitializeComponent();
        }

        private bool mouseDown;
        private Point lastLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        private TcpSender tcpSender;
        public Invite invite;
        public Accept accept;
        public Decline decline;
        public Bye bye;
        private INetworkChatCodec selectedCodec = new UltraWideBandSpeexCodec();
        private volatile bool connected;
        private NetworkAudioPlayer player;
        private NetworkAudioSender audioSender;

        public delegate void UpdateVisibilityCallback(bool visible, bool toplevel);
        private void UpdateVisibility(bool visible, bool toplevel)
        {
            Visible = visible;
            TopLevel = toplevel;
        }
        public void UpdateUI(bool buttonOdbierz, bool buttonOdrzuc, string text)
        {
            textBoxLog.Invoke(new UpdateTextCallback(UpdateText), new object[] { text });
            Button_Odbierz.Invoke(new UpdateOdbierzButton(UpdateOdbierz), new object[] { buttonOdbierz });
            Button_Odrzuc.Invoke(new UpdateOdrzucButton(UpdateOdrzuc), new object[] { buttonOdrzuc });
        }
        public void OnInviteDo(Invite inviteCom)
        {
            invite = (Invite)inviteCom;
            textBoxLog.Invoke(new UpdateTextCallback(UpdateText), new object[] { invite.RequestedBy + " dzwoni." });
            Button_Odbierz.Invoke(new UpdateOdbierzButton(UpdateOdbierz), new object[] { true });
            Button_Odrzuc.Invoke(new UpdateOdrzucButton(UpdateOdrzuc), new object[] { true });
            Invoke(new UpdateVisibilityCallback(UpdateVisibility), new object[] { true, true });
        }
        public void OnAcceptDo(Accept acceptCom)
        {
            accept = (Accept)acceptCom;
            textBoxLog.Invoke(new UpdateTextCallback(UpdateText), new object[] { accept.CallAcceptedBy + " przyjęła połączenie." });
            textBoxLog.Invoke(new UpdateTextCallback(UpdateText), new object[] { "Trwa połączenie z " + accept.CallAcceptedBy });
            Button_Odbierz.Invoke(new UpdateOdbierzButton(UpdateOdbierz), new object[] { false });
            Button_Odrzuc.Invoke(new UpdateOdrzucButton(UpdateOdrzuc), new object[] { false });
            Invoke(new TryStreaming(StreamIt));

        }
        public void OnDeclineDo(Decline declineCom)
        {
            decline = (Decline)declineCom;
            textBoxLog.Invoke(new UpdateTextCallback(UpdateText), new object[] { decline.CallDeclinedBy + " odrzucił(a) połączenie." });
            Button_Odbierz.Invoke(new UpdateOdbierzButton(UpdateOdbierz), new object[] { false });
            Button_Odrzuc.Invoke(new UpdateOdrzucButton(UpdateOdrzuc), new object[] { false });
            Invoke(new UpdateVisibilityCallback(UpdateVisibility), new object[] { false, false });
            Invoke(new DisconnectCallback(Disconnect));
            //Invoke(new CloseAll(CloseApplication));
        }
        public void OnByeDo(Bye byeCom)
        {
            bye = (Bye)byeCom;
            //textBoxLog.Invoke(new UpdateTextCallback(UpdateText), new object[] { bye.ByeSentBy + " zakończył(a) połączenie." });
            //Invoke(new TryStreaming(StreamIt));
            //Invoke(new CloseAll(CloseApplication));

            Invoke(new UpdateVisibilityCallback(UpdateVisibility), new object[] { false, false });
            Invoke(new DisconnectCallback(Disconnect));
        }

        public delegate void TryStreaming();
        private void Streaming()
        {
            StreamIt();
        }
        private void Connect(IPEndPoint endPoint, int inputDeviceNumber, INetworkChatCodec codec)
        {
            var receiver = (IAudioReceiver)new UdpAudioReceiver(endPoint);
            var sender = (IAudioSender)new UdpAudioSender(endPoint);

            player = new NetworkAudioPlayer(codec, receiver);
            audioSender = new NetworkAudioSender(codec, inputDeviceNumber, sender);
            connected = true;
        }
        private void Disconnect()
        {
            if (connected)
            {
                connected = false;

                player.Dispose();
                audioSender.Dispose();
                selectedCodec.Dispose();
            }
        }
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
        public void StreamIt()
        {
            if (!connected)
            {
                if (invite == null && accept == null) Invoke(new DisconnectCallback(Disconnect));
                else
                {
                    IPEndPoint endPoint;
                    if (invite == null)
                    {
                        endPoint = new IPEndPoint(IPAddress.Parse(accept.IP_CallAcceptedBy), 8080);
                    }
                    else
                    {
                        endPoint = new IPEndPoint(IPAddress.Parse(invite.IP_RequestedBy), 8080);
                    }
                    int inputDeviceNumber = 0;
                    Connect(endPoint, inputDeviceNumber, selectedCodec);
                }
            }
            else
            {
                Invoke(new DisconnectCallback(Disconnect));
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            tcpSender = new TcpSender(new IPEndPoint(IPAddress.Parse(invite.IP_RequestedBy), 8082));
            IComunicates sendAccept = new Accept()
            {
                CallAcceptedBy = "Odbierajacy",
                IP_CallAcceptedBy = GetLocalIPv4(NetworkInterfaceType.Wireless80211),
                CallAcceptedFrom = invite.RequestedBy,
                IP_CallAcceptedFrom = invite.IP_RequestedBy
            };
            tcpSender.Send(sendAccept);
            Button_Odbierz.Invoke(new UpdateOdbierzButton(UpdateOdbierz), new object[] { false });
            Button_Odrzuc.Invoke(new UpdateOdrzucButton(UpdateOdrzuc), new object[] { false });
            Invoke(new TryStreaming(StreamIt));
        }


        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            // functions.logout();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            datachange d = new datachange();
            d.Show();
            this.Hide();
        }

        public delegate void DisconnectCallback();

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                if (invite == null)
                {
                    tcpSender = new TcpSender(new IPEndPoint(IPAddress.Parse(accept.IP_CallAcceptedFrom), 8082));
                }
                else
                {
                    tcpSender = new TcpSender(new IPEndPoint(IPAddress.Parse(invite.IP_RequestedBy), 8082));
                }
                IComunicates sendBye = new Bye
                {
                    ByeSentBy = invite == null ? accept.CallAcceptedBy : invite.CalledUser,
                    ByeSentTo = invite == null ? accept.CallAcceptedFrom : invite.RequestedBy
                };
                using (tcpSender)
                {
                    tcpSender.Send(sendBye);
                }
                Invoke(new DisconnectCallback(Disconnect));
                Invoke(new UpdateVisibilityCallback(UpdateVisibility), new object[] { false, false });
            }
            catch (Exception)
            {

            }
            //Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            functions.blist();
            blacklist f = new blacklist();
            f.Show();
            this.Hide();
        }

        private void logged_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            functions.online();
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            functions.logout();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tcpSender = new TcpSender(new IPEndPoint(IPAddress.Parse(invite.IP_RequestedBy), 8082));
            IComunicates sendDecline = new Decline()
            {
                CallDeclinedBy = "Odrzucajacy",
                IP_CallDeclinedBy = GetLocalIPv4(NetworkInterfaceType.Wireless80211),
                CallDeclinedFrom = invite.RequestedBy,
                IP_CallDeclinedFrom = invite.IP_RequestedBy
            };
            using (tcpSender)
            {
                tcpSender.Send(sendDecline);
            }

            Invoke(new DisconnectCallback(Disconnect));
            Invoke(new UpdateVisibilityCallback(UpdateVisibility), new object[] { false, false });
        }
        public delegate void UpdateTextCallback(string message);
        private void UpdateText(string message)
        {
            textBoxLog.Text += (message + "\n");
        }

        public delegate void UpdateOdbierzButton(bool state);
        private void UpdateOdbierz(bool state)
        {
            Button_Odbierz.Enabled = state;
        }

        public delegate void UpdateOdrzucButton(bool state);
        private void UpdateOdrzuc(bool state)
        {
            Button_Odrzuc.Enabled = state;
        }

        public delegate void CloseAll();
        private void CloseApplication()
        {
            Close();
        }

        private IComunicates comuniacate;

        private TcpListener tcpListener;
        private bool listeningTcp;
        private BinaryFormatter bFormatter;

        public void TcpReceiver(int portNumber)
        {
            try
            {
                var endPoint = new IPEndPoint(IPAddress.Parse(GetLocalIPv4(NetworkInterfaceType.Wireless80211)), portNumber);
                tcpListener = new TcpListener(endPoint);
                tcpListener.Start();
                listeningTcp = true;
                bFormatter = new BinaryFormatter();
                ThreadStart threadWindowDelegate = new ThreadStart(ListenerWindowThread);
                Thread newWindowThread = new Thread(threadWindowDelegate);
                newWindowThread.Start();
                //ThreadPool.QueueUserWorkItem(ListenerWindowThread, null);
            }
            catch (Exception)
            {
                Thread.ResetAbort();
                tcpListener.Server.Close();
            }
        }
        private void ListenerWindowThread()
        {
            try
            {
                while (listeningTcp)
                {
                    using (var client = tcpListener.AcceptTcpClient())
                    {
                        comuniacate = (IComunicates)bFormatter.Deserialize(client.GetStream());
                        var c = comuniacate.Code;
                        //Obsługa komunikatu invite. Czyli co się dzieje kiedy klient otrzyma komunikat invite.
                        if (c == 0)
                        {
                            invite = (Invite)comuniacate;
                            OnInviteDo(invite);
                        }
                        if (c == 1)
                        {
                            accept = (Accept)comuniacate;
                            OnAcceptDo((Accept)comuniacate);
                            
                        }
                        else if (c == 2)
                        {

                            decline = (Decline)comuniacate;
                            OnDeclineDo((Decline)comuniacate);
 
                        }
                        else if (c == 3)
                        {

                            bye = (Bye)comuniacate;
                            OnByeDo((Bye)comuniacate);

                        }
                    }
                }
            }
            catch (SocketException)
            {
                Thread.ResetAbort();
                tcpListener.Server.Close();
            }
            catch (System.IO.IOException ex)
            {
                Thread.ResetAbort();
                tcpListener.Server.Close();
                //MessageBox.Show("" + ex);
            }
            catch (Exception)
            {
                Thread.ResetAbort();
                tcpListener.Server.Close();
            }
        }

        private void ConnectionWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            Dispose();
            //Disconnect();
            //Show();
            //Invoke(new UpdateVisibilityCallback(UpdateVisibility), new object[] { false, false });
        }

        private void ConnectionWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
