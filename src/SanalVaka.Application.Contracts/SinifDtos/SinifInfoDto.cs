using SanalVaka.OgrenciDtos;
using SanalVaka.YetkiliDtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace SanalVaka.SinifDtos
{
    public class SinifInfoDto
    {
        public string SinifAdi { get; set; }
        public int SinifLimit { get; set; }
        public Guid DersId { get; set; }
        public string DersAdi { get; set; }
        public bool IsOnaylandi { get; set; }
        public string OnaylayanKullaniciAdi { get; set; }
        public List<OgrenciSelectionDto>? OgrenciList { get; set; }
        public List<SinifYetkiliDto>? YetkiliList { get; set; }
        public string CreatorUserName { get; set; }
        public Guid CreatorId { get; set; }
    }
}
