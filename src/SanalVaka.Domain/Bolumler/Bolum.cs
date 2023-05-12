using SanalVaka.Dersler;
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
        public string Name { get; set; }
        public bool IsOnaylandi { get; set; }
        public List<Ders> DersList { get; set; }

    }
}
