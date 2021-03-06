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
            public void PrivateMessage(string message, string receiver)
            {
                ClientHandler tmpClient = clients.Find(x => x.UserName == receiver);
                if (tmpClient != null)
                {
                    NetworkStream n = tmpClient.tcpclient.GetStream();
                    BinaryWriter w = new BinaryWriter(n);
                    w.Write(message);
                    w.Flush();
                }
            }

            public void Broadcast(ClientHandler client, string message)
            {
                //for (int i = 0; i < clients.Count; i++)
                //{
                //    if (!clients[i].tcpclient.Connected)
                //    {
                //        clients.RemoveAt(i);
                //        // Skicka en uppdaterad lista med de clienter som fortfarande är online (minus den som blev removed)...
                //    }
                //}
                Server.ListUsers();

                foreach (ClientHandler tmpClient in clients)
                {
                    if (tmpClient != client || tmpClient == client)
                    {
                        NetworkStream n = tmpClient.tcpclient.GetStream();
                        BinaryWriter w = new BinaryWriter(n);
                        w.Write(message);
                        w.Flush();
                    }
                    //if (clients.Count() == 1)
                    //{
                    //    NetworkStream n = tmpClient.tcpclient.GetStream();
                    //    BinaryWriter w = new BinaryWriter(n);
                    //    w.Write("Sorry, no clients connected. You are alone...");
                    //    w.Flush();
                    //}
                }
            }
            public static void ListUsers()
            {
                string listUser = "";
                for (int i = 0; i < clients.Count; i++)
                {
                    if (!clients[i].tcpclient.Connected)
                    {
                        clients.RemoveAt(i);
                        // Skicka en uppdaterad lista med de clienter som fortfarande är online (minus den som blev removed)...
                    }
                    else
                    {
                        listUser += clients[i].UserName + ';';
                    }
                }
                User user2 = new User();
                user2.TypeOfMessage = MessageType.UserList;
                user2.Message = listUser;
                user2.UserName = "PHANTOM USER";

                foreach (ClientHandler tmpClient in clients)
                {
                    NetworkStream n = tmpClient.tcpclient.GetStream();
                    BinaryWriter w = new BinaryWriter(n);
                    w.Write(User.ToJson(user2));
                    w.Flush();
                }
            }
            public void DisconnectClient(ClientHandler client)
            {
                clients.Remove(client);
                Server.ListUsers();
                //Console.WriteLine("Client X has left the building...");
                //Broadcast(client, "Client X has left the building...");
            }
        }

        public class ClientHandler
        {
            public TcpClient tcpclient;
            private Server myServer;
            public string UserName { get; set; } // Kolla så att det finns unika användare
            public bool IsOnline { get; set; }

            public ClientHandler(TcpClient c, Server server)
            {
                tcpclient = c;
                this.myServer = server;
                IsOnline = true;
            }

            public void Run()
            {
                try
                {
                    // Skriva ut "quit" när någon användare har stängt av programmet
                    bool koll = true;
                    while (koll)
                    {
                        NetworkStream n = tcpclient.GetStream();
                        string message = new BinaryReader(n).ReadString();
                        User tempUser = JsonConvert.DeserializeObject<User>(message);
                        if (tempUser.TypeOfMessage == MessageType.Quit)
                        {
                            koll = false;
                        }

                        if (tempUser.TypeOfMessage == MessageType.UserName)
                        {
                            bool nameDoesNotExists = Server.clients.FindAll(x => x.UserName == tempUser.UserName).Count() == 0;

                            if (nameDoesNotExists)
                            {
                                this.UserName = tempUser.UserName;
                                Server.clients.Add(this);
                                Server.ListUsers();
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
                        if (tempUser.TypeOfMessage == MessageType.PrivateMessage)
                        {
                            message = JsonConvert.SerializeObject(tempUser);
                            myServer.PrivateMessage(message, tempUser.Receiver);
                        }

                        //message = JsonConvert.SerializeObject(tempUser);
                        if (tempUser.TypeOfMessage == MessageType.Message)
                        {
                            User tmpUser = InvokeProfanityFilter(tempUser);
                            message = JsonConvert.SerializeObject(tmpUser);
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

            private User InvokeProfanityFilter(User tempUser)
            {
                tempUser.Message = tempUser.Message.Replace("tottenham", "I love Arsenal");
                tempUser.Message = tempUser.Message.Replace("Tottenham", "I love Arsenal");
                tempUser.Message = tempUser.Message.Replace("Hotspurs", "I love Arsenal");
                tempUser.Message = tempUser.Message.Replace("hotspurs", "I love Arsenal");
                tempUser.Message = tempUser.Message.Replace("ManU", "I love Arsenal");
                tempUser.Message = tempUser.Message.Replace("kuk", "***");
                tempUser.Message = tempUser.Message.Replace("korv", "Borg");
                tempUser.Message = tempUser.Message.Replace("Korv", "Borg");
                tempUser.Message = tempUser.Message.Replace("idiot", "***");
                tempUser.Message = tempUser.Message.Replace("jävla", "***");
                tempUser.Message = tempUser.Message.Replace("mullbänk", "***");
                tempUser.Message = tempUser.Message.Replace("Mullbänk", "***");
                tempUser.Message = tempUser.Message.Replace("JavaScript", "C# IS KING!");
                tempUser.Message = tempUser.Message.Replace("javaScript", "C# RULES!");
                tempUser.Message = tempUser.Message.Replace("javascript", "C# RULES!");
                tempUser.Message = tempUser.Message.Replace("Java", "I LOVE C#");
                tempUser.Message = tempUser.Message.Replace("java", "I LOVE C#");
                tempUser.Message = tempUser.Message.Replace("<", "nice try, injection alert ");
                tempUser.Message = tempUser.Message.Replace(">", "nice try, injection alert ");
                tempUser.Message = tempUser.Message.Replace("{", "nice try, injection alert ");
                tempUser.Message = tempUser.Message.Replace("}", "nice try, injection alert ");
                tempUser.Message = tempUser.Message.Replace("/", "nice try, injection alert ");
                tempUser.Message = tempUser.Message.Replace(@"\", "nice try, injection alert ");
                if (tempUser.Message.Contains("nice try, injection alert "))
                {
                    tempUser.Message = "nice try, injection alert ";
                }
                return tempUser;
            }
        }
    }
}
