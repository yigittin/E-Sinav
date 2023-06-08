using SanalVaka.Sinavlar;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Sorular
{
    public class Soru:FullAuditedAggregateRoot<Guid>
    {
        public string SoruMetni { get; set; }
        public double Puan { get; set; }
        public ICollection<Cevap> CevapList { get; set; }
        public Guid SinavId { get; set; }
        [ForeignKey("SinavId")]
        public Sinav Sinav { get; set; }
        
    }
}
