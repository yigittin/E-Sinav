using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Ogrenciler
{
    public class OgrenciCevap:FullAuditedAggregateRoot<int>
    {
        public Guid SoruId { get; set; }
        public Guid CevapId { get; set; }
        public Guid OgrenciId { get; set; }
    }
}
