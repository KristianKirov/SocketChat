﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketChat.Models
{
    public class MessageModel
    {
        public string RoomId { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}