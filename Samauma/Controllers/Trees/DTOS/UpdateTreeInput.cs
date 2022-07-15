using System.ComponentModel.DataAnnotations;

namespace Samauma.Controllers.Trees.DTOS
{
    public class UpdateTreeInput
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Values below {1} are invalid.")]
        public double Value { get; set; }
        [Required]
        public string Biome { get; set; }
        public string? Image { get; set; }
    }
}
