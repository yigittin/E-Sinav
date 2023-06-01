using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanalVaka.SinavDtos
{
    public class CreateUpdateSoruDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string SoruMetni { get; set; }
        [Required]
        public Guid SinavId { get; set; }
    }
}
