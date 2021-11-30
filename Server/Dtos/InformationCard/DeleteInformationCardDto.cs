using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Dtos.InformationCard
{
    public class DeleteInformationCardDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Name { get; set; }
    }
}
