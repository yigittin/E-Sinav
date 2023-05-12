using SanalVaka.Bolumler;
using System;
using System.Collections.Generic;
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
        public List<IdentityUser> OgrenciList { get; set; }
    }
}
