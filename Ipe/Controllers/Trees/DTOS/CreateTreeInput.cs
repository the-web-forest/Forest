using System.ComponentModel.DataAnnotations;

namespace Ipe.Controllers.Trees.DTOS
{
    public class CreateTreeInput
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Value { get; set; }
        [Required]
        public string Biome { get; set; }
    }
}
