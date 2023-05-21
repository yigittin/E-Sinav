using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace SanalVaka.DersDtos
{
    public class DersInfoDto
    {
        public Guid Id { get; set; }
        public string DersAdi { get; set; }
        public Guid BolumId { get; set; }
        public string BolumName { get; set; }
        public bool IsOnaylandi { get; set; }
        public Guid? CreatorId { get; set; }
        public string CreatorUserName { get; set; }
        public string? DersOnayciAdi { get; set; }
        public Guid DersOnayciId { get; set; }
        public List<Guid> YetkiliId { get; set; }
        public virtual List<IdentityUserDto> Yetkililer { get;set; }
    }
}
