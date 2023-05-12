using SanalVaka.Dersler;
using SanalVaka.Permissions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace SanalVaka.Bolumler
{
    public class BolumAppService:CrudAppService<Bolum,BolumDto,Guid,PagedAndSortedResultRequestDto,CreateUpdateBolumDto>,IBolumAppService
    {
        public BolumAppService(IRepository<Bolum, Guid> repository)
        : base(repository)
        {
            GetPolicyName = SanalVakaPermissions.Bolumler.Default;
            GetListPolicyName = SanalVakaPermissions.Bolumler.Default;
            CreatePolicyName = SanalVakaPermissions.Bolumler.Create;
            UpdatePolicyName = SanalVakaPermissions.Bolumler.Edit;
            DeletePolicyName = SanalVakaPermissions.Bolumler.Delete;
        }
    }
}
