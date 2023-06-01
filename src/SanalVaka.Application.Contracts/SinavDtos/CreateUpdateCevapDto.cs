using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanalVaka.SinavDtos
{
    public class CreateUpdateCevapDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string CevapMetni { get; set; }
        [Required]
        public bool IsDogru { get; set; }
        [Required]
        public Guid SoruId { get; set; }
    }
}
