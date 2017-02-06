using HotDogs.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HotDogs.Web.Context
{
    public class HotDogSeedDb : DropCreateDatabaseIfModelChanges<HotDogContext>
    {
        public HotDogSeedDb()
        {
        }

        protected override void Seed(HotDogContext context)
        {
            if (!context.Stores.Any())
            {
                #region Init Roles
                context.Roles.Add(new IdentityRole("admin"));
                context.Roles.Add(new IdentityRole("manager"));
                context.Roles.Add(new IdentityRole("customer"));
                #endregion

                #region Init Users

                //les Owner doit être defini en dehors du scope de userManager pour être utilisé plus tard
                HotDogStoreManager tommysOwner = null;
                HotDogStoreManager hotdogfatherOwner = null;
                HotDogStoreManager emilysOwner = null;

                using (var userManager = new HotDogUserManager(context))
                {
                    // Init Admins
                    var lcudini = new HotDogUser()
                    {
                        UserName = "lcudini",
                        Email = "lcudini@klanik.com",
                    };

                    userManager.Create(lcudini, "Azerty0!");
                    userManager.AddToRole(lcudini.Id, "admin");

                    var gmoreau = new HotDogUser()
                    {
                        UserName = "gmoreau",
                        Email = "gmoreau@klanik.com",
                    };

                    userManager.Create(gmoreau, "Azerty0!");
                    userManager.AddToRole(gmoreau.Id, "manager");

                    // Init Store Owner
                    tommysOwner = new HotDogStoreManager()
                    {
                        UserName = "tommys",
                        Email = "tommys@hotdog.com",
                    };

                    userManager.Create(tommysOwner, "Azerty0!");
                    userManager.AddToRole(tommysOwner.Id, "manager");

                    hotdogfatherOwner = new HotDogStoreManager()
                    {
                        UserName = "hotdogfather",
                        Email = "hotdogfather@hotdog.com",
                    };

                    userManager.Create(hotdogfatherOwner, "Azerty0!");
                    userManager.AddToRole(hotdogfatherOwner.Id, "manager");

                    emilysOwner = new HotDogStoreManager()
                    {
                        UserName = "emilys",
                        Email = "emilys@hotdog.com"
                    };

                    userManager.Create(emilysOwner, "Azerty0!");
                    userManager.AddToRole(emilysOwner.Id, "manager");

                    // Create Clients
                    var customer1 = new HotDogCustomer()
                    {
                        UserName = "gerard",
                        Email = "gerard@gmail.com",
                    };

                    userManager.Create(customer1, "Azerty0!");
                    userManager.AddToRole(customer1.Id, "customer");

                    var customer2 = new HotDogCustomer()
                    {
                        UserName = "jeanjacques",
                        Email = "jeanjacques@gmail.com",
                    };

                    userManager.Create(customer2, "Azerty0!");
                    userManager.AddToRole(customer2.Id, "customer");
                }

                #endregion

                #region Init Stores

                var tommys = new HotDogStore()
                {
                    Name = "Tommy's Dinner",
                    Location = "Avignon",
                    Latitude = 43.9786091,
                    Longitude = 4.8712175,
                    Owner = tommysOwner,
                    Managers = new List<HotDogStoreManager>()
                    {
                        tommysOwner
                    }
                };

                context.Stores.Add(tommys);

                var hotdogfather = new HotDogStore()
                {
                    Name = "The Hot Dog Father",
                    Location = "Lyon",
                    Latitude = 45.7634609,
                    Longitude = 4.8427191,
                    Owner = hotdogfatherOwner,
                    Managers = new List<HotDogStoreManager>()
                    {
                        hotdogfatherOwner
                    }
                };

                context.Stores.Add(hotdogfather);

                var emilys = new HotDogStore()
                {
                    Name = "Emily's American Diner",
                    Location = "Grenoble",
                    Latitude = 45.1921764,
                    Longitude = 5.7304209,
                    Owner = emilysOwner,
                    Managers = new List<HotDogStoreManager>()
                    {
                        emilysOwner
                    }
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