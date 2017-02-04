using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotDogs.Web.Models
{
    /// <summary>
    /// Classe specialisé representant un client de l'application 
    /// </summary>
    public class HotDogCustomer : HotDogUser
    {
        /// <summary>
        /// Magasins favoris du client
        /// </summary>
        public virtual ICollection<HotDogStore> FavoriteStores { get; set; }

        /// <summary>
        /// Commandes du client
        /// </summary>
        public virtual ICollection<HotDogOrder> Orders { get; set; }
    }
}