using Microsoft.AspNet.Identity.EntityFramework;

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
