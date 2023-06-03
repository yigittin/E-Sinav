using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.SinavDtos
{
    public class SoruDto
    {
        public Guid Id { get; set; }
        public string SoruMetni { get; set; }
        public Guid SinavId { get; set; }
        public List<CevapDto>? CevapList { get; set; }
        public double Puan { get; set; }
    }
}
