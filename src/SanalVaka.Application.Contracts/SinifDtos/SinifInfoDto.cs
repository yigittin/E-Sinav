using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace SanalVaka.SinifDtos
{
    public class SinifInfoDto
    {
        public string Name { get; set; }
        public int SinifLimit { get; set; }
        public Guid DersId { get; set; }
        public string DersName { get; set; }
        public bool IsOnaylandi { get; set; }
        public string OnaylayanKullaniciAdi { get; set; }
        public List<IdentityUserDto> OgrenciList { get; set; }
        public string CreatorUserName { get; set; }
        public Guid CreatorId { get; set; }
        public Guid YetkiliId { get; set; }
        public string YetkiliName { get; set; }
    }
}
