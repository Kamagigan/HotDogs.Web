using Microsoft.AspNet.Identity.EntityFramework;

namespace HotDogs.Web.Context
{
    public class HotDogUserStore : UserStore<IdentityUser>
    {
        public HotDogUserStore() : base(new HotDogContext())
        {

        }

        public HotDogUserStore(HotDogContext context) : base(context)
        {

        }
    }
}