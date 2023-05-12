using SanalVaka.Dersler;
using System;
using System.Collections.Generic;
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
        public List<IdentityUser> OgrenciList { get; set; }
    }
}
