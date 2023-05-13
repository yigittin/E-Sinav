using SanalVaka.Bolumler;
using SanalVaka.CustomUsers;
using SanalVaka.Dersler;
using SanalVaka.Ogrenciler;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.Identity;
using Volo.Abp.Users;
using static Volo.Abp.Identity.Settings.IdentitySettingNames;

namespace SanalVaka.Siniflar
{
    public class Sinif:FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; set; }
        public int SinifLimit { get; set; }
        public Ders Ders { get; set; }
        public bool IsOnaylandi { get; set; }
        public IdentityUser OnaylayanKullanici { get; set; }
        public ICollection<Ogrenci> SinifOgrenciList { get; set; }
        public ICollection<SinifYetkili> SinifYetkiliList { get; set; }
    }
}
