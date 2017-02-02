using System.ComponentModel.DataAnnotations;

namespace HotDogs.Web.ViewModels
{
    public class HotDogStoreViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Location { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string ManagerName { get; set; }
    }
}
