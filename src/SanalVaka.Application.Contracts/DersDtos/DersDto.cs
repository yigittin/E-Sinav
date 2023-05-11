using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace SanalVaka.DersDtos
{
    public class DersDto:AuditedEntityDto<Guid>
    {
         public string Name { get; set; }
        public Guid BolumId { get; set; }
        public string BolumName { get; set; }
    }
}
