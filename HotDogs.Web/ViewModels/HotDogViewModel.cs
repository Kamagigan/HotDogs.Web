using System;
using System.ComponentModel.DataAnnotations;

namespace HotDogs.Web.ViewModels
{
    public class HotDogViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "nom du hotdog trop (max 30)")]
        public string Name { get; set; }

        [StringLength(100, MinimumLength =10)]
        public string Description { get; set; }

        public DateTime DateCreated { get; set; } = DateTime.Now;

        [Required]
        [Range(1, 50, ErrorMessage = "le prix du hotdog doit être compris entre 0 et 50 euros")]
        public int Price { get; set; }

        public bool Available { get; set; } = false;

        [Range(1, Int32.MaxValue, ErrorMessage = "le temps de preparation du hotdog doit être minimum de 1 minutes")]
        public int PrepTime { get; set; }
    }
}
