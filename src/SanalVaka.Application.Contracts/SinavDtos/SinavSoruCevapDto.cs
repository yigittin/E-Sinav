using System;
using System.Collections.Generic;
using System.Text;

namespace SanalVaka.SinavDtos
{
    public class SinavSoruCevapDto
    {
        public Guid SinavId { get; set; }
        public Guid OgrenciId { get; set; }
        public List<SoruDto> SoruList { get; set; }
    }
}
