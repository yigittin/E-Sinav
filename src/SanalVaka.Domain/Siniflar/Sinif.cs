using SanalVaka.Bolumler;
using SanalVaka.Dersler;
using SanalVaka.Ogrenciler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace SanalVaka.Siniflar
{
    public class Sinif:FullAuditedAggregateRoot<Guid>
    {
        public int SinifLimit { get; set; }
        public string SinifName { get; set; }
        public Guid DersId { get; set; }
        [ForeignKey("DersId")]
        public virtual Ders Ders { get; set; }
        public virtual ICollection<Ogrenci>? SinifOgrenciler { get; set; }
        public virtual ICollection<IdentityUser>? Yetkililer { get; set; }
        public bool IsOnaylandi { get; set; }
        public Guid SinifOnayciId { get; set; }
        public string SinifOnayciUsername { get; set; }
        public string SinifOnayciAdi { get; set; }

    }
}
