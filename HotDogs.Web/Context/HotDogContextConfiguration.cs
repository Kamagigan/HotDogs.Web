using HotDogs.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace HotDogs.Web.Context
{
    public class HotDogContextConfiguration : DbMigrationsConfiguration<HotDogContext>
    {
        public HotDogContextConfiguration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override async void Seed(HotDogContext context)
        {
            if (!context.Stores.Any())
            {
                using (HotDogUserManager userManager = new HotDogUserManager())
                {
                    if (await userManager.FindByEmailAsync("lcudini@klanik.com") == null)
                    {
                        var user = new HotDogUser()
                        {
                            UserName = "lcudini",
                            Email = "lcudini@klanik.com",
                        };

                        await userManager.CreateAsync(user, "Azerty0!");
                    }

                    if (await userManager.FindByEmailAsync("gmoreau@klanik.com") == null)
                    {
                        var user = new HotDogUser()
                        {
                            UserName = "gmoreau",
                            Email = "gmoreau@klanik.com",
                        };

                        await userManager.CreateAsync(user, "Azerty0!");
                    }
                }

                var tommys = new HotDogStore()
                {
                    Name = "Tommy's Dinner",
                    Location = "Avignon",
                    ManagerName = "lcudini",
                    Latitude = 43.9786091,
                    Longitude = 4.8712175,
                };

                context.Stores.Add(tommys);

                var hotdogfather = new HotDogStore()
                {
                    Name = "The Hot Dog Father",
                    Location = "Lyon",
                    ManagerName = "lcudini",
                    Latitude = 45.7634609,
                    Longitude = 4.8427191,
                };

                context.Stores.Add(hotdogfather);

                var emilys = new HotDogStore()
                {
                    Name = "Emily's American Diner",
                    Location = "Grenoble",
                    ManagerName = "dkadaboom",
                    Latitude = 45.1921764,
                    Longitude = 5.7304209
                };

                context.Stores.Add(emilys);

                context.SaveChanges();

                tommys.HotDogs = new List<HotDog>()
            {
                new HotDog () {
                    Name = "The Classic",
                    Description = "Avec Pain, Saucisse Jumbo et Moutarde",
                    Available =true,
                    PrepTime =10,
                    Price =8,
                    HotDogStoreId = tommys.Id
                },
                new HotDog()
                {
                    Name = "Chili Hot Dog",
                    Description = "Avec Pain, Saucisse, Chili Con Carne et Fromage",
                    Available = true,
                    PrepTime= 15,
                    Price = 10,
                    HotDogStoreId = tommys.Id
                },
                new HotDog()
                {
                    Name = "Pastrami'n Hot Dog",
                    Description = "Avec Pain, Saucisse, Pastrami et Fromage",
                    Available = true,
                    PrepTime= 15,
                    Price = 10,
                    HotDogStoreId = tommys.Id
                }
            };

                context.HotDogs.AddRange(tommys.HotDogs);

                hotdogfather.HotDogs = new List<HotDog>()
            {
                new HotDog()
                {
                    Name = "The Original",
                    Description = "Avec Pain, Saucisse et Moutarde US",
                    Available = true,
                    PrepTime= 10,
                    Price = 8,
                    HotDogStoreId = hotdogfather.Id
                },
                new HotDog()
                {
                    Name = "BBQ Dog",
                    Description = "Avec Pain, Saucisse, Sauce BBQ, Oigons Frits",
                    Available = true,
                    PrepTime= 12,
                    Price = 8,
                    HotDogStoreId = hotdogfather.Id
                },
                new HotDog()
                {
                    Name = "Spicy Dog",
                    Description = "Avec Pain, Saucisse, Sauce Cheddar, Hot Sauce, Jalapeno",
                    Available = true,
                    PrepTime= 15,
                    Price = 8,
                    HotDogStoreId = hotdogfather.Id
                }
            };

                context.HotDogs.AddRange(hotdogfather.HotDogs);

                emilys.HotDogs = new List<HotDog>()
                {
                    new HotDog()
                    {
                        Name = "Classic Hot Dog",
                        Description = "Avec Pain, Saucisse, Ketchup & Moutarde au miel",
                        Available = true,
                        PrepTime= 10,
                        Price = 8,
                        HotDogStoreId = emilys.Id
                    },
                    new HotDog()
                    {
                        Name = "Manhattan Hot Dog",
                        Description = "Avec Pain, Saucisse, Bacon, Chedar sauce, Ketchup & Moutarde au miel ",
                        Available = true,
                        PrepTime= 12,
                        Price = 11,
                        HotDogStoreId = emilys.Id
                    },
                    new HotDog()
                    {
                        Name = "Hot Dog Park",
                        Description = "Avec Pain, Saucisse, champignons Sauce Cheddar, Ketchup & Moutarde au miel",
                        Available = true,
                        PrepTime= 10,
                        Price = 11,
                        HotDogStoreId = emilys.Id
                    }
                };

                context.HotDogs.AddRange(emilys.HotDogs);

                base.Seed(context);
            }
        }
    }
}