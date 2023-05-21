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
using Volo.Abp.Linq;
using System.Linq.Dynamic.Core;
using SanalVaka.Siniflar;

namespace SanalVaka.Dersler
{
    public class DersAppService:CrudAppService<Ders,DersDto, Guid, PagedAndSortedResultRequestDto,CreateUpdateDersDto>,IDersAppService
    {
        private readonly IRepository<IdentityUser, Guid> _kullaniciRepo;
        private readonly IRepository<Ogrenci> _ogrenciRepo;
        private readonly ICurrentUser _currentUser;
        private readonly IRepository<Bolum,Guid> _bolumRepository;
        public DersAppService(IRepository<Ders, Guid> repository, IRepository<IdentityUser, Guid> kullaniciRepo, ICurrentUser currentUser,IRepository<Ogrenci> ogrenciRepo,IRepository<Bolum, Guid> bolumRepo)
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
            _bolumRepository = bolumRepo;
        }
        public async Task<List<DersInfoDto>> GetDersInfo(int skipCount,
        int maxResultCount,
        string sorting="DersAdi",
        string filter = null)
        {
            var queryable = await Repository.GetQueryableAsync();

            var entity = queryable.WhereIf(
            !filter.IsNullOrWhiteSpace(),
            Ders => Ders.DersAdi.Contains(filter)
            ).OrderBy(sorting)
            .Skip(skipCount)
            .Take(maxResultCount);


            var infoList = new List<DersInfoDto>();
            foreach (var item in entity)
            {
                var res = new DersInfoDto()
                {
                    CreatorId = item.CreatorId,
                    DersAdi = item.DersAdi,
                    DersOnayciAdi = item.DersOnayciAdi,
                    IsOnaylandi = item.IsOnaylandi,
                    BolumId = item.BolumId,
                    BolumName=item.Bolum.BolumAdi,
                    Id = item.Id,
                };
                if (res.CreatorId is not null)
                {
                    var creator = await _kullaniciRepo.FindAsync((Guid)res.CreatorId);
                    res.CreatorUserName = creator.UserName;
                }
                infoList.Add(res);
            }
            return infoList;
        }

        public async Task CreateDers(CreateUpdateDersDto input)
        {
            var bolum = await _bolumRepository.FindAsync(input.BolumId);
            if(bolum is null)
            {
                throw new UserFriendlyException("Bölüm bulunamadı");
            }

            var ders = new Ders();
            ders.DersAdi = input.DersAdi;
            ders.BolumId= input.BolumId;
            ders.Bolum = bolum;
            ders.IsOnaylandi = false;

            await Repository.InsertAsync(ders);
        }

        public async Task UpdateDers(UpdateDersDto input)
        {
            var entity = await Repository.GetAsync(input.Id);
            if(entity is null)
            {
                throw new UserFriendlyException("Ders Bulunamadı!");
            }
            var yetkiliList = new List<IdentityUser>();
            foreach(var item in input.YetkiliId)
            {
                var yetkili=await _kullaniciRepo.GetAsync(item);
                if(yetkili is null)
                {
                    throw new UserFriendlyException("Yetkili Bulunamadı!");
                }
                yetkiliList.Add(yetkili);
            }
            var bolum=await _bolumRepository.GetAsync(input.BolumId);
            if(bolum is null)
            {
                throw new UserFriendlyException("Bölüm Bulunamadı!");
            }
            entity.Bolum = bolum;
            entity.Yetkililer = yetkiliList;
            entity.DersAdi = input.DersAdi;
            entity.BolumId=input.BolumId;
            
            await Repository.UpdateAsync(entity);

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

        public async Task YetkiliEkle(Guid dersId,List<Guid> yetkiliId)
        {
            var entity=await Repository.FindAsync(dersId);
            if(entity is null)
            {
                throw new UserFriendlyException("Ders bulunamadı");
            }

            foreach(var item in  yetkiliId)
            {
                var yetkili = await _kullaniciRepo.GetAsync(item);
                if(yetkili is null)
                {
                    throw new UserFriendlyException("Kullanıcı bulunamadı");
                }
                entity.Yetkililer.Add(yetkili);
            }

            await Repository.UpdateAsync(entity);
        }

        public async Task<List<DersInfoDto>> GetDersDropdown()
        {
            var entity = await Repository.GetListAsync();
            
            var res=new List<DersInfoDto>();
            foreach (var item in entity) 
            { 
                var dersInfo=new DersInfoDto();

                dersInfo.DersAdi = item.DersAdi;
                dersInfo.DersOnayciAdi=item.DersOnayciAdi;
                dersInfo.DersOnayciId=item.DersOnayciId;
                dersInfo.BolumId=item.BolumId;
                dersInfo.BolumName = item.Bolum.BolumAdi;
                dersInfo.CreatorId = item.CreatorId;
                dersInfo.Yetkililer = (List<IdentityUserDto>)item.Yetkililer;
                res.Add(dersInfo);
            }
            return res;
        }
        public async Task<PagedResultDto<DersInfoDto>> GetPagedDersler(PagedAndSortedResultRequestDto input, string filter = null)
        {
            var queryable = await Repository.GetQueryableAsync();
            var entity = queryable.WhereIf
            (
                !filter.IsNullOrWhiteSpace(),
                Ders => Ders.DersAdi.Contains(filter)
            )
            //.OrderBy(input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
            var infoList = new List<DersInfoDto>();

            foreach (var item in entity)
            {
                var res = new DersInfoDto()
                {
                    CreatorId = item.CreatorId,
                    DersAdi = item.DersAdi,
                    DersOnayciAdi = item.DersOnayciAdi,
                    IsOnaylandi = item.IsOnaylandi,
                    BolumId = item.BolumId,
                    BolumName = item.Bolum.BolumAdi,
                    Id = item.Id,
                };
                if (res.CreatorId is not null)
                {
                    var creator = await _kullaniciRepo.FindAsync((Guid)res.CreatorId);
                    res.CreatorUserName = creator.UserName;

                }
                infoList.Add(res);
            }
            return new PagedResultDto<DersInfoDto>(
                infoList.Count,
                infoList
            );
        }
    }

}

