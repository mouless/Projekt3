﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Networking_server
{
    class Program
    {
        static void Main(string[] args)
        {
            Server myServer = new Server();
            Thread serverThread = new Thread(myServer.Run);
            serverThread.Start();
            serverThread.Join();
        }

        public class Server
        {
            public static List<ClientHandler> clients = new List<ClientHandler>();
            public void Run()
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 5000);
                Console.WriteLine("Server up and running, waiting for messages...");

                try
                {
                    listener.Start();

                    while (true)
                    {
                        TcpClient c = listener.AcceptTcpClient();
                        ClientHandler newClient = new ClientHandler(c, this);
                        //Måste lägga till username till newClient för att kunna kolla om det finns i listan.
                        //clients.Add(newClient); // Don't do this here...

                        Thread clientThread = new Thread(newClient.Run);
                        clientThread.Start();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    if (listener != null)
                        listener.Stop();
                }
            }

            public void Broadcast(ClientHandler client, string message)
            {
                foreach (ClientHandler tmpClient in clients)
                {
                    if (tmpClient != client || tmpClient == client)
                    {
                        NetworkStream n = tmpClient.tcpclient.GetStream();
                        BinaryWriter w = new BinaryWriter(n);
                        w.Write(message);
                        w.Flush();
                    }
                    if (clients.Count() == 1)
                    {
                        NetworkStream n = tmpClient.tcpclient.GetStream();
                        BinaryWriter w = new BinaryWriter(n);
                        w.Write("Sorry, no clients connected. You are alone...");
                        w.Flush();
                    }
                }
            }

            public void DisconnectClient(ClientHandler client)
            {
                clients.Remove(client);
                Console.WriteLine("Client X has left the building...");
                Broadcast(client, "Client X has left the building...");
            }
        }

        public class ClientHandler
        {
            public TcpClient tcpclient;
            private Server myServer;
            public string UserName { get; set; } // Kolla så att det finns unika användare

            public ClientHandler(TcpClient c, Server server)
            {
                tcpclient = c;
                this.myServer = server;
            }

            public void Run()
            {
                try
                {
                    string message = "";
                    //while (UserName != null)
                    //{
                    //    // Kolla så att UserName är unikt eller inte
                    //    NetworkStream n = tcpclient.GetStream();
                    //    message = new BinaryReader(n).ReadString();
                    //}
                    while (!message.Equals("quit"))
                    {
                        NetworkStream n = tcpclient.GetStream();
                        message = new BinaryReader(n).ReadString();
                        // de-serialize
                        User tempUser = JsonConvert.DeserializeObject<User>(message);
                        
                        if(tempUser.TypeOfMessage == 0) //username type.
                        {
                            foreach (ClientHandler newClient in Server.clients)
                            {
                                if(!(newClient.UserName == tempUser.UserName))
                                {
                                    this.UserName = tempUser.UserName;
                                    Server.clients.Add(this);
                                }
                            }
                        }
                        // serialize
                        message = JsonConvert.SerializeObject(tempUser);
                        // serialize
                        myServer.Broadcast(this, message);
                        Console.WriteLine(message);
                    }

                    myServer.DisconnectClient(this);
                    tcpclient.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
