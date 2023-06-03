using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanalVaka.SinavDtos
{
    public class CreateUpdateSinavDto
    {
        public Guid? Id { get; set; }
        [Required]
        public string SinavAdi { get; set; }
        [Required]
        public Guid DersId { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime BaslangicTarih { get; set; }
        [Required]
        public int SinavSure { get; set; }
        [Required]
        public double Agirlik { get; set; }
    }
}
