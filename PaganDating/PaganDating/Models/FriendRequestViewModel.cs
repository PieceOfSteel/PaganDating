using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;

namespace PaganDating.Models
{
    public class FriendRequestViewModel
    {
        public User Requester { get; set; }
        public User Recipient { get; set; }

        public FriendRequestViewModel() { }
    }
}