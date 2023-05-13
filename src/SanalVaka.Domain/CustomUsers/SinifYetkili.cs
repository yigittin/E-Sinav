using SanalVaka.Dersler;
using SanalVaka.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace SanalVaka.CustomUsers
{
    public class SinifYetkili : FullAuditedAggregateRoot<Guid>
    {
        public Guid SinifId { get; set; }
        public IdentityUser YetkiliId { get; set; }
    }
}
