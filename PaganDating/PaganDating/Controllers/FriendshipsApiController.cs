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
        public void SendRequest(int requesterId, int recipientId)
        {
            
            var request = new Friendships();

            request.RequestAccepted = false;
            request.User = db.UserSet.FirstOrDefault(u => u.Id == requesterId);
            request.Friend = db.UserSet.FirstOrDefault(f => f.Id == recipientId);

            db.FriendshipsSet.Add(request);
            db.SaveChanges();
        }
        
        [HttpGet]
        [Route("acceptRequest")]
        public void AcceptRequest(int requesterId, int recipientId)
        {
            var requester = db.UserSet.FirstOrDefault(r => r.Id == requesterId);
            var recipient = db.UserSet.FirstOrDefault(r => r.Id == recipientId);

            var request = db.FriendshipsSet
                .Where(a => a.RequestAccepted == false)
                .Where(f => f.User.Id == requester.Id)
                .FirstOrDefault(f => f.Friend.Id == recipient.Id);

            request.RequestAccepted = true;

            var newFriendship = new Friendships
            {
                User = recipient,
                Friend = requester,
                RequestAccepted = true
            };

            db.FriendshipsSet.Add(newFriendship);
            db.SaveChanges();
        }

        [HttpGet]
        [Route("countRequests")]
        public int CountRequests(int userId)
        {
            var user = db.UserSet.FirstOrDefault(u => u.Id == userId);
            var count = user.Friends1.Where(a => a.RequestAccepted == false).Count();

            return count;
        }
    }
}