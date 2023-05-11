using System;
using System.Collections.Generic;
using System.Text;
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
            
        }
    }
}
