using SanalVaka.Dersler;
using SanalVaka.SinavDtos;
using SanalVaka.SinifDtos;
using SanalVaka.Siniflar;
using SanalVaka.Sorular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SanalVaka.Sinavlar
{
    public class SinavAppService: CrudAppService<Sinav, SinavCrudDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSinavDto>, ISinavAppService
    {
        private readonly IRepository<Soru, Guid> _soruRepository;
        private readonly IRepository<Cevap, Guid> _cevapRepository;
        private readonly IRepository<Ders, Guid> _dersRepository;

        public SinavAppService(IRepository<Sinav, Guid> repository,
            IRepository<Soru, Guid> soruRepository,
            IRepository<Cevap, Guid> cevapRepository,
            IRepository<Ders, Guid> dersRepository):base(repository)
        {
            _soruRepository=soruRepository;
            _cevapRepository=cevapRepository;
            _dersRepository=dersRepository;
        }

        public async Task CreateUpdateSinav(CreateUpdateSinavDto input)
        {
            if(input.Id is null || String.IsNullOrWhiteSpace(input.Id.ToString()))
            {
                var ders = await _dersRepository.GetAsync(input.DersId);
                if(ders is null)
                {
                    throw new UserFriendlyException("Ders bulunamadı");
                }
                var sinav = new Sinav()
                {
                    SinavAdi = input.SinavAdi,
                    Agirlik = input.Agirlik,
                    SinavSure = input.SinavSure,
                    BaslangicTarih = input.BaslangicTarih,
                    DersId = input.DersId,
                    Ders = ders,
                };

                await Repository.InsertAsync(sinav);
            }
            else if(input.Id is not null)
            {
                var sinav=await Repository.GetAsync((Guid)input.Id);
                if(sinav is null)
                {
                    throw new UserFriendlyException("Sınav bulunamadı");
                }
                var ders = await _dersRepository.GetAsync(input.DersId);
                if(ders is null)
                {
                    throw new UserFriendlyException("Ders bulunamadı");
                }

                sinav.DersId = input.DersId;
                sinav.Ders = ders;
                sinav.SinavAdi=input.SinavAdi;
                sinav.SinavSure = input.SinavSure;
                sinav.BaslangicTarih = input.BaslangicTarih;
                sinav.Agirlik = input.Agirlik;

                await Repository.UpdateAsync(sinav);
            }
        }

        public async Task DeleteSinav(Guid id)
        {
            var sinav= await Repository.GetAsync(id);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı");
            }
            await Repository.DeleteAsync(sinav);
        }   

        public async Task<List<SinavDto>> GetSinavInfo()
        {
            var entityList = await Repository.GetListAsync();
            List<SinavDto> res = new List<SinavDto>();
            foreach(var item in entityList)
            {
                var sinav = new SinavDto()
                {
                    Agirlik=item.Agirlik,
                    SinavAdi=item.SinavAdi,
                    SinavSure=item.SinavSure,
                    DersId=item.DersId,
                    BaslangicTarih=item.BaslangicTarih,
                    Id= item.Id
                };
                res.Add(sinav);
            }
            return res;
        }
        public async Task<CompleteSinavDto> GetSinavDetails(Guid id)
        {
            var sinav = await Repository.GetAsync(id);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı");
            }
            var sinavDto = new SinavDto()
            {
                Agirlik = sinav.Agirlik,
                SinavAdi = sinav.SinavAdi,
                SinavSure = sinav.SinavSure,
                DersId = sinav.DersId,
                BaslangicTarih = sinav.BaslangicTarih,
                Id = sinav.Id
            };
            var soru = await _soruRepository.GetListAsync(x => x.SinavId == sinav.Id && x.IsDeleted == false);
            List<SoruDto> soruList = new List<SoruDto>();
            foreach(var item in soru)
            {
                var cevapList = await _cevapRepository.GetListAsync(x => x.SoruId == item.Id && x.IsDeleted==false);
                var cevapDtoList= new List<CevapDto>();
                foreach (var item2 in cevapList)
                {
                    var cevap = new CevapDto()
                    {
                        CevapMetni= item2.CevapMetni,
                        Id=item2.Id,
                        IsDogru=item2.IsDogru
                    };
                    cevapDtoList.Add(cevap);
                }
                item.CevapList = (ICollection<Cevap>)cevapDtoList;
            }
            var res = new CompleteSinavDto()
            {
                Sinav= sinavDto,
                SoruList=soruList
            };
            return res;
        }
    }
}
