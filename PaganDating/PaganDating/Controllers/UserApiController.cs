using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using DataLayer;

namespace PaganDating.Controllers
{
    [RoutePrefix("api/user")]
    public class UserApiController : ApiController
    {
        [HttpGet]
        [Route("getUserId")]
        public string GetAccountId()
        {
            var accountId = User.Identity.GetUserId();
            
            return accountId;
        }

        public int GetUserId(string accountId)
        {
            var userId = 0;
            if(!string.IsNullOrEmpty(accountId))
            {
                var db = new PaganDatingModelContainer();
                userId = db.UserSet.FirstOrDefault(u => u.AccountId == accountId).Id;
            }
 
            return userId;
        }
    }
}