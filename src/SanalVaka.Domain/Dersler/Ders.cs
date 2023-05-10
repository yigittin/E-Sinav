using SanalVaka.Bolumler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Dersler
{
    public class Ders:AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public Bolum Bolum { get; set; }
    }
}
