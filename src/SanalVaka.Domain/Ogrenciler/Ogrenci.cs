using SanalVaka.Dersler;
using SanalVaka.Siniflar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;

namespace SanalVaka.Ogrenciler
{
    public class Ogrenci:FullAuditedAggregateRoot<int>
    {
        [MaxLength(30)]
        public string OgrenciNo { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual IdentityUser User { get; set; }
        public virtual ICollection<Ders> Dersler { get; set; }
        public virtual ICollection<Sinif> Siniflar { get; set; }
    }
}
