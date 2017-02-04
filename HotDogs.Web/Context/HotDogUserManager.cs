using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotDogs.Web.Context
{
    public class HotDogUserManager : UserManager<IdentityUser>
    {
        public HotDogUserManager() : base(new HotDogUserStore())
        {

        }

        public HotDogUserManager(HotDogContext context) : base(new HotDogUserStore(context))
        {

        }
    }
}