using System.ComponentModel.DataAnnotations;

namespace FDMC.Models
{
    public class Cat
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        [Range(1, 30)]
        public int Age { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Breed { get; set; } = string.Empty;
        public IFormFile? Image {  get; set; }
    }
}
