using AutoMapper.Internal.Mappers;
using SanalVaka.DersDtos;
using SanalVaka.Dersler;
using SanalVaka.Many2Many;
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
        private readonly IRepository<SinifUser> _sinifUserRepo;
        public SinifAppService(IRepository<Sinif, Guid> repository,IRepository<IdentityUser, Guid> kullaniciRepo,ICurrentUser currentUser,IRepository<SinifUser> sinifUserRepo)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Siniflar.Default;
            GetListPolicyName = SanalVakaPermissions.Siniflar.Default;
            CreatePolicyName = SanalVakaPermissions.Siniflar.Create;
            UpdatePolicyName = SanalVakaPermissions.Siniflar.Edit;
            DeletePolicyName = SanalVakaPermissions.Siniflar.Delete;
            _kullaniciRepo = kullaniciRepo;
            _currentUser = currentUser;
            _sinifUserRepo = sinifUserRepo;
        }
        public async Task OgrenciEkleSingle(Guid guidSinif, Guid ogrenciId)
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
            var sinifUser = new SinifUser();
            //sinifUser.UserId = entityOgrenci.Id;
            //sinifUser.SinifId = entity.Id;
            await _sinifUserRepo.InsertAsync(sinifUser);
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
                entity.OnaylayanKullanici = entityKullanici;
                await Repository.UpdateAsync(entity);
            }
            else
            {
                throw new UserFriendlyException("Tekrar giriş yapınız!");
            }
        }

        //public async Task OgrenciEkleMulti(List<Guid> list,Guid guidSinif)
        //{
        //    var entity = await Repository.FindAsync(guidSinif);
        //    if(entity == null)
        //    {
        //        throw new UserFriendlyException("Sınıf bulunamadı");
        //    }
        //    List<SinifUser> sinifUserList = new List<SinifUser>();
        //    foreach (var identityUser in list)
        //    {
        //        var user=await _kullaniciRepo.FindAsync(identityUser);
        //        if(user is not null)
        //        {
        //            entity.OgrenciList.Add(user);
        //            var sinifUser = new SinifUser();
        //            sinifUser.SinifId = entity.Id;
        //            sinifUser.UserId = identityUser;
        //            sinifUserList.Add(sinifUser);
        //        }
        //    }
        //    await _sinifUserRepo.InsertManyAsync(sinifUserList);
        //    await Repository.UpdateAsync(entity);
        //   // return ObjectMapper.Map<Sinif,SinifDto>(entity);
        //}

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


    }
}
