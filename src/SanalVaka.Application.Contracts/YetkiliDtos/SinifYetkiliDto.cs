using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace SanalVaka.YetkiliDtos
{
    public class SinifYetkiliDto:AuditedEntityDto<int>
    {
        public string OgretmenNo { get; set; }
        public Guid UserId { get; set; }
        public virtual IdentityUserDto User { get; set; }
        public ICollection<SinifDto> Siniflar { get; set; } 
    }
}
