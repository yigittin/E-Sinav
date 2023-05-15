﻿using SanalVaka.Bolumler;
using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace SanalVaka.YetkiliDtos
{
    public class CreateUpdateBolumYetkiliDto
    {
        [Required]
        public string OgretmenNo { get; set; }
        [Required]
        public Guid UserId { get; set; }
        public ICollection<BolumDto> Bolumler { get; set; }
    }
}
