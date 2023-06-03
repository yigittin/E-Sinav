using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace SanalVaka.SinavDtos
{
    public class SinavDto
    {
        public Guid Id { get; set; }
        public string SinavAdi { get; set; }
        public List<SoruDto>? SoruList { get; set; }
        public Guid DersId { get; set; }
        public DateTime BaslangicTarih { get; set; }
        public int SinavSure { get; set; }
        public double Agirlik { get; set; }
        public string DersAdi { get; set; }

    }
}
