using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace SanalVaka.Bolumler
{
    public class BolumDto:AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
    }
}
