using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Many2Many
{
    public class SinifYetkili:FullAuditedAggregateRoot<int>
    {
        public Guid YetkiliId { get; set; }
        public Guid SinifId { get; set; }
    }
}
