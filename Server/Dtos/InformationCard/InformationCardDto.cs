using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Dtos.InformationCard
{
    public class InformationCardDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
        [Required]
        public string Img { get; set; }
    }
}
