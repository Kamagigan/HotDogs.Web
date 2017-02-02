using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotDogs.Web.Models
{
    public class HotDog
    {
        /// <summary>
        /// Id du hotdog
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Nom du Hotdog
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description du hotdog
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Prix du Hotdog
        /// </summary>
        public int Price { get; set; }
        /// <summary>
        /// Disponibilité du hotdig dans le magasin
        /// </summary>
        public bool Available { get; set; }
        /// <summary>
        /// Date d'ajout du hotdog
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Temps de preparation du hotdogs en minutes
        /// </summary>
        public int PrepTime { get; set; }
        /// <summary>
        /// Id du magasin vendant le hotdog
        /// </summary>
        public int HotDogStoreId { get; set; }

        //public string Description { get; set; }
        //public string ImagePath { get; set; }
        //public bool IsFavorite { get; set; }
        //public string GroupName { get; set; }
    }
}
