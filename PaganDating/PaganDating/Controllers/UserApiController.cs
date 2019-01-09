using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace PaganDating.Controllers
{
    [RoutePrefix("api/user")]
    public class UserApiController : ApiController
    {
        [HttpGet]
        [Route("getUserId")]
        public int GetUserId()
        {
            var idString = User.Identity.GetUserId();
            var id = 0;

            if (!string.IsNullOrEmpty(idString))
            {
                id = Convert.ToInt32(idString);
            }

            return id;
        }
    }
}