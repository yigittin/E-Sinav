using SanalVaka.DersDtos;
using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanalVaka.OgrenciDtos
{
    public class CreateUpdateOgrenciDto
    {
        [Required]
        public string OgrenciNo { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public virtual ICollection<DersInfoDto> Dersler { get; set; }
        public virtual ICollection<SinifInfoDto> Siniflar { get; set; }
    }
}
