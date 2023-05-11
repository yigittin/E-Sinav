using AutoMapper;
using SanalVaka.Bolumler;

namespace SanalVaka;

public class SanalVakaApplicationAutoMapperProfile : Profile
{
    public SanalVakaApplicationAutoMapperProfile()
    {
        CreateMap<Bolum, BolumDto>();
        CreateMap<CreateUpdateBolumDto, Bolum>();
    }
}
