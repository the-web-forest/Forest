using System.ComponentModel.DataAnnotations;

namespace Ipe.Controllers.Plant.DTOs
{
    public class PlantInput
    {
        [Required, MinLength(10)]
        public string CardToken { get; set; }

        [Required, MinLength(1)]
        public List<TreeInput> Trees { get; set; }
    }
}

