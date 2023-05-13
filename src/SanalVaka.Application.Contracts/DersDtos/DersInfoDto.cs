using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace SanalVaka.DersDtos
{
    public class DersInfoDto
    {
        public string Name { get; set; }
        public Guid BolumId { get; set; }
        public string BolumName { get; set; }
        public List<SinifDto> SinifList { get; set; }
        public bool IsOnaylandi { get; set; }
        public string CreatorUserName { get; set; }
        public Guid CreatorId { get; set; }
        public Guid YetkiliId { get; set; }
        public string YetkiliName { get;set; }
    }
}
