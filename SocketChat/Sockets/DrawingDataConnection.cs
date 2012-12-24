using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SignalR;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using SocketChat.Models;

namespace SocketChat.Sockets
{
    public class DrawingDataConnection : PersistentConnection
    {
        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            return Connection.Broadcast(data);
        }
    }
}