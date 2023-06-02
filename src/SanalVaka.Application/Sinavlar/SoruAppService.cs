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
using Volo.Abp.Domain.Repositories;

namespace SanalVaka.Sinavlar
{
    public class SoruAppService:ISoruAppService
    {
        private readonly IRepository<Soru, Guid> _repository;
        private readonly IRepository<Sinav, Guid> _sinavRepository;
        private readonly IRepository<Cevap, Guid> _cevapRepository;
        private readonly IRepository<Ders, Guid> _dersRepository;

        public SoruAppService(IRepository<Soru, Guid> repository,
           IRepository<Sinav, Guid> sinavRepository,
           IRepository<Cevap, Guid> cevapRepository,
           IRepository<Ders, Guid> dersRepository)
        {
            _repository = repository;
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

                await _repository.InsertAsync(entity);
            }
            else if(input.Id is not null)
            {
                var entity = await _repository.GetAsync((Guid)input.Id);
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

                await _repository.UpdateAsync(entity);
            }
        }
        public async Task DeleteSoru(Guid id)
        {
            var soru = await _repository.GetAsync(id);
            if (soru is null)
            {
                throw new UserFriendlyException("Soru bulunamadı");
            }
            await _repository.DeleteAsync(soru);
        }
    }
}
