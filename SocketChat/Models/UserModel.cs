using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocketChat.Models
{
    public class UserModel : IEquatable<UserModel>
    {
        public string ConnectionId { get; set; }
        public string UserName { get; set; }

        public bool Equals(UserModel other)
        {
            return this.ConnectionId == other.ConnectionId;
        }
    }
}