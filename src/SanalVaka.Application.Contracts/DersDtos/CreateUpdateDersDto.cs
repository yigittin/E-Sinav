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
        public string DersAdi { get; set; }
        [Required]
        public Guid BolumId { get; set; }
    }
}
