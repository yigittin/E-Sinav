using System;
using System.Collections.Generic;
using System.Text;

namespace SanalVaka.SinavDtos
{
    public class CompleteSinavDto
    {
        public SinavDto Sinav { get; set; }
        public List<SoruDto> SoruList { get; set; }
    }
}
