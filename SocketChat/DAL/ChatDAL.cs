using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocketChat.Models;

namespace SocketChat.DAL
{
    public static class ChatDAL
    {
        private static int roomId = 1;
        private static Dictionary<string, RoomModel> availableRooms = new Dictionary<string, RoomModel>();

        public static IEnumerable<RoomModel> GetRooms()
        {
            return availableRooms.Select(kvp => kvp.Value);
        }

        public static int CreateRoom(string name, UserModel creator)
        {
            RoomModel newRoom = new RoomModel()
            {
                Id = roomId,
                Name = name
            };
            newRoom.Users.Add(creator);

            availableRooms.Add(roomId.ToString(), newRoom);

            return roomId++;
        }

        public static void AddUserToRoom(string roomId, UserModel user)
        {
            RoomModel roomToJoin = availableRooms[roomId];
            roomToJoin.Users.Add(user);
        }

        public static void RemoveUserFromRoom(string roomId, UserModel user)
        {
            RoomModel roomToJoin = availableRooms[roomId];
            roomToJoin.Users.Remove(user);
        }

        public static IEnumerable<UserModel> GetUsersInRoom(string roomId)
        {
            return availableRooms[roomId].Users;
        }
    }
}