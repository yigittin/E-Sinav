using AutoMapper.Internal.Mappers;
using SanalVaka.DersDtos;
using SanalVaka.Dersler;
using SanalVaka.Many2Many;
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
        private readonly IRepository<SinifOgrenci, int> _sinifOgrenci;
        public SinifAppService(IRepository<Sinif, Guid> repository,
            IRepository<IdentityUser, Guid> kullaniciRepo,
            ICurrentUser currentUser,
            IRepository<Ogrenci> ogrenciRepo,
            IRepository<Ders, Guid> dersRepo,
            IRepository<SinifOgrenci, int> sinifOgrenci)
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
            _sinifOgrenci = sinifOgrenci;
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
                Id=entity.Id,
                DersId=entity.DersId,
                IsOnaylandi=entity.IsOnaylandi,
                OnaylayanKullaniciAdi=entity.SinifOnayciAdi
            };
            var ders = await _dersRepo.GetAsync(res.DersId);
            res.DersAdi = ders.DersAdi;
            return res;
            //public Guid Id { get; set; }
            //public string SinifAdi { get; set; }
            //public int SinifLimit { get; set; }
            //public Guid DersId { get; set; }
            //public string DersAdi { get; set; }
            //public bool IsOnaylandi { get; set; }
        }
        public async Task CreateSinif(CreateUpdateSinifDto input)
        {
            var ders=await _dersRepo.GetAsync(input.DersId); 
            if(ders is null)
            {
                throw new UserFriendlyException("Ders bulunamadı!");
            }
            var entity = new Sinif()
            {
                DersId=input.DersId,
                SinifName=input.SinifAdi,
                SinifLimit=input.SinifLimit,
                IsOnaylandi=input.IsOnaylandi,
                Ders=ders
            };
            await Repository.InsertAsync(entity);
        }
        public async Task UpdateSinifCustom(CreateUpdateSinifDto input)
        {
            var entity = await Repository.GetAsync(x=>x.Id==input.Id);
            if(entity is null)
            {
                throw new UserFriendlyException("Sınıf bulunamadı!");
            }
            var ders = await _dersRepo.GetAsync(input.DersId);
            if (ders is null)
            {
                throw new UserFriendlyException("Ders bulunamadı!");
            }
            entity.Ders= ders;
            entity.SinifName=input.SinifAdi;
            entity.SinifLimit= input.SinifLimit;

            await Repository.UpdateAsync(entity);
        }
        public async Task SinifOgrenciEkleMulti(List<Guid> list, Guid guidSinif)
        {
            var entity = await Repository.FindAsync(guidSinif);
            if (entity == null)
            {
                throw new UserFriendlyException("Sınıf bulunamadı");
            }
            var sinifOgrenciList = new List<SinifOgrenci>();
            foreach (var identityUser in list)
            {
                var ogrenci = await _kullaniciRepo.GetAsync(x => x.Id == identityUser);
                if (ogrenci is not null)
                {
                    var sinifOgrenci = new SinifOgrenci();
                    sinifOgrenci.SinifId = entity.Id;
                    sinifOgrenci.OgrenciId = ogrenci.Id;
                    sinifOgrenciList.Add(sinifOgrenci);
                }
            }
            await _sinifOgrenci.InsertManyAsync(sinifOgrenciList);
            await Repository.UpdateAsync(entity);
        }
        public async Task SinifOgrenciEkleSingle(Guid guidSinif, Guid ogrenciId)
        {
            var entity = await Repository.FindAsync(guidSinif);
            if (entity == null)
            {
                throw new UserFriendlyException("Sinif bulunamadı!");
            }
            var entityOgrenci = await _ogrenciRepo.GetAsync(x => x.UserId == ogrenciId);


            if (entityOgrenci == null)
            {
                throw new UserFriendlyException("Öğrenci bulunamadı");
            }
            var sinifOgrenci = new SinifOgrenci();
            sinifOgrenci.SinifId = entity.Id;
            sinifOgrenci.OgrenciId = entityOgrenci.UserId;
            sinifOgrenci.IsDeleted = false;
            await _sinifOgrenci.InsertAsync(sinifOgrenci);
            await Repository.UpdateAsync(entity);
        }
        public async Task SinifOgrenciCikarSingle(Guid guidSinif,Guid ogrenciId)
        {
            var ogrenciSinif=await _sinifOgrenci.GetAsync(x=>x.SinifId== guidSinif&&x.OgrenciId==ogrenciId);
            await _sinifOgrenci.DeleteAsync(ogrenciSinif);
        }
        public async Task SinifOgrenciCikarMulti(Guid guidSinif, List<Guid> list)
        {
            List<SinifOgrenci> sinifOgrenci = new List<SinifOgrenci>();
            foreach(var item in list)
            {
                var ogrenciEnt = await _sinifOgrenci.GetAsync(x => x.SinifId == guidSinif && x.OgrenciId == item);
                sinifOgrenci.Add(ogrenciEnt);
            }
            await _sinifOgrenci.DeleteManyAsync(sinifOgrenci);
        }

    }
}
