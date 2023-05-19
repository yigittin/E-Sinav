using SanalVaka.Ogrenciler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace SanalVaka.Bolumler
{
    public class Bolum : FullAuditedAggregateRoot<Guid>
    {
        public string BolumAdi { get; set; }
        public virtual ICollection<IdentityUser>? Yetkililer { get; set; }
        public bool IsOnaylandi { get; set; }
        public Guid? BolumOnayciId { get; set; }
        public string? BolumOnayciUsername { get; set; }
        public string? BolumOnayciAdi { get; set; }

    }
}
