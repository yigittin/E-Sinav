using System;
using System.Collections.Generic;
using System.Text;

namespace SanalVaka.Bolumler
{
    public class BolumInfoDto
    {
        public Guid Id { get; set; }
        public string BolumAdi { get; set; }
        public bool IsOnaylandi { get; set; }
        public Guid? CreatorId { get; set; }
        public string CreatorUserName { get; set;}
        public string? BolumOnayciAdi { get; set; }
    }
}
