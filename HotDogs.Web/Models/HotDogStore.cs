using System.Collections.Generic;

namespace HotDogs.Web.Models
{
    public class HotDogStore
    {
        /// <summary>
        /// Id du magasin
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nom du magasin
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Nom de la ville du magasin
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Latitude GPS du magasin
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude GPS du magasin
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// lien vers le gerant du magasin
        /// </summary>
        public virtual HotDogStoreOwner Owner { get; set; }

        /// <summary>
        /// Hotdogs vendu par le magasin
        /// </summary>
        public virtual ICollection<HotDog> HotDogs { get; set; }

        /// <summary>
        /// Commandes passés par les clients du magasin
        /// </summary>
        public virtual ICollection<HotDogOrder> Orders { get; set; }
    }
}
