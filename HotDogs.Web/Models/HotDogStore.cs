using System.Collections.Generic;

namespace HotDogs.Web.Models
{
    public class HotDogStore
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string ManagerName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<HotDog> HotDogs { get; set; }
    }
}
