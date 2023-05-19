using SanalVaka.Dersler;
using SanalVaka.Permissions;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Collections.Generic;
using System.Net;
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

        public async Task<List<BolumInfoDto>> GetBolumlerInfo(int skipCount,
        int maxResultCount,
        string sorting,
        string filter = null)
        {
            var queryable = await Repository.GetQueryableAsync();
                            
                var entity= queryable.WhereIf(
                !filter.IsNullOrWhiteSpace(),
                Bolum => Bolum.BolumAdi.Contains(filter)
                ).OrderBy(sorting)
                .Skip(skipCount)
                .Take(maxResultCount);


            var infoList =new List<BolumInfoDto>();
            foreach (var item in entity)
            {
                var res = new BolumInfoDto()
                {
                    CreatorId=item.CreatorId,
                    BolumAdi=item.BolumAdi,
                    BolumOnayciAdi=item.BolumOnayciAdi,
                    Id=item.Id,
                };
                if(res.CreatorId is not null)
                {
                    var creator = await _kullaniciRepo.FindAsync((Guid)res.CreatorId);
                    res.CreatorUserName = creator.UserName;

                }
                infoList.Add(res);
            }
            return infoList;
        }
        public async Task<BolumInfoDto> NewBolum(CreateUpdateBolumDto input)
        {
            var bolum = new Bolum();
            bolum.BolumAdi = input.BolumAdi;
            bolum.IsOnaylandi = input.IsOnaylandi;
            await Repository.InsertAsync(bolum);
            var res = new BolumInfoDto()
            {
                BolumAdi = bolum.BolumAdi,
            };
            return res;
        }
        public async Task<PagedResultDto<BolumInfoDto>> GetPagedBolumler(PagedAndSortedResultRequestDto input, string filter = null)
        {
            var queryable = await Repository.GetQueryableAsync();
            var entity = queryable.WhereIf
            (
                !filter.IsNullOrWhiteSpace(),
                Bolum => Bolum.BolumAdi.Contains(filter)
            )
            //.OrderByIf(!input.Sorting.IsNullOrWhiteSpace(), input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);
            var infoList = new List<BolumInfoDto>();

            foreach (var item in entity)
            {
                var res = new BolumInfoDto()
                {
                    CreatorId = item.CreatorId,
                    BolumAdi = item.BolumAdi,
                    BolumOnayciAdi = item.BolumOnayciAdi,
                    Id = item.Id,
                };
                if (res.CreatorId is not null)
                {
                    var creator = await _kullaniciRepo.FindAsync((Guid)res.CreatorId);
                    res.CreatorUserName = creator.UserName;

                }
                infoList.Add(res);
            }
            return new PagedResultDto<BolumInfoDto>(
                infoList.Count,
                infoList
            );
        }
    }
}
