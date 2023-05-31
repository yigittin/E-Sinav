﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Many2Many
{
    public class DersOgrenci: FullAuditedAggregateRoot<int>
    {
        public Guid DersId { get; set; }
        public Guid OgrenciId { get; set; }

    }
}
