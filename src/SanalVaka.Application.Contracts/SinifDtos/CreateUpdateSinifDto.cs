using SanalVaka.OgrenciDtos;
using SanalVaka.YetkiliDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanalVaka.SinifDtos
{
    public class CreateUpdateSinifDto
    {
        [Required]
        public string SinifAdi { get; set; }
        [Required]
        public int SinifLimit { get; set; }
        [Required]
        public Guid DersId { get; set; }
        [Required]
        public string DersAdi { get; set; }
        public ICollection<OgrenciDto> OgrenciList { get; set; }
        public ICollection<SinifYetkiliDto> YetkiliList { get; set; }
        
    }
}
