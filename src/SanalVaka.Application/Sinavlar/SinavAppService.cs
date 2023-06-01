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
    public class SinavAppService:ISinavAppService
    {
        private readonly IRepository<Sinav, Guid> _repository;
        private readonly IRepository<Soru, Guid> _soruRepository;
        private readonly IRepository<Cevap, Guid> _cevapRepository;
        private readonly IRepository<Ders, Guid> _dersRepository;

        public SinavAppService(IRepository<Sinav, Guid> repository,
            IRepository<Soru, Guid> soruRepository,
            IRepository<Cevap, Guid> cevapRepository,
            IRepository<Ders, Guid> dersRepository)
        {
            _repository = repository;
            _soruRepository=soruRepository;
            _cevapRepository=cevapRepository;
            _dersRepository=dersRepository;
        }

        public async Task CreateUpdateSinav(CreateUpdateSinavDto input)
        {
            if(input.Id is null)
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

                await _repository.InsertAsync(sinav);
            }
            else if(input.Id is not null)
            {
                var sinav=await _repository.GetAsync((Guid)input.Id);
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

                await _repository.UpdateAsync(sinav);
            }
        }

        public async Task DeleteSinav(Guid id)
        {
            var sinav= await _repository.GetAsync(id);
            if(sinav is null)
            {
                throw new UserFriendlyException("Sınav bulunamadı");
            }
            await _repository.DeleteAsync(sinav);
        }

    }
}
