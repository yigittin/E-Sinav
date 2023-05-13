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

namespace SanalVaka.Bolumler
{
    public class Bolum : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public bool IsOnaylandi { get; set; }
        public ICollection<Ogrenci> OgrenciList { get; set; }
        public ICollection<BolumYetkili> BolumYetkiliList { get; set; }

    }
}
