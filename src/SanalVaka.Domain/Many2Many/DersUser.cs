using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Many2Many
{
    public class DersUser:FullAuditedAggregateRoot<Guid>
    {
        public Guid DersId { get; set; }
        public Guid UserId { get; set; }

    }
}
