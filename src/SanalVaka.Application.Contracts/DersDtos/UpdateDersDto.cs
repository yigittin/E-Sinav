using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Identity;

namespace SanalVaka.DersDtos
{
    public class UpdateDersDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        [StringLength(128)]
        public string DersAdi { get; set; }
        [Required]
        public Guid BolumId { get; set; }
        public List<Guid>? YetkiliId { get; set; }
        public bool IsOnaylandi { get; set; }
    }
}
