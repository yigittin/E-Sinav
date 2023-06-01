using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace SanalVaka.SinavDtos
{
    public class CevapDto
    {
        public Guid Id { get; set; }
        public string CevapMetni { get; set; }
        public Guid SoruId { get; set; }
        public bool IsDogru { get; set; }
    }
}
