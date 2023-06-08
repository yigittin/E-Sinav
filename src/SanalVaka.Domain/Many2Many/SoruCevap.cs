using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Many2Many
{
    public class SoruCevap:FullAuditedAggregateRoot<Guid>
    {
        public Guid SoruId { get; set; }
        public Guid? OgrenciCevap { get; set; }
        public Guid OgrenciId { get; set; }
    }
}
