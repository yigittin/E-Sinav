using AutoMapper;
using SanalVaka.Bolumler;
using SanalVaka.DersDtos;
using SanalVaka.Dersler;
using SanalVaka.OgrenciDtos;
using SanalVaka.Ogrenciler;
using SanalVaka.SinifDtos;
using SanalVaka.Siniflar;
using SanalVaka.YetkiliDtos;

namespace SanalVaka;

public class SanalVakaApplicationAutoMapperProfile : Profile
{
    public SanalVakaApplicationAutoMapperProfile()
    {
        CreateMap<Bolum, BolumDto>();
        CreateMap<CreateUpdateBolumDto, Bolum>();

        CreateMap<Ders, DersDto>();
        CreateMap<CreateUpdateDersDto, Ders>();

        CreateMap<Sinif, SinifDto>();
        CreateMap<CreateUpdateSinifDto, Sinif>();

        CreateMap<Ogrenci, OgrenciDto>();
        CreateMap<CreateUpdateOgrenciDto, Ogrenci>();
    }
}
