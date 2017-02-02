using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace HotDogs.Web.Context
{
    public class HotDogContextInitializer : MigrateDatabaseToLatestVersion<HotDogContext, HotDogContextConfiguration>
    {
    }
}