using SanalVaka.Bolumler;
using SanalVaka.Ogrenciler;
using SanalVaka.Siniflar;
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
        public string Name { get; set; }
        public Bolum Bolum { get; set; }
        public ICollection<Ogrenci> DersOgrenciList { get; set; }
        public ICollection<DersYetkili> DersYetkiliList { get; set; }

    }
}
