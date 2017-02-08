using HotDogs.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace HotDogs.Web.Context
{
    public class HotDogContext : IdentityDbContext
    {
        public HotDogContext() : base("HotDogContext")
        {
            Database.SetInitializer(new HotDogSeedDb());
            this.Configuration.ProxyCreationEnabled = false;
        }

        // persiste les magasins
        public DbSet<HotDogStore> Stores { get; set; }
        // persiste les hotdogs
        public DbSet<HotDog> HotDogs { get; set; }
        // persiste les clients
        public DbSet<HotDogCustomer> Customers {get; set;}
        // persiste les proprietaires
        public DbSet<HotDogStoreManager> StoreManagers { get; set; }
        // persiste les commandes
        public DbSet<HotDogOrder> Orders { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Deactive les cascade de suppression dans les relations ManyToMany
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            // Relation 1 hotdog => 1 Store => * hotdogs
            // Si le store est supprimé -> les hotdogs sont supprimés 
            modelBuilder.Entity<HotDogStore>()
                .HasMany(s => s.HotDogs)
                .WithRequired(h => h.Store)
                .WillCascadeOnDelete(true);

            //// Relation 1 Store => 1 Owner => 0.1 Store
            modelBuilder.Entity<HotDogStore>()
                .HasRequired(s => s.Owner);

            // Relation * managers => * magasins
            modelBuilder.Entity<HotDogStore>()
                .HasMany(s => s.Managers)
                .WithMany(m => m.Stores)
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("StoreId");
                    m.ToTable("Managers_Stores");
                });

            // Relation 1 Store => * Orders => 1 Store
            modelBuilder.Entity<HotDogStore>()
                .HasMany(s => s.Orders)
                .WithRequired(o => o.Store)
                .WillCascadeOnDelete(true);

            // Relation * Customer => * Favorite Store
            modelBuilder.Entity<HotDogCustomer>()
                .HasMany(c => c.FavoriteStores)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("UserId");
                    m.MapRightKey("StoreId");
                    m.ToTable("Customers_FavoriteStores");
                });

            // Relation 1 client => * Orders => 1 client
            modelBuilder.Entity<HotDogCustomer>()
                .HasMany(c => c.Orders)
                .WithRequired(o => o.Customer)
                .WillCascadeOnDelete(false);

            // Relation 1 commande =>  1 client => * commandes
            // Si une commande est supprimée -> le client n'est pas supprimé
            modelBuilder.Entity<HotDogOrder>()
                .HasRequired(o => o.Customer)
                .WithMany(c => c.Orders)
                .WillCascadeOnDelete(false);

            // Relation 1 commande =>  1.* hotdogs
            // Si une commande est supprimée -> les hotdogs ne sont pas supprimés
            modelBuilder.Entity<HotDogOrder>()
                .HasRequired(o => o.HotDogs)
                .WithOptional()
                .WillCascadeOnDelete(false);

            // Relation 1 Commande => * Hotdogs
            modelBuilder.Entity<HotDogOrder>()
                .HasMany(o => o.HotDogs)
                .WithMany()
                .Map(m =>
                {
                    m.MapLeftKey("OrderId");
                    m.MapRightKey("HotDogId");
                    m.ToTable("OrdersHotDogs");
                });
        }
    }
}