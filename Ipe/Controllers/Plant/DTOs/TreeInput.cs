using System;
using System.ComponentModel.DataAnnotations;

namespace Ipe.Controllers.Plant.DTOs
{
    public class TreeInput
    {
        [Required]
        public string Id { get; set; }
        public string Name { get; set; }

        [MaxLength(280)]
        public string Message { get; set; }

        [MaxLength(5)]
        [StringLengthListAttribute(1, 20)]
        public List<string> Hastags { get; set; }
    }
}

