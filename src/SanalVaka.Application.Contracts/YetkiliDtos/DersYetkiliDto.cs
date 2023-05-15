using SanalVaka.DersDtos;
using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace SanalVaka.YetkiliDtos
{
    public class DersYetkiliDto: AuditedEntityDto<int>
    {
        public string OgretmenNo { get; set; }
        public Guid UserId { get; set; }
        public virtual IdentityUserDto User { get; set; }
        public ICollection<DersDto> Dersler { get; set; }
    }
}
