using System.Collections.Generic;

namespace HotDogs.Web.Models
{
    public class HotDogStoreManager : HotDogUser
    {
        public HotDogStoreManager() : base()
        {
            Stores = new List<HotDogStore>();
        }

        /// <summary>
        /// Magagsins gérés par le propriétaire
        /// </summary>
        public virtual IList<HotDogStore> Stores { get; set; }
    }
}