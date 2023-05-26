using SanalVaka.Bolumler;
using SanalVaka.Ogrenciler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace SanalVaka.Dersler
{
    public class Ders:FullAuditedAggregateRoot<Guid>
    {
        public string DersAdi { get; set; }
        public Guid BolumId { get; set; }
        [ForeignKey("BolumId")]
        public virtual Bolum Bolum { get; set; }
        [ForeignKey("OgrenciId")]
        public virtual ICollection<Ogrenci>? DersOgrencileri { get; set; }
        public virtual ICollection<IdentityUser>? Yetkililer { get; set; }
        public bool IsOnaylandi { get; set; }
        public Guid? DersOnayciId { get; set; }
        public string? DersOnayciUsername { get; set; }
        public string? DersOnayciAdi { get; set; }


    }
}
