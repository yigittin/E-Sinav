using SanalVaka.Dersler;
using SanalVaka.SinavDtos;
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
    public class SoruAppService:CrudAppService<Soru,SoruCrudDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSoruDto>,ISoruAppService
    {
        private readonly IRepository<Sinav, Guid> _sinavRepository;
        private readonly IRepository<Cevap, Guid> _cevapRepository;
        private readonly IRepository<Ders, Guid> _dersRepository;

        public SoruAppService(IRepository<Soru, Guid> repository,
           IRepository<Sinav, Guid> sinavRepository,
           IRepository<Cevap, Guid> cevapRepository,
           IRepository<Ders, Guid> dersRepository):base(repository)
        {
            _sinavRepository = sinavRepository;
            _cevapRepository = cevapRepository;
            _dersRepository = dersRepository;
        }

        public async Task CreateUpdateSoru(CreateUpdateSoruDto input)
        {
            if(input.Id is null)
            {
                var sinav = await _sinavRepository.GetAsync(input.SinavId);
                if(sinav is null)
                {
                    throw new UserFriendlyException("Sınav bulunamadı");
                }
                var entity = new Soru()
                {
                    SoruMetni=input.SoruMetni,
                    Puan=input.Puan,
                    SinavId=input.SinavId,
                    Sinav=sinav,
                };

                await Repository.InsertAsync(entity);
            }
            else if(input.Id is not null)
            {
                var entity = await Repository.GetAsync((Guid)input.Id);
                if(entity is null)
                {
                    throw new UserFriendlyException("Soru bulunamadı");
                }
                var sinav = await _sinavRepository.GetAsync(input.SinavId);
                if (sinav is null)
                {
                    throw new UserFriendlyException("Sınav bulunamadı");
                }
                entity.SinavId= input.SinavId;
                entity.Sinav = sinav;
                entity.SoruMetni= input.SoruMetni;
                entity.Puan = input.Puan;

                await Repository.UpdateAsync(entity);
            }
        }
        public async Task DeleteSoru(Guid id)
        {
            var soru = await Repository.GetAsync(id);
            if (soru is null)
            {
                throw new UserFriendlyException("Soru bulunamadı");
            }
            await Repository.DeleteAsync(soru);
        }
        public async Task<List<SoruDto>> GetSoruListBySinavId(Guid id)
        {
            var entity = await Repository.GetListAsync(x=>x.Id==id&&x.IsDeleted==false);
            var res = new List<SoruDto>();
            foreach(var item in entity)
            {
                var soru = new SoruDto()
                {
                    Id = item.Id,
                    SinavId=item.SinavId,
                    SoruMetni=item.SoruMetni,
                    Puan=item.Puan
                };
                res.Add(soru);
            }
            return res;
        }
    }
}
