using SanalVaka.Dersler;
using SanalVaka.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;
using Volo.Abp.Users;

namespace SanalVaka.Bolumler
{
    public class BolumAppService : CrudAppService<Bolum, BolumDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateBolumDto>, IBolumAppService
    {
        private readonly IRepository<IdentityUser, Guid> _kullaniciRepo;
        public BolumAppService(IRepository<Bolum, Guid> repository, IRepository<IdentityUser, Guid> kullaniciRepo)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Bolumler.Default;
            GetListPolicyName = SanalVakaPermissions.Bolumler.Default;
            CreatePolicyName = SanalVakaPermissions.Bolumler.Create;
            UpdatePolicyName = SanalVakaPermissions.Bolumler.Edit;
            DeletePolicyName = SanalVakaPermissions.Bolumler.Delete;
            _kullaniciRepo = kullaniciRepo;
        }

        public async Task<List<BolumInfoDto>> GetBolumlerInfo()
        {
            var entity = await Repository.GetListAsync();
            var res = ObjectMapper.Map<List<Bolum>, List<BolumInfoDto>>(entity);
            foreach (var item in res)
            {
                var creator = await _kullaniciRepo.FindAsync(item.CreatorId);
                item.CreatorUserName = creator.UserName;
            }
            return res;
        }
    }
}
