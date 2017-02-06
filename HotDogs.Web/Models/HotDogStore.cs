using System.Collections.Generic;

namespace HotDogs.Web.Models
{
    public class HotDogStore
    {
        public HotDogStore()
        {
            Orders = new List<HotDogOrder>();
            HotDogs = new List<HotDog>();
            Managers = new List<HotDogStoreManager>();
        }

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

        ///// <summary>
        ///// le manager qui possède le magasin
        ///// </summary>
        public virtual HotDogStoreManager Owner { get; set; }

        /// <summary>
        /// les managers du magasin  
        /// </summary>
        public virtual IList<HotDogStoreManager> Managers { get; set; }

        /// <summary>
        /// Hotdogs vendu par le magasin
        /// </summary>
        public virtual IList<HotDog> HotDogs { get; set; }

        /// <summary>
        /// Commandes passés par les clients du magasin
        /// </summary>
        public virtual IList<HotDogOrder> Orders { get; set; }
    }
}
