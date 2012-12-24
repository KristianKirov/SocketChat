using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SocketChat.Models;
using SignalR;
using SocketChat.Sockets;
using SocketChat.DAL;

namespace SocketChat.Controllers
{
    public class DataController : Controller
    {
        public ActionResult GetRooms()
        {
            IEnumerable<RoomModel> rooms = ChatDAL.GetRooms();

            return Json(rooms, JsonRequestBehavior.AllowGet);
        }

        public JsonResult JoinToRoom(string roomId, UserModel user)
        {
            var context = GlobalHost.ConnectionManager.GetConnectionContext<CommonDataConnection>();
            ChatDAL.AddUserToRoom(roomId, user);
            context.Groups.Add(user.ConnectionId, roomId).
                ContinueWith(
                 (t) => context.Connection.Broadcast(
                     string.Format("{{ \"messagetype\": \"joined\", \"connectionid\": \"{0}\", \"username\": \"{1}\", \"roomId\": \"{2}\" }}",
                         user.ConnectionId, user.UserName, roomId)
                     )
                 );

            return Json(ChatDAL.GetUsersInRoom(roomId), JsonRequestBehavior.AllowGet);
        }

        public void LeaveRoom(string roomId, UserModel user)
        {
            var context = GlobalHost.ConnectionManager.GetConnectionContext<CommonDataConnection>();
            context.Groups.Remove(user.ConnectionId, roomId).ContinueWith((t) => ChatDAL.RemoveUserFromRoom(roomId, user)).
                ContinueWith(
                (t) => context.Connection.Broadcast(
                    string.Format("{{ \"messagetype\": \"left\", \"connectionid\": \"{0}\", \"username\": \"{1}\", \"roomId\": \"{2}\" }}",
                        user.ConnectionId, user.UserName, roomId)
                    )
                );
        }

        public void CreateRoom(string roomName, UserModel user)
        {
            int roomId = ChatDAL.CreateRoom(roomName, user);

            var context = GlobalHost.ConnectionManager.GetConnectionContext<CommonDataConnection>();
            context.Groups.Add(user.ConnectionId, roomId.ToString()).
                ContinueWith(
                (t) =>
                context.Connection.Broadcast(
                    string.Format("{{ \"messagetype\": \"roomCreated\", \"id\": \"{0}\", \"name\": \"{1}\", \"connectionid\": \"{2}\" }}",
                        roomId, roomName, user.ConnectionId)
                    )
                );
        }
    }
}
