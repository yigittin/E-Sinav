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

namespace SanalVaka.Dersler
{
    public class DersAppService:CrudAppService<Ders,DersDto, Guid, PagedAndSortedResultRequestDto,CreateUpdateDersDto>,IDersAppService
    {
        public DersAppService(IRepository<Ders, Guid> repository)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Dersler.Default;
            GetListPolicyName = SanalVakaPermissions.Dersler.Default;
            CreatePolicyName = SanalVakaPermissions.Dersler.Create;
            UpdatePolicyName = SanalVakaPermissions.Dersler.Edit;
            DeletePolicyName = SanalVakaPermissions.Dersler.Delete;
        }
    }
}
