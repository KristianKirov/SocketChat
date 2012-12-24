using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketChat.Models
{
    public class RoomModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int PartisipantsCount
        {
            get
            {
                return Users.Count;
            }
        }

        public List<UserModel> Users { get; private set; }

        public RoomModel()
        {
            Users = new List<UserModel>();
        }
    }
}