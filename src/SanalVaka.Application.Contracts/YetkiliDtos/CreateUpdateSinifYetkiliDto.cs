using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Identity;

namespace SanalVaka.YetkiliDtos
{
    public class CreateUpdateSinifYetkiliDto
    {
        [Required]
        public string OgretmenNo { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public ICollection<SinifDto> Siniflar { get; set; }
    }
}
