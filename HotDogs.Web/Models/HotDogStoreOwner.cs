using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotDogs.Web.Models
{
    public class HotDogStoreOwner : HotDogUser
    {
        public virtual HotDogStore Store { get; set; }
    }
}