using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Group6Chat
{
    public class Networking_client
    {
        public static TcpClient StartTheClient(Form1 form1)
        {
            Client myClient = new Client(form1);
            myClient.Start();

            //Thread clientThread = new Thread(myClient.Start);
            //clientThread.Start();

            return myClient.HostServer;
        }

        public class Client
        {
            Form1 form1;

            public Client(Form1 form1)
            {
                this.form1 = form1;
            }

            public TcpClient HostServer { get; set; }

            public void Start()
            {
                HostServer = new TcpClient("192.168.25.87", 5000);

                Thread listenerThread = new Thread(Listen);
                listenerThread.Start();

                //Thread senderThread = new Thread(Send);
                //senderThread.Start();

            }

            public void Listen()
            {
                string message = "";

                try
                {
                    while (true)
                    {
                        NetworkStream n = HostServer.GetStream();
                        message = new BinaryReader(n).ReadString();
                        //Console.WriteLine("Other: " + message);
                        form1.WriteToTextbox(message);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            //public void Send()
            //{
            //    string message = "";

            //    try
            //    {
            //        while (!message.Equals("quit"))
            //        {
            //            NetworkStream n = client.GetStream();

            //            message = Console.ReadLine();
            //            BinaryWriter w = new BinaryWriter(n);
            //            w.Write(message);
            //            w.Flush();
            //        }

            //        client.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        Console.WriteLine(ex.Message);
            //    }
            //}
        }
    }
}
