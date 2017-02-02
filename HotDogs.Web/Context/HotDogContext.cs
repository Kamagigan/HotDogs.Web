using HotDogs.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;

namespace HotDogs.Web.Context
{
    public class HotDogContext : IdentityDbContext
    {
        public HotDogContext() : base("HotDogContext")
        {
            Database.SetInitializer(new HotDogContextInitializer());
        }

        public DbSet<HotDogStore> Stores { get; set; }
        public DbSet<HotDog> HotDogs { get; set; }
    }
}