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
    public class CommonDataConnection : PersistentConnection
    {
        //protected override Task OnConnectedAsync(IRequest request, string connectionId)
        //{
        //    return Groups.Add(connectionId, "foo");
        //}

        protected override Task OnReceivedAsync(IRequest request, string connectionId, string data)
        {
            JavaScriptSerializer deserializer = new JavaScriptSerializer();
            MessageModel messageData = deserializer.Deserialize<MessageModel>(data);

            return Groups.Send(messageData.RoomId, data);
        }

        //protected override Task OnDisconnectAsync(string connectionId)
        //{
        //    return Groups.Remove(connectionId, "foo");
        //}
    }
}