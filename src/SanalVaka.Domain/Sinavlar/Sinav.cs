using SanalVaka.Dersler;
using SanalVaka.Sorular;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Sinavlar
{
    public class Sinav:FullAuditedAggregateRoot<Guid>
    {
        public string SinavAdi { get; set; }
        public double Agirlik { get; set; }
        public ICollection<Soru> SoruList { get; set; }
        public Guid DersId { get; set; }
        [ForeignKey("DersId")]
        public Ders Ders { get; set; }
        public int SinavSure { get; set; }
        public DateTime BaslangicTarih { get; set; }

    }
}
