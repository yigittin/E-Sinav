using SanalVaka.Bolumler;
using SanalVaka.DersDtos;
using SanalVaka.Ogrenciler;
using SanalVaka.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.Users;

namespace SanalVaka.Dersler
{
    public class DersAppService:CrudAppService<Ders,DersDto, Guid, PagedAndSortedResultRequestDto,CreateUpdateDersDto>,IDersAppService
    {
        private readonly IRepository<IdentityUser, Guid> _kullaniciRepo;
        private readonly IRepository<Ogrenci> _ogrenciRepo;
        private readonly ICurrentUser _currentUser;
        public DersAppService(IRepository<Ders, Guid> repository, IRepository<IdentityUser, Guid> kullaniciRepo, ICurrentUser currentUser,IRepository<Ogrenci> ogrenciRepo)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Dersler.Default;
            GetListPolicyName = SanalVakaPermissions.Dersler.Default;
            CreatePolicyName = SanalVakaPermissions.Dersler.Create;
            UpdatePolicyName = SanalVakaPermissions.Dersler.Edit;
            DeletePolicyName = SanalVakaPermissions.Dersler.Delete;
            _kullaniciRepo = kullaniciRepo;
            _currentUser = currentUser;
            _ogrenciRepo= ogrenciRepo;  
        }
        public async Task<List<DersInfoDto>> GetDersInfo()
        {
            var entity = await Repository.GetListAsync();
            var res = ObjectMapper.Map<List<Ders>, List<DersInfoDto>>(entity);
            foreach (var item in res)
            {
                var creator = await _kullaniciRepo.FindAsync(item.CreatorId);
                item.CreatorUserName = creator.UserName;
            }
            return res;
        }

        public async Task OgrenciEkleSingle(Guid guidSinif, int ogrenciId)
        {
            var entity = await Repository.FindAsync(guidSinif);
            if (entity == null)
            {
                throw new UserFriendlyException("Sinif bulunamadı!");
            }
            var entityOgrenci = await _ogrenciRepo.GetAsync(x => x.Id == ogrenciId);
            if (entityOgrenci == null)
            {
                throw new UserFriendlyException("Öğrenci bulunamadı");
            }
            entity.DersOgrencileri.Add(entityOgrenci);
            await Repository.UpdateAsync(entity);
        }

        public async Task OgrenciEkleMulti(List<int> list, Guid guidSinif)
        {
            var entity = await Repository.FindAsync(guidSinif);
            if (entity == null)
            {
                throw new UserFriendlyException("Sınıf bulunamadı");
            }

            foreach (var identityUser in list)
            {
                var ogrenci = await _ogrenciRepo.GetAsync(x => x.Id == identityUser);
                if (ogrenci is not null)
                {
                    entity.DersOgrencileri.Add(ogrenci);
                }
            }
            await Repository.UpdateAsync(entity);
        }

        public async Task DersOnayla(Guid guidDers)
        {
            var entity = await Repository.FindAsync(guidDers);

            if (entity == null)
            {
                throw new UserFriendlyException("Sınıf bulunamadı");
            }
            if (_currentUser.Id is not null)
            {
                var entityKullanici = await _kullaniciRepo.FindAsync((Guid)_currentUser.Id);
                entity.IsOnaylandi = true;
                entity.DersOnayciId = entityKullanici.Id;
                entity.DersOnayciAdi = entityKullanici.Name;
                entity.DersOnayciUsername = entityKullanici.UserName;
                await Repository.UpdateAsync(entity);
            }
            else
            {
                throw new UserFriendlyException("Tekrar giriş yapınız!");
            }
        }
    }

}

