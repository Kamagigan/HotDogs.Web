using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;

namespace HotDogs.Web.Models
{
    public class HotDogUser : IdentityUser
    {
        public HotDogUser() : base()
        {

        }

        public string Avatar { get; set; }
    }
}
