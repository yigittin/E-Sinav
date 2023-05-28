using AutoMapper.Internal.Mappers;
using SanalVaka.DersDtos;
using SanalVaka.Dersler;
using SanalVaka.Ogrenciler;
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
        private readonly IRepository<Ogrenci> _ogrenciRepo;
        private readonly IRepository<Ders, Guid> _dersRepo;
        public SinifAppService(IRepository<Sinif, Guid> repository,
            IRepository<IdentityUser, Guid> kullaniciRepo,
            ICurrentUser currentUser,
            IRepository<Ogrenci> ogrenciRepo,
            IRepository<Ders, Guid> dersRepo)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Siniflar.Default;
            GetListPolicyName = SanalVakaPermissions.Siniflar.Default;
            CreatePolicyName = SanalVakaPermissions.Siniflar.Create;
            UpdatePolicyName = SanalVakaPermissions.Siniflar.Edit;
            DeletePolicyName = SanalVakaPermissions.Siniflar.Delete;
            _kullaniciRepo = kullaniciRepo;
            _currentUser = currentUser;
            _ogrenciRepo = ogrenciRepo;
            _dersRepo = dersRepo;
        }
        public async Task OgrenciEkleSingle(Guid guidSinif, int ogrenciId)
        {
            var entity = await Repository.FindAsync(guidSinif);
            if(entity == null)
            {
                throw new UserFriendlyException("Sinif bulunamadı!");
            }
            var entityOgrenci = await _ogrenciRepo.GetAsync(x=>x.Id==ogrenciId);
            if(entityOgrenci == null)
            {
                throw new UserFriendlyException("Öğrenci bulunamadı");
            }
            entity.SinifOgrenciler.Add(entityOgrenci);
            await Repository.UpdateAsync(entity);
        }
        public async Task SinifOnayla(Guid guidSinif)
        {
            var entity=await Repository.FindAsync(guidSinif);

            if(entity == null)
            {
                throw new UserFriendlyException("Sınıf bulunamadı");
            }
            if(_currentUser.Id is not null)
            {
                var entityKullanici=await _kullaniciRepo.FindAsync((Guid)_currentUser.Id);
                entity.IsOnaylandi=true;
                entity.SinifOnayciId = entityKullanici.Id;
                entity.SinifOnayciAdi = entityKullanici.Name;
                entity.SinifOnayciUsername = entityKullanici.UserName;
                await Repository.UpdateAsync(entity);
            }
            else
            {
                throw new UserFriendlyException("Tekrar giriş yapınız!");
            }
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
                var ogrenci = await _ogrenciRepo.GetAsync(x=>x.Id==identityUser);
                if (ogrenci is not null)
                {
                    entity.SinifOgrenciler.Add(ogrenci);
                }
            }
            await Repository.UpdateAsync(entity);
        }
        public async Task<List<SinifInfoDto>> GetSinifInfo()
        {
            var entity= await Repository.GetListAsync();
            var res=ObjectMapper.Map<List<Sinif>,List<SinifInfoDto>>(entity);
            foreach(var item in res)
            {
                var creator = await _kullaniciRepo.FindAsync(item.CreatorId);
                item.CreatorUserName = creator.UserName;
            }
            return res;
        }
        public async Task<PagedResultDto<SinifInfoDto>> GetPagedSiniflar(PagedAndSortedResultRequestDto input, string filter = null)
        {
            if (input.Sorting=="SinifName")
            {
                input.Sorting = nameof(Sinif.SinifName);
            }
            else if(input.Sorting == "SinifLimit")
            {
                input.Sorting = nameof(Sinif.SinifLimit);
            }
            else if(input.Sorting == "DersAdi")
            {
                input.Sorting = nameof(Sinif.Ders.DersAdi);
            }
            else
            {
                input.Sorting=nameof(Sinif.SinifName);
            }
            var queryable = await Repository.GetQueryableAsync();
            var entity = queryable.WhereIf
            (
                !filter.IsNullOrWhiteSpace(),
                Sinif => Sinif.SinifName.Contains(filter)
            )
            .OrderBy(x=> input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
            var infoList = new List<SinifInfoDto>();

            foreach (var item in entity)
            {
                var ders = await _dersRepo.GetAsync(item.DersId);
                if (ders is null)
                {
                    throw new UserFriendlyException("Bölüm bulunamadı");
                }
                var res = new SinifInfoDto()
                {
                    CreatorId = (Guid)item.CreatorId,
                    SinifAdi = item.SinifName,
                    OnaylayanKullaniciAdi = item.SinifOnayciAdi,
                    IsOnaylandi = item.IsOnaylandi,
                    DersId = item.DersId,
                    DersAdi = ders.DersAdi,
                    Id = item.Id,
                };
                var creator = await _kullaniciRepo.FindAsync((Guid)res.CreatorId);
                res.CreatorUserName = creator.UserName;                
                infoList.Add(res);
            }
            return new PagedResultDto<SinifInfoDto>(
                infoList.Count,
                infoList
            );
        }
        public async Task<SinifInfoDto> GetSinifSingle(Guid id)
        {
            var entity=await Repository.GetAsync(id);
            if(entity is null)
            {
                throw new UserFriendlyException("Sınıf bulunamadı");
            }
            var res = new SinifInfoDto()
            {
                SinifAdi=entity.SinifName,
                SinifLimit=entity.SinifLimit,
                DersAdi=entity.Ders.DersAdi,
                Id=entity.Id,
                IsOnaylandi=entity.IsOnaylandi,
                OnaylayanKullaniciAdi=entity.SinifOnayciAdi
            };
            return res;
            //public Guid Id { get; set; }
            //public string SinifAdi { get; set; }
            //public int SinifLimit { get; set; }
            //public Guid DersId { get; set; }
            //public string DersAdi { get; set; }
            //public bool IsOnaylandi { get; set; }
        }

    }
}
