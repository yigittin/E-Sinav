﻿using SanalVaka.OgrenciDtos;
using SanalVaka.YetkiliDtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace SanalVaka.SinifDtos
{
    public class SinifDto:AuditedEntityDto<Guid>
    {
        public string SinifAdi { get; set; }
        public int SinifLimit { get; set; }
        public Guid DersId { get; set; }
        public string DersAdi { get; set; }
        public bool IsOnaylandi { get; set; }
        public string OnaylayanKullaniciAdi { get; set; }
        public ICollection<OgrenciDto> OgrenciList { get; set; }
        public ICollection<SinifYetkiliDto> YetkiliList { get; set; }


    }
}
