using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataLayer;

namespace PaganDating.Models
{
    public class UserDetailsViewModel
    {
        public User User { get; set; }
        public List<User> Friends { get; set; }
        public List<User> Requests { get; set; }
        public bool FriendWithProfileOwner { get; set; }

        public UserDetailsViewModel() { }

        public UserDetailsViewModel(User user)
        {
            User = user;
            UpdateFriends();
            UpdateRequests();
            FriendWithProfileOwner = false;
        }

        public void UpdateFriends()
        {
            try
            {
                Friends = User
                    .Friends
                    .Where(a => a.RequestAccepted == true)
                    .Select(row => row.Friend)
                    .ToList();
            }
            catch { }
        }

        public void UpdateRequests()
        {
            try
            {
                Requests = User
                    .Friends1
                    .Where(a => a.RequestAccepted == false)
                    .Select(row => row.User)
                    .ToList();
            }
            catch { }
        }

        //public void SetProfileFriend(int profileId)
        //{
        //    if (Friends.FirstOrDefault(f => f.Id == profileId) == null)
        //    {
        //        FriendWithProfileOwner = true;
        //    }
        //}
    }
}