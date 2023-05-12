using SanalVaka.DersDtos;
using SanalVaka.Dersler;
using SanalVaka.Permissions;
using SanalVaka.SinifDtos;
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
using Volo.Abp.Users;

namespace SanalVaka.Siniflar
{
    public class SinifAppService:CrudAppService<Sinif, SinifDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSinifDto>,ISinifAppService
    {
        private readonly IRepository<IdentityUser,Guid> _kullaniciRepo;
        private readonly ICurrentUser _currentUser;
        public SinifAppService(IRepository<Sinif, Guid> repository,IRepository<IdentityUser, Guid> kullaniciRepo,ICurrentUser currentUser)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Siniflar.Default;
            GetListPolicyName = SanalVakaPermissions.Siniflar.Default;
            CreatePolicyName = SanalVakaPermissions.Siniflar.Create;
            UpdatePolicyName = SanalVakaPermissions.Siniflar.Edit;
            DeletePolicyName = SanalVakaPermissions.Siniflar.Delete;
            _kullaniciRepo = kullaniciRepo;
            _currentUser = currentUser;
        }
        public async Task<SinifDto> OgrenciEkleSingle(Guid guidSinif, Guid ogrenciId)
        {
            var entity = await Repository.FindAsync(guidSinif);
            if(entity == null)
            {
                throw new UserFriendlyException("Sinif bulunamadı!");
            }
            var entityOgrenci = await _kullaniciRepo.FindAsync(ogrenciId);
            if(entityOgrenci == null)
            {
                throw new UserFriendlyException("Öğrenci bulunamadı");
            }
            entity.OgrenciList.Add(entityOgrenci);
            await Repository.UpdateAsync(entity);

            return ObjectMapper.Map<Sinif,SinifDto>(entity);

        }

        public async Task<SinifDto> SinifOnayla(Guid guidSinif)
        {
            var entity=await Repository.FindAsync(guidSinif);

            if(entity == null)
            {
                throw new UserFriendlyException("Sınıf bulunamadı");
            }
            var entityKullanici=await _kullaniciRepo.FindAsync((Guid)_currentUser.Id);
            entity.IsOnaylandi=true;
            entity.OnaylayanKullanici = entityKullanici;
            await Repository.UpdateAsync(entity);

            return ObjectMapper.Map<Sinif, SinifDto>(entity);
        }

        public async Task<SinifDto> OgrenciEkleMulti(List<Guid> list,Guid guidSinif)
        {
            var entity = await Repository.FindAsync(guidSinif);
            if(entity == null)
            {
                throw new UserFriendlyException("Sınıf bulunamadı");
            }
            foreach (var identityUser in list)
            {
                var user=await _kullaniciRepo.FindAsync(identityUser);
                if(user is not null)
                {
                    entity.OgrenciList.Add(user);
                }
            }
            await Repository.UpdateAsync(entity);
            return ObjectMapper.Map<Sinif,SinifDto>(entity);
        }


    }
}
