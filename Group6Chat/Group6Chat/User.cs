﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Group6Chat
{
    public enum MessageType
    {
        UserName,
        PrivateMessage,
        Message,
        ErrorMessage,
        UserList,
        Quit
    }
    public class User
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public string Version { get; set; }
        public MessageType TypeOfMessage { get; set; }
        public string Receiver { get; set; }

        public User()
        {
            Version = "1.0";
        }

        public static string ToJson(string userName, string message, MessageType messageType)
        {
            User u = new User();
            u.UserName = userName;
            u.Message = message;
            u.TypeOfMessage = messageType;
            return JsonConvert.SerializeObject(u);
        }
        public static string ToJson(User u)
        {
            return User.ToJson(u.UserName, u.Message, u.TypeOfMessage);
        }
        public static string ToJson(string userName, string message, MessageType messageType, string receiver)
        {
            User u = new User();
            u.UserName = userName;
            u.Message = message;
            u.TypeOfMessage = messageType;
            u.Receiver = receiver;
            return JsonConvert.SerializeObject(u);
        }
    }
}
