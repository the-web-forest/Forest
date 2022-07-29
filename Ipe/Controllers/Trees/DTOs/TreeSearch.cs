using System.ComponentModel.DataAnnotations;

namespace Ipe.Controllers.Trees.DTOs
{
    public class TreeSearch
    {
#nullable enable
        [MaxLength(15)]
        public string? Biome { get; set; }
#nullable disable
        public int? Skip { get; set; }
        public int? Take { get; set; }
        public bool? RequiredTotal { get; set; }
    }
}
