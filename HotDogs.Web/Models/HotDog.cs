using System;

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
        /// Magasin Vendant le hotdog
        /// </summary>
        public virtual HotDogStore Store { get; set; }
    }
}
