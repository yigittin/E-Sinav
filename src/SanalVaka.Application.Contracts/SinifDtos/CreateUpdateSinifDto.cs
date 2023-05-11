using System;
using System.Collections.Generic;
using System.Text;

namespace SanalVaka.SinifDtos
{
    public class CreateUpdateSinifDto
    {
        public string Name { get; set; }
        public int SinifLimit { get; set; }
        public Guid DersId { get; set; }
    }
}
