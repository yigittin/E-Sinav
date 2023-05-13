using SanalVaka.Bolumler;
using SanalVaka.DersDtos;
using SanalVaka.Permissions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private readonly ICurrentUser _currentUser;
        public DersAppService(IRepository<Ders, Guid> repository, IRepository<IdentityUser, Guid> kullaniciRepo, ICurrentUser currentUser)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Dersler.Default;
            GetListPolicyName = SanalVakaPermissions.Dersler.Default;
            CreatePolicyName = SanalVakaPermissions.Dersler.Create;
            UpdatePolicyName = SanalVakaPermissions.Dersler.Edit;
            DeletePolicyName = SanalVakaPermissions.Dersler.Delete;
            _kullaniciRepo = kullaniciRepo;
            _currentUser = currentUser;

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
    }

}

