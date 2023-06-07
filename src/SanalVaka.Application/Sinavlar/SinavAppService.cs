using SanalVaka.Dersler;
using SanalVaka.OgrenciDtos;
using SanalVaka.Ogrenciler;
using SanalVaka.SinavDtos;
using SanalVaka.SinifDtos;
using SanalVaka.Siniflar;
using SanalVaka.Sorular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace SanalVaka.Sinavlar
{
    public class SinavAppService: CrudAppService<Sinav, SinavCrudDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSinavDto>, ISinavAppService
    {
        private readonly IRepository<Soru, Guid> _soruRepository;
        private readonly IRepository<Cevap, Guid> _cevapRepository;
        private readonly IRepository<Ders, Guid> _dersRepository;
        private readonly IRepository<OgrenciSinav, int> _ogrenciSinavRepo;
        private readonly IRepository<OgrenciCevap, int> _ogrenciCevapRepo;
        private readonly ICurrentUser _currentUser;

        public SinavAppService(IRepository<Sinav, Guid> repository,
            IRepository<Soru, Guid> soruRepository,
            IRepository<Cevap, Guid> cevapRepository,
            IRepository<Ders, Guid> dersRepository,
            ICurrentUser curUser,
            IRepository<OgrenciSinav, int> ogrenciSinavRepo,
            IRepository<OgrenciCevap, int> ogrenciCevapRepo) :base(repository)
        {
            _soruRepository=soruRepository;
            _cevapRepository=cevapRepository;
            _dersRepository=dersRepository;
            _currentUser = curUser;
            _ogrenciCevapRepo = ogrenciCevapRepo;
            _ogrenciSinavRepo = ogrenciSinavRepo;
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
                var ders = await _dersRepository.GetAsync(item.DersId);
                var sinav = new SinavDto()
                {
                    Agirlik=item.Agirlik,
                    SinavAdi=item.SinavAdi,
                    SinavSure=item.SinavSure,
                    DersId=item.DersId,
                    BaslangicTarih=item.BaslangicTarih,
                    Id= item.Id,
                    DersAdi=ders.DersAdi
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
            var ders=await _dersRepository.GetAsync(sinav.DersId);
            var sinavDto = new SinavDto()
            {
                Agirlik = sinav.Agirlik,
                SinavAdi = sinav.SinavAdi,
                SinavSure = sinav.SinavSure,
                DersId = sinav.DersId,
                BaslangicTarih = sinav.BaslangicTarih,
                Id = sinav.Id,
                DersAdi=ders.DersAdi
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
        public async Task<PagedResultDto<SinavDto>> GetPagedSiniflar(PagedAndSortedResultRequestDto input,string filter=null)
        {
            var queryable = await Repository.GetQueryableAsync();
            var entity = queryable.WhereIf
                (
                    !filter.IsNullOrWhiteSpace(), Sinav => Sinav.SinavAdi.Contains(filter)
                )
                .OrderBy(x => x.BaslangicTarih)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);
            List<SinavDto> res = new List<SinavDto>();
            
            foreach (var item in entity)
            {
                var ders = await _dersRepository.GetAsync(item.DersId);
                var sinav = new SinavDto()
                {
                    Agirlik = item.Agirlik,
                    SinavAdi = item.SinavAdi,
                    SinavSure = item.SinavSure,
                    DersId = item.DersId,
                    BaslangicTarih = item.BaslangicTarih,
                    Id = item.Id,
                    DersAdi=ders.DersAdi
                };
                res.Add(sinav);
            }
            return new PagedResultDto<SinavDto>(
                res.Count,
                res
            );
        }
        public async Task<SinavDto> GetSinavSingle(Guid id)
        {
            var sinav = await Repository.GetAsync(id);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı");
            }
            var ders = await _dersRepository.GetAsync(sinav.DersId);
            var res = new SinavDto()
            {
                Agirlik=sinav.Agirlik,
                BaslangicTarih=sinav.BaslangicTarih,
                DersAdi=ders.DersAdi,
                DersId=sinav.DersId,
                Id=sinav.Id,
                SinavAdi=sinav.SinavAdi,
                SinavSure = sinav.SinavSure
            };
            return res;
        }

        public async Task SinavBaslat(Guid sinavId)
        {
            var ogrenciSinav=await _ogrenciSinavRepo.GetAsync(x=>x.SinavId==sinavId&&x.OgrenciId==_currentUser.Id);
            var sinav = await Repository.GetAsync(sinavId);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı!");
            }
            if(ogrenciSinav is not null)
            {
                if (ogrenciSinav.Bitis < DateTime.Now)
                {
                    throw new UserFriendlyException("Sınav süreniz dolmuştur");
                }
            }
            else if(ogrenciSinav is null)
            {
                var yeniGiris = new OgrenciSinav()
                {
                    OgrenciId= (Guid)_currentUser.Id,
                    Baslangic=DateTime.Now,
                    Bitis=DateTime.Now.AddMinutes(sinav.SinavSure)
                };
                await _ogrenciSinavRepo.InsertAsync(yeniGiris);
            }
        }
        public async Task<OgrenciSinavDto> GetOgrenciSinavSure(Guid sinavId)
        {
            var sinav = await Repository.GetAsync(sinavId);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı!");
            }
            var sinavOgrenci = await _ogrenciSinavRepo.GetAsync(x => x.OgrenciId == _currentUser.GetId() && x.SinavId == sinavId);
            var sorular = await _soruRepository.GetListAsync(x => x.SinavId == sinavId && x.IsDeleted == false);
            List<OgrenciCevapDto> ogrenciCevap = new List<OgrenciCevapDto>();
            foreach(var item in sorular)
            {
                var cevap = await _cevapRepository.GetListAsync(x => x.SoruId == item.Id && x.IsDeleted == false);
                List<CevapDto> cevapDtoList=new List<CevapDto>();
                foreach(var item2 in cevap)
                {
                    CevapDto cDto = new CevapDto()
                    {
                        Id=item2.Id,
                        SoruId=item.Id,
                        CevapMetni=item2.CevapMetni,
                        IsDogru=item2.IsDogru
                    };
                    cevapDtoList.Add(cDto);
                }
                var ogrenciCevaplar = await _ogrenciCevapRepo.GetListAsync(x => x.SoruId == item.Id);
                foreach (var item2 in ogrenciCevaplar)
                {
                    OgrenciCevapDto oCDto = new OgrenciCevapDto()
                    {
                        CevapList= cevapDtoList,
                        OgrenciCevapId=item2.CevapId,
                        SoruId= item2.SoruId,
                    };
                    ogrenciCevap.Add(oCDto);
                }
            }
            OgrenciSinavDto res = new OgrenciSinavDto()
            {
                Baslangic = sinavOgrenci.Baslangic,
                Bitis = sinavOgrenci.Bitis,
                SinavId = sinavId,
                OgrenciId = _currentUser.GetId(),
                OgrenciCevaplar = ogrenciCevap
            };
            return res;
        }
        
    }
}
