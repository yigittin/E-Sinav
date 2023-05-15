using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanalVaka.DersDtos
{
    public class CreateUpdateDersDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
        [Required]
        public Guid BolumId { get; set; }
        public List<SinifDto> SinifList { get; set; }
    }
}
