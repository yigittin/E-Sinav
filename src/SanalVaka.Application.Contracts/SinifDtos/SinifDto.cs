using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace SanalVaka.SinifDtos
{
    public class SinifDto:AuditedEntityDto<Guid>
    {
        public string Name { get; set; }
        public int SinifLimit { get; set; }
        public Guid DersId { get; set; }
        public string DersName { get; set; }

    }
}
