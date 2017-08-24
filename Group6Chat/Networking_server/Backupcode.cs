//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Networking_server
//{
//    class Backupcode
//    {
//        public void Run()
//        {
//            try
//            {
//                string message = "";
//                // Skriva ut "quit" när någon användare har stängt av programmet

//                while (!message.Equals("quit"))
//                {
//                    NetworkStream n = tcpclient.GetStream();
//                    message = new BinaryReader(n).ReadString();
//                    User tempUser = JsonConvert.DeserializeObject<User>(message);

//                    if (tempUser.TypeOfMessage == MessageType.UserName)
//                    {
//                        bool nameDoesNotExists = Server.clients.FindAll(x => x.UserName == tempUser.UserName).Count() == 0;

//                        if (nameDoesNotExists)
//                        {
//                            this.UserName = tempUser.UserName;
//                            Server.clients.Add(this);
//                            Server.ListUsers();
//                            NetworkStream nn = tcpclient.GetStream();
//                            BinaryWriter w = new BinaryWriter(nn);
//                            w.Write(message);
//                            w.Flush();
//                        }
//                        else
//                        {
//                            tempUser.TypeOfMessage = MessageType.ErrorMessage;
//                            message = JsonConvert.SerializeObject(tempUser);

//                            NetworkStream nn = tcpclient.GetStream();
//                            BinaryWriter w = new BinaryWriter(nn);
//                            w.Write(message);
//                            w.Flush();
//                            myServer.DisconnectClient(this);
//                            tcpclient.Close();
//                            // Skicka tillbaka ett värde (bool???) för att kolla om UserName inte var unikt eller dyl...
//                        }
//                    }

//                    message = JsonConvert.SerializeObject(tempUser);
//                    if (tempUser.TypeOfMessage == MessageType.Message)
//                    {
//                        myServer.Broadcast(this, message);
//                    }
//                    Console.WriteLine(message);
//                }

//                myServer.DisconnectClient(this);
//                tcpclient.Close();
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//        }
//    }
//}
