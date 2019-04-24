using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Net.Sockets;
using System.Net;

namespace TipServer
{
    class Program
    {
        // Holds our connection with the database
        SQLiteConnection m_dbConnection;

        public Program()
        {
            connectToDatabase(); 
        }

        // Creates a connection with our database file.
        void connectToDatabase()
        {
            m_dbConnection = new SQLiteConnection("Data Source=C:\\TipDB.db;Version=3;");
            m_dbConnection.Open();
        }

        void online2(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }

            string sql = "update Users set online = 1 where nick ='" + temp2 + "';";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        void online(string data)
        {
            string sql = "update Users set online = 1 where nick ='"+data+"';";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        void offline(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }

            string sql = "update Users set online = 0 where nick ='" + temp2 + "';";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        void datachange(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            string sql = "update Users set nick='";
            sql += temp2 + "', ";

            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            sql += "name='" + temp2 + "', ";

            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            sql += "sname='" + temp2 + "', ";

            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            sql += "password='" + temp2 + "' where nick = '";

            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            sql += temp2 + "';";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        string login(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";
            string temp3 = "";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            string sql = "select * from Users where login ='";
            sql += temp2 + "' and password = '";

            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            
            sql += temp2 + "';";
            temp = "0";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader["nick"].ToString() != null)
                {
                    temp = reader["nick"].ToString();
                    online(temp);
                }
            }
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp3;
                    temp3 = "";
                    break;
                }
                temp3 += data[i];

            }
            sql = "update Users set IP ='" + temp2 + "' where nick='" + temp + "';";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            return temp;

        }

        void createuser(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";
            for (int i=x; i < data.Length; i++)
            {
                if(data[i]==':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            string sql = "insert into Users (login, nick, name, sname, password) values ('";
            sql += temp2 + "', '";

            for (int z = 0; z < 3; z++)
            {
                for (int i = x; i < data.Length; i++)
                {
                    if (data[i] == ':')
                    {
                        x = i + 1;
                        temp2 = temp;
                        temp = "";
                        break;
                    }
                    temp += data[i];

                }
                sql += temp2 + "', '";

            }
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            sql += temp2 + "')";


            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            
        }

        void bldel(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";

            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            string sql = "delete from Blacklist where nick='" + temp2 + "' and nick2='";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            sql += temp2 + "';";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

        }

        void friendsdel(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";

            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            string sql = "delete from Contacts where nick='" + temp2 + "' and nick2='";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            sql += temp2 + "';";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();

        }

        string friendsoff(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            string sql = "select * from Contacts C inner join Users U on C.nick2 = U.nick where C.nick = '" + temp2 + "' and U.online = 0";
            temp = ":";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                if(reader["nick2"]!=null)
                temp += reader["nick2"].ToString() + ":";
            return temp;
        }

        string bl(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            string sql = "select * from Blacklist C inner join Users U on C.nick2 = U.nick where C.nick = '" + temp2 + "';";
            temp = "";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                temp += reader["nick2"].ToString() + ":";
            return temp;
        }
        string friendson(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            string sql = "select * from Contacts C inner join Users U on C.nick2 = U.nick where C.nick = '"+temp2+"' and U.online = 1";
            temp = "";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                temp+=reader["nick2"].ToString()+":";
            return temp;
        }
        void printOnline()
        {
            string sql = "select * from Users where online = 1";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
                Console.WriteLine("Name: " + reader["name"] + "\tSname: " + reader["sname"]);
            
        }

        void bladd(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }

            string sql = "insert into Blacklist (nick,nick2) values('" + temp2 + "', '";

            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            sql += temp2 + "');";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }
        void addcontact(string data)
        {
            int x = 0;
            string temp = "";
            string temp2 = "";
            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }

            string sql = "insert into Contacts (nick,nick2) values('" + temp2 + "', '";

            for (int i = x; i < data.Length; i++)
            {
                if (data[i] == ':')
                {
                    x = i + 1;
                    temp2 = temp;
                    temp = "";
                    break;
                }
                temp += data[i];

            }
            sql += temp2 + "');";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
        }

        void TCP()
        {
            TcpListener server = null; 
                server = new TcpListener(IPAddress.Any, 13000);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

            // Enter the listening loop.
            while (true)
            {
                printOnline();
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;
                string data2 = "";
                string data3 = "";
                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);
                    //data = data.ToUpper();

                    for (int x = 0; x < data.Length; x++)
                    {
                        if (x != 0)
                            data2 += data[x];
                        x++;
                    }
                    byte[] msg;
                    switch (data[0])
                    {
                        case '0':
                            data3 = login(data2);
                            msg= System.Text.Encoding.ASCII.GetBytes(data3);
                            stream.Write(msg, 0, msg.Length);
                            break;
                        case '1':
                            createuser(data2);
                            break;
                        case '2':
                            offline(data2);
                            break;
                        case '3':
                            online2(data2);
                            break;
                        case '4':
                            datachange(data2);
                            break;
                        case '5':
                            addcontact(data2);
                            break;
                        case '6':
                            data3 = friendson(data2);
                            msg = System.Text.Encoding.ASCII.GetBytes(data3);
                            stream.Write(msg, 0, msg.Length);
                            break;
                        case '7':
                            friendsdel(data2);
                            break;
                        case '8':
                            data3 = friendsoff(data2);
                            msg = System.Text.Encoding.ASCII.GetBytes(data3);
                            stream.Write(msg, 0, msg.Length);
                            break;
                        case '9':
                            bladd(data2);
                            break;
                        case '-':
                            bldel(data2);
                            break;
                        case '=':
                            data3 = bl(data2);
                            msg = System.Text.Encoding.ASCII.GetBytes(data3);
                            stream.Write(msg, 0, msg.Length);
                            break;
                    }



                    //byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    //Send back a response.
                    //stream.Write(msg, 0, msg.Length);
                    //Console.WriteLine("Sent: {0}", data3);
                }
                

                // Shutdown and end connection
                client.Close();

                }
           
         
        }

        static void Main(string[] args)
        {
            Program p = new Program();
            p.TCP();
  
        }
    }
}
