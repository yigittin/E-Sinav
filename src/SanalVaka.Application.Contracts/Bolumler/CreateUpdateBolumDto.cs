using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;

namespace SanalVaka.Bolumler
{
    public class CreateUpdateBolumDto
    {
        [Required]
        [StringLength(128)]
        public string Name { get; set; }
    }
}
