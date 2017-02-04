using HotDogs.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;

namespace HotDogs.Web.Context
{
    public class HotDogSeedDb : DropCreateDatabaseIfModelChanges<HotDogContext>
    {
        public HotDogSeedDb()
        {
            //AutomaticMigrationsEnabled = true;
            //AutomaticMigrationDataLossAllowed = true;
        }

        protected override async void Seed(HotDogContext context)
        {
            if (!context.Stores.Any())
            {
                #region Init Users

                // Init Store Owner
                var tommysOwner = new HotDogStoreOwner()
                {
                    UserName = "tommys",
                    Email = "tommys@hotdog.com",
                };

                var hotdogfatherOwner = new HotDogStoreOwner()
                {
                    UserName = "hotdogfather",
                    Email = "hotdogfather@hotdog.com",
                };

                var emilysOwner = new HotDogStoreOwner()
                {
                    UserName = "emilys",
                    Email = "emilys@hotdog.com"
                };

                using (var userManager = new HotDogUserManager(context))
                {
                    // Create Admins
                    userManager.Create(
                        new HotDogUser()
                        {
                            UserName = "lcudini",
                            Email = "lcudini@klanik.com"
                        }, "Azerty0!");

                    userManager.Create(
                        new HotDogUser()
                        {
                            UserName = "gmoreau",
                            Email = "gmoreau@klanik.com",
                        }, "Azerty0!");

                    // Create Store Owners  
                    userManager.Create(tommysOwner, "Azerty0!");

                    userManager.Create(hotdogfatherOwner, "Azerty0!");

                    userManager.Create(emilysOwner, "Azerty0!");

                    // Create Clients
                    userManager.Create(
                        new HotDogCustomer()
                        {
                            UserName = "gerard",
                            Email = "gerard@gmail.com",
                        }, "Azerty0!");

                    userManager.Create(
                        new HotDogCustomer()
                        {
                            UserName = "jeanjacques",
                            Email = "jeanjacques@gmail.com",
                        }, "Azerty0!");
                }

                #endregion

                #region Init Stores

                var tommys = new HotDogStore()
                {
                    Name = "Tommy's Dinner",
                    Location = "Avignon",
                    Latitude = 43.9786091,
                    Longitude = 4.8712175,
                    Owner = tommysOwner
                };

                context.Stores.Add(tommys);

                var hotdogfather = new HotDogStore()
                {
                    Name = "The Hot Dog Father",
                    Location = "Lyon",
                    Latitude = 45.7634609,
                    Longitude = 4.8427191,
                    Owner = hotdogfatherOwner
                };

                context.Stores.Add(hotdogfather);

                var emilys = new HotDogStore()
                {
                    Name = "Emily's American Diner",
                    Location = "Grenoble",
                    Latitude = 45.1921764,
                    Longitude = 5.7304209,
                    Owner = emilysOwner
                };

                context.Stores.Add(emilys);

                #endregion

                #region Init Hotdogs
                tommys.HotDogs = new List<HotDog>()
                {
                    new HotDog () {
                        Name = "The Classic",
                        Description = "Avec Pain, Saucisse Jumbo et Moutarde",
                        Available =true,
                        PrepTime =10,
                        Price =8,
                        DateCreated = DateTime.Now,
                        Store = tommys
                    },
                    new HotDog()
                    {
                        Name = "Chili Hot Dog",
                        Description = "Avec Pain, Saucisse, Chili Con Carne et Fromage",
                        Available = true,
                        PrepTime= 15,
                        Price = 10,
                        DateCreated = DateTime.Now,
                        Store = tommys
                    },
                    new HotDog()
                    {
                        Name = "Pastrami'n Hot Dog",
                        Description = "Avec Pain, Saucisse, Pastrami et Fromage",
                        Available = true,
                        PrepTime= 15,
                        Price = 10,
                        DateCreated = DateTime.Now,
                        Store = tommys
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
                        DateCreated = DateTime.Now,
                        Store = hotdogfather
                    },
                    new HotDog()
                    {
                        Name = "BBQ Dog",
                        Description = "Avec Pain, Saucisse, Sauce BBQ, Oigons Frits",
                        Available = true,
                        PrepTime= 12,
                        Price = 8,
                        DateCreated = DateTime.Now,
                        Store = hotdogfather
                    },
                    new HotDog()
                    {
                        Name = "Spicy Dog",
                        Description = "Avec Pain, Saucisse, Sauce Cheddar, Hot Sauce, Jalapeno",
                        Available = true,
                        PrepTime= 15,
                        Price = 8,
                        DateCreated = DateTime.Now,
                        Store = hotdogfather
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
                        DateCreated = DateTime.Now,
                        Store = emilys
                    },
                    new HotDog()
                    {
                        Name = "Manhattan Hot Dog",
                        Description = "Avec Pain, Saucisse, Bacon, Chedar sauce, Ketchup & Moutarde au miel ",
                        Available = true,
                        PrepTime= 12,
                        Price = 11,
                        DateCreated = DateTime.Now,
                        Store = emilys
                    },
                    new HotDog()
                    {
                        Name = "Hot Dog Park",
                        Description = "Avec Pain, Saucisse, champignons Sauce Cheddar, Ketchup & Moutarde au miel",
                        Available = true,
                        PrepTime= 10,
                        Price = 11,
                        DateCreated = DateTime.Now,
                        Store = emilys
                    }
                };

                context.HotDogs.AddRange(emilys.HotDogs);

                #endregion

                base.Seed(context);
            }
        }
    }
}