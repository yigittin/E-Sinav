using SanalVaka.Dersler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Siniflar
{
    public class Sinif:AuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public int SinifLimit { get; set; }
        public Ders Ders { get; set; }
    }
}
