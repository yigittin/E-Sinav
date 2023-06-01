using SanalVaka.Bolumler;
using SanalVaka.Dersler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Sorular
{
    public class Cevap:FullAuditedAggregateRoot<Guid>
    {
        public string CevapMetni { get; set; }
        public bool IsDogru { get; set; }
        public Guid SoruId { get; set; }
        [ForeignKey("SoruId")]
        public Soru Soru { get; set; }
    }
}
