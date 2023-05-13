using SanalVaka.Dersler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace SanalVaka.Ogrenciler
{
    public class DersYetkili : FullAuditedAggregateRoot<Guid>
    {
        public Guid DersId { get; set; }
        public IdentityUser YetkiliId { get; set; }
    }
}
