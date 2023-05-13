using SanalVaka.Ogrenciler;
using SanalVaka.Yetkililer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Siniflar
{
    public class Sinif:FullAuditedAggregateRoot<Guid>
    {
        public int SinifLimit { get; set; }
        public string SinifName { get; set; }

        public virtual ICollection<Ogrenci> SinifOgrenciler { get; set; }
        public virtual ICollection<SinifYetkili> Yetkililer { get; set; }
    }
}
