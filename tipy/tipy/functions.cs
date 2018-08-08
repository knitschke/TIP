using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Windows;

namespace tipy
{
    static class functions
    {
        public static string nick;
        public static string ip;
        public static List<string> on;
        public static List<string> off;
        public static List<string> bllist;
        public static string target;
        public static void connect_IP(string a) { ip = a;}
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
        public static void connect(string a) { ip = a; }

        public static void register(string log, string nick, string name, string sname, string passw) {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "1"+log+":" +nick+ ":"+name+ ":"+sname+ ":"+passw+":";//Console.ReadLine();
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            client.Close();
        }

        public static void datachange(string nick, string name, string sname, string passw)
        {
            
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "4" + nick + ":" + name + ":" + sname + ":" + passw + ":" + functions.nick + ":";//Console.ReadLine();
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            client.Close();
        }

        public static string log(string log,string pass)
        {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "0" + log + ":" + pass + ":"+GetLocalIPAddress()+":";
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            Byte[] data = new Byte[256];
            Int32 bytes = ns.Read(data, 0, data.Length);
            client.Close();
            string resp= System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine(resp);
            return resp;
        }

        public static void addblist(string d)
        {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "9" + functions.nick + ":" + d + ":";
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            client.Close();
        }

        public static void blistdelete(string data)
        {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "-" + functions.nick + ":" + data + ":";
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            client.Close();

        }

        public static void blist()
        {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "=" + functions.nick + ":";
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            Byte[] data = new Byte[256];
            Int32 bytes = ns.Read(data, 0, data.Length);
            client.Close();
            string resp = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine(resp);
            string temp = "";
            string temp2 = "";
            functions.bllist = new List<string>();
            for (int i = 0; i < resp.Length; i++)
            {
                if (resp[i] == ':')
                {

                    temp2 = temp;
                    temp = "";
                    bllist.Add(temp2);
                }
                else
                    temp += resp[i];

            }


        }

        public static void addcontact(string d)
        {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "5" + functions.nick + ":"+d+":";
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            client.Close();
        }

        public static void logout()
        {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "2" + functions.nick + ":"; 
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            client.Close();
        }
        public static void online()
        {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "3" + functions.nick + ":";
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            client.Close();
        }

        public static void frienddelete(string data)
        {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "7" + functions.nick + ":"+data+":";
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            client.Close();

        }

        public static void friendson()
        {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "6" + functions.nick + ":";
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            Byte[] data = new Byte[256];
            Int32 bytes = ns.Read(data, 0, data.Length);
            client.Close();
            string resp = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine(resp);
            string temp="";
            string temp2="";
            functions.on = new List<string>();
            for (int i = 0; i < resp.Length; i++)
            {
                if (resp[i] == ':')
                {
                    
                    temp2 = temp;
                    temp = "";
                    on.Add(temp2);
                }
                else
                temp += resp[i];

            }


        }

        public static void friendsoff()
        {
            TcpClient client = new TcpClient(ip, 13000);
            NetworkStream ns = client.GetStream();
            string ch = "8" + functions.nick + ":";
            byte[] message = Encoding.Unicode.GetBytes(ch);
            ns.Write(message, 0, message.Length);
            Byte[] data = new Byte[256];
            Int32 bytes = ns.Read(data, 0, data.Length);
            client.Close();
            string resp = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
            Console.WriteLine(resp);
            string temp = "";
            string temp2 = "";
            functions.off = new List<string>();
            for (int i = 0; i < resp.Length; i++)
            {
                if (resp[i] == ':')
                {

                    temp2 = temp;
                    temp = "";
                    if(temp2!="")
                    off.Add(temp2);
                }
                else
                    temp += resp[i];

            }


        }

    }
}
