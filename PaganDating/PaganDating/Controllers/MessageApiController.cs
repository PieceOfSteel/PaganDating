using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PaganDating.Controllers
{
    [RoutePrefix("api/messages")]
    public class MessageApiController : ApiController
    {
        [HttpGet]
        [Route("create")]
        public void createMessage (string content, int author, int recipient)
        {
            var db = new PaganDatingModelContainer();
            var message = new Message();

            message.Content = content;
            message.Author = db.UserSet.FirstOrDefault(a => a.Id == author);
            message.Recipient = db.UserSet.FirstOrDefault(r => r.Id == recipient);

            db.MessageSet.Add(message);
            db.SaveChanges();
        }
    }
}