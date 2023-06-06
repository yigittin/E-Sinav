using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Ogrenciler
{
    public class OgrenciSinav:FullAuditedAggregateRoot<int>
    {
        public Guid OgrenciId { get; set; }
        public Guid SinavId { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public List<OgrenciCevap>? ogrenciCevaplar { get; set; }
    }
}
