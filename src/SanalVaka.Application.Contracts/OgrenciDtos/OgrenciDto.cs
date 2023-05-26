using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using SanalVaka.DersDtos;
using SanalVaka.SinifDtos;
using Volo.Abp.Application.Dtos;

namespace SanalVaka.OgrenciDtos
{
    public class OgrenciDto:FullAuditedEntityDto<Guid>
    {
        public string OgrenciNo { get; set; }
        public Guid UserId { get; set; }
        public string OgrenciAdi { get; set; }
        public virtual ICollection<DersInfoDto> Dersler { get; set; }
        public virtual ICollection<SinifInfoDto> Siniflar { get; set; }
    }
}
