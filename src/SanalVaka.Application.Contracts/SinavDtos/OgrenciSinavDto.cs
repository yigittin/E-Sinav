using SanalVaka.OgrenciDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanalVaka.SinavDtos
{
    public class OgrenciSinavDto
    {
        public Guid OgrenciId { get; set; }
        public Guid SinavId { get; set; }
        public DateTime Baslangic { get; set; }
        public DateTime Bitis { get; set; }
        public SinavSoruCevapDto SinavSorular { get; set; }

    }
}
