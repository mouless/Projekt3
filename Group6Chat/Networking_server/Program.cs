using System;
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
                for (int i = 0; i < clients.Count; i++)
                {
                    if (!clients[i].tcpclient.Connected)
                    {
                        clients.RemoveAt(i);
                    }
                    if (clients[i] != client || clients[i] == client)
                    {
                        NetworkStream n = clients[i].tcpclient.GetStream();
                        BinaryWriter w = new BinaryWriter(n);
                        w.Write(message);
                        w.Flush();
                    }
                }
                
                //foreach (ClientHandler tmpClient in clients)
                //{
                //    if (tmpClient != client || tmpClient == client)
                //    {
                //        NetworkStream n = tmpClient.tcpclient.GetStream();
                //        BinaryWriter w = new BinaryWriter(n);
                //        w.Write(message);
                //        w.Flush();
                //    }
                //    //if (clients.Count() == 1)
                //    //{
                //    //    NetworkStream n = tmpClient.tcpclient.GetStream();
                //    //    BinaryWriter w = new BinaryWriter(n);
                //    //    w.Write("Sorry, no clients connected. You are alone...");
                //    //    w.Flush();
                //    //}
                //}
            }

            public void DisconnectClient(ClientHandler client)
            {
                clients.Remove(client);
                //Console.WriteLine("Client X has left the building...");
                //Broadcast(client, "Client X has left the building...");
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
                        User tempUser = JsonConvert.DeserializeObject<User>(message);

                        if (tempUser.TypeOfMessage == MessageType.UserName)
                        {
                            bool nameExists = Server.clients.FindAll(x => x.UserName == tempUser.UserName).Count() == 0;

                            if (nameExists)
                            {
                                this.UserName = tempUser.UserName;
                                Server.clients.Add(this);
                                NetworkStream nn = tcpclient.GetStream();
                                BinaryWriter w = new BinaryWriter(nn);
                                w.Write(message);
                                w.Flush();
                            }
                            else
                            {
                                tempUser.TypeOfMessage = MessageType.ErrorMessage;
                                message = JsonConvert.SerializeObject(tempUser);

                                NetworkStream nn = tcpclient.GetStream();
                                BinaryWriter w = new BinaryWriter(nn);
                                w.Write(message);
                                w.Flush();
                                myServer.DisconnectClient(this);
                                tcpclient.Close();
                                // Skicka tillbaka ett värde (bool???) för att kolla om UserName inte var unikt eller dyl...
                            }
                        }

                        message = JsonConvert.SerializeObject(tempUser);
                        if (tempUser.TypeOfMessage == MessageType.Message)
                        {
                            myServer.Broadcast(this, message);
                        }
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
