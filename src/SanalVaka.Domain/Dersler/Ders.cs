using SanalVaka.Bolumler;
using SanalVaka.Ogrenciler;
using SanalVaka.Yetkililer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Dersler
{
    public class Ders:FullAuditedAggregateRoot<Guid>
    {
        public string DersAdi { get; set; }
        public Guid BolumId { get; set; }
        [ForeignKey("BolumId")]
        public virtual Bolum Bolum { get; set; }
        public virtual ICollection<Ogrenci> DersOgrencileri { get; set; }
        public virtual ICollection<DersYetkili> Yetkililer { get; set; }


    }
}
