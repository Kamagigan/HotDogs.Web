using System.Collections.Generic;

namespace HotDogs.Web.Models
{
    /// <summary>
    /// Classe specialisé representant un client de l'application 
    /// </summary>
    public class HotDogCustomer : HotDogUser
    {
        public HotDogCustomer() : base()
        {
            FavoriteStores = new List<HotDogStore>();
            Orders = new List<HotDogOrder>();
        }

        /// <summary>
        /// Magasins favoris du client
        /// </summary>
        public virtual IList<HotDogStore> FavoriteStores { get; set; }

        /// <summary>
        /// Commandes du client
        /// </summary>
        public virtual IList<HotDogOrder> Orders { get; set; }
    }
}