using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PaganDating.Controllers
{
    [RoutePrefix("api/friendships")]
    public class FriendshipsApiController : ApiController
    {
        PaganDatingModelContainer db = new PaganDatingModelContainer();

        [HttpGet]
        [Route("sendRequest")]
        public void sendRequest(int senderId, int recipientId)
        {
            
            var request = new Friendships();

            request.RequestAccepted = false;
            request.User = db.UserSet.FirstOrDefault(u => u.Id == senderId);
            request.Friend = db.UserSet.FirstOrDefault(f => f.Id == recipientId);

            db.FriendshipsSet.Add(request);
            db.SaveChanges();
        }

        [HttpGet]
        [Route("getFriends")]
        public List<User> getFriends(int userId)
        {
            
            var user = db.UserSet.FirstOrDefault(u => u.Id == userId);
            var friendships = user.Friends.Where(a => a.RequestAccepted == true).ToList();
            var friends = friendships.Select(row => row.Friend).ToList();

            return friends;
        }

        [HttpGet]
        [Route("getRequests")]
        public List<User> getRequests(int userId)
        {
            var user = db.UserSet.FirstOrDefault(u => u.Id == userId);
            var requestedFriendships = user.Friends1.Where(a => a.RequestAccepted == false).ToList();
            var requests = requestedFriendships.Select(row => row.User).ToList();

            return requests;
        }
    }
}