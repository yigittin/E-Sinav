using SanalVaka.Bolumler;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SanalVaka.DersDtos
{
    public interface IDersAppService:ICrudAppService<DersDto,
                Guid,
                PagedAndSortedResultRequestDto,
                CreateUpdateDersDto>
    {
    }
}
