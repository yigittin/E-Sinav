using SanalVaka.Ogrenciler;
using SanalVaka.Yetkililer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Bolumler
{
    public class Bolum : FullAuditedAggregateRoot<Guid>
    {
        public string BolumAdi { get; set; }
        public virtual ICollection<BolumYetkili> Yetkililer { get; set; }

    }
}
