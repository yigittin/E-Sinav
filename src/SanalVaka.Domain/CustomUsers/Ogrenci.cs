using SanalVaka.Bolumler;
using SanalVaka.Dersler;
using SanalVaka.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Identity;

namespace SanalVaka.Ogrenciler
{
    public class Ogrenci:IdentityUser
    {
        public string OgrenciNo { get; set; }
        public ICollection<Bolum> OgrenciBolumList { get; set; }
        public ICollection<Ders> OgrenciDersList { get; set; }
        public ICollection<Sinif> OgrenciSinifList { get; set; }
    }
}
