using SanalVaka.Dersler;
using SanalVaka.SinavDtos;
using SanalVaka.Sorular;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;

namespace SanalVaka.Sinavlar
{
    public class CevapAppService:ICevapAppService
    {
        private readonly IRepository<Cevap, Guid> _repository;
        private readonly IRepository<Sinav, Guid> _sinavRepository;
        private readonly IRepository<Soru, Guid> _soruRepository;
        private readonly IRepository<Ders, Guid> _dersRepository;

        public CevapAppService(IRepository<Cevap, Guid> repository,
           IRepository<Sinav, Guid> sinavRepository,
           IRepository<Soru, Guid> soruRepository,
           IRepository<Ders, Guid> dersRepository)
        {
            _repository = repository;
            _sinavRepository = sinavRepository;
            _soruRepository = soruRepository;
            _dersRepository = dersRepository;
        }

        public async Task CreateUpdateCevap(CreateUpdateCevapDto input)
        {
            var soru = await _soruRepository.GetAsync(input.SoruId);
            if(soru is null)
            {
                throw new UserFriendlyException("Soru bulunamadı!");
            }
            if(input.Id is null)
            {
                var entity = new Cevap()
                {
                    CevapMetni=input.CevapMetni,
                    IsDogru=input.IsDogru,
                    SoruId=input.SoruId,
                    Soru=soru
                };
                await _repository.InsertAsync(entity);
            }
            else if(input.Id is not null)
            {
                var entity = await _repository.GetAsync((Guid)input.Id);
                if(entity is null)
                {
                    throw new UserFriendlyException("Cevap bulunamadı");
                }
                entity.CevapMetni = input.CevapMetni;
                entity.IsDogru = input.IsDogru;
                entity.SoruId=input.SoruId;
                entity.Soru = soru;

                await _repository.UpdateAsync(entity);
            }
        }

        public async Task DogruCevapSec(Guid id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity is null)
            {
                throw new UserFriendlyException("Cevap bulunamadı");
            }
            entity.IsDogru = true;

            await _repository.UpdateAsync(entity);
        }

        public async Task<List<CevapDto>> GetCevapList(Guid soruId)
        {
            List<CevapDto> res=new List<CevapDto> ();
            var entity = await _repository.GetListAsync(x => x.SoruId == soruId && x.IsDeleted == false);
            foreach(var item in entity)
            {
                var cevap = new CevapDto()
                {
                    CevapMetni=item.CevapMetni,
                    Id=item.Id,
                    IsDogru=item.IsDogru,
                    SoruId=item.SoruId
                };
                res.Add(cevap);
            }
            return res;
        }
    }
}
