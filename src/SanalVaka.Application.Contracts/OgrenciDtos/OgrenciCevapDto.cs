using SanalVaka.SinavDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanalVaka.OgrenciDtos
{
    public class OgrenciCevapDto
    {
        public Guid SoruId { get; set; }
        public List<CevapDto> CevapList { get; set; }
        public Guid OgrenciCevapId { get; set; }
    }
}
