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
    public partial class logged : Form
    {
        ConnectionWindow connectionWindow;
        datachange datachange;
        list list;
        blacklist blacklist;
        DialogResult dr;
        public logged()
        {
            InitializeComponent();

            connectionWindow = new ConnectionWindow();
            connectionWindow.Show();
            connectionWindow.Visible = false;

            functions.friendson();
            functions.friendsoff();
            list = new list();
            list.Show();
            list.Visible = false;

            datachange = new datachange();
            datachange.Show();
            datachange.Visible = false;

            functions.blist();
            blacklist = new blacklist();
            blacklist.Show();
            blacklist.Visible = false;

        }

        private TcpSender tcpSender;

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (list.Visible == true)
                {

                    tcpSender = new TcpSender(new IPEndPoint(IPAddress.Parse(list.GetIP()), 8082));
                    IComunicates sendInvite = new Invite
                    {
                        RequestedBy = "Dzwoniący", //Do zmiany
                        IP_RequestedBy = GetLocalIPv4(NetworkInterfaceType.Wireless80211),
                        CalledUser = "Odbierający", // Do zmiany
                        IP_CalledUser = list.GetIP()
                    };
                    tcpSender.Send(sendInvite);
                    var text = "Dzwonię do " + ((Invite)sendInvite).CalledUser + "...";
                    connectionWindow.Visible = true;
                    connectionWindow.TopLevel = true;
                    connectionWindow.UpdateUI(false, false, text);
                    
                }
                else
                {
                    tcpSender = new TcpSender(new IPEndPoint(IPAddress.Parse(textBox1.Text), 8082));
                    IComunicates sendInvite = new Invite
                    {
                        RequestedBy = "Dzwoniący", //Do zmiany
                        IP_RequestedBy = GetLocalIPv4(NetworkInterfaceType.Wireless80211),
                        CalledUser = "Odbierający", // Do zmiany
                        IP_CalledUser = textBox1.Text
                    };

                    tcpSender.Send(sendInvite);

                    var text = "Dzwonię do " + ((Invite)sendInvite).CalledUser + "...";
                    connectionWindow.Visible = true;
                    connectionWindow.TopLevel = true;
                    connectionWindow.UpdateUI(false, false, text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Wystąpił błąd podczas próby wykonania połączenia.\n" + ex);
            }

            

            /*Thread newWindowThread = new Thread(new ThreadStart(() =>
            {
                // create and show the window
                ConnectionWindow window = new ConnectionWindow();
                window.Show();

                // start the Dispatcher processing  
                System.Windows.Threading.Dispatcher.Run();
            }));

            // set the apartment state  
            newWindowThread.SetApartmentState(ApartmentState.STA);

            // make the thread a background thread  
            newWindowThread.IsBackground = true;

            // start the thread  
            newWindowThread.Start();
            */
            //Thread t = new Thread(() => WindowThread());
            //t.SetApartmentState(ApartmentState.STA);
            //MessageBox.Show(""+t.GetApartmentState());
            //t.SetApartmentState(ApartmentState.STA);
            //t.Start();

            // Thread otherWindowHostingThread = new Thread(WindowThread);
            // otherWindowHostingThread.SetApartmentState(ApartmentState.STA);
            // otherWindowHostingThread.Start();

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
            if (datachange.IsDisposed == true)
            {
                datachange = new datachange();
                datachange.Show();
            }
            else
            {
                if (datachange.Visible == true) datachange.Visible = false;
                else datachange.Visible = true;
            }

        }

        private void button7_Click(object sender, EventArgs e)
        {
            functions.logout();
            Form1 f = new Form1();
            f.Show();
            Dispose();
            Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
            if (blacklist.IsDisposed == true)
            {
                functions.blist();
                blacklist = new blacklist();
                blacklist.Show();
            }
            else
            {
                if (blacklist.Visible == true) blacklist.Visible = false;
                else blacklist.Visible = true;
            }
        }

        private void logged_Load(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (list.IsDisposed == true)
            {
                functions.friendson();
                functions.friendsoff();
                list = new list();
                list.Show();
            }
            else
            {
                if (list.Visible == true) list.Visible = false;
                else list.Visible = true;
            }
        }

        private void radioButton1_MouseClick(object sender, MouseEventArgs e)
        {
            functions.online();
        }

        private void radioButton2_MouseClick(object sender, MouseEventArgs e)
        {
            functions.logout();
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

        private void logged_FormClosing(object sender, FormClosingEventArgs e)
        {
            //connectionWindow.Dispose();
            connectionWindow.Close();

            //list.Dispose();
            list.Close();

            //blacklist.Dispose();
            blacklist.Close();

            //datachange.Dispose();
            datachange.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
