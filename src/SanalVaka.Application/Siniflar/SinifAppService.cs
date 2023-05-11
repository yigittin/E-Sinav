using SanalVaka.DersDtos;
using SanalVaka.Dersler;
using SanalVaka.Permissions;
using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SanalVaka.Siniflar
{
    public class SinifAppService:CrudAppService<Sinif, SinifDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateSinifDto>,ISinifAppService
    {
        public SinifAppService(IRepository<Sinif, Guid> repository)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Siniflar.Default;
            GetListPolicyName = SanalVakaPermissions.Bolumler.Default;
            CreatePolicyName = SanalVakaPermissions.Bolumler.Create;
            UpdatePolicyName = SanalVakaPermissions.Bolumler.Edit;
            DeletePolicyName = SanalVakaPermissions.Bolumler.Delete;
        }
    }
}
