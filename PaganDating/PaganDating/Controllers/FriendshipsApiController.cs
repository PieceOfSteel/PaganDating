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
            if(requesterId == recipientId)
            {
                return;
            }

            if(GetFriendship(requesterId, recipientId) != null)
            {
                //Alert: request sent/already friend
                return;
            }
            else
            {
                var request = new Friendships();

                request.RequestAccepted = false;
                request.User = db.UserSet.FirstOrDefault(u => u.Id == requesterId);
                request.Friend = db.UserSet.FirstOrDefault(f => f.Id == recipientId);

                db.FriendshipsSet.Add(request);
                db.SaveChanges();
            }

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
        [Route("rejectRequest")]
        public void RejectRequest(int requesterId, int recipientId)
        {
            var requester = db.UserSet.FirstOrDefault(u => u.Id == requesterId);
            var recipient = db.UserSet.FirstOrDefault(u => u.Id == recipientId);

            var request = requester
                .Friends
                .Where(a => a.RequestAccepted == false)
                .Where(u => u.User.Id == requester.Id)
                .FirstOrDefault(f => f.Friend.Id == recipient.Id);

            db.FriendshipsSet.Remove(request);
            db.SaveChanges();
        }

        [HttpGet]
        [Route("countRequests")]
        public int CountRequests(int userId)
        {
            try
            {
                var user = db.UserSet.FirstOrDefault(u => u.Id == userId);
                var count = user.Friends1.Where(a => a.RequestAccepted == false).Count();

                return count;
            }
            catch { }
            
            return 0;
        }

        private bool FriendshipExists(Friendships friendship)
        {
            if(friendship != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Friendships GetFriendship(int userId, int friendId)
        {
            var friendship = db.FriendshipsSet
                .Where(f => f.User.Id == userId)
                .FirstOrDefault(f => f.Friend.Id == friendId);

            return friendship;
        }
    }
}