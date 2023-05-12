using System;
using System.Collections.Generic;
using System.Text;

namespace SanalVaka.Bolumler
{
    public class BolumInfoDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsOnaylandi { get; set; }
        public Guid CreatorId { get; set; }
    }
}
