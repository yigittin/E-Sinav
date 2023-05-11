using SanalVaka.DersDtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SanalVaka.SinifDtos
{
    public interface ISinifAppService: ICrudAppService<SinifDto,
                Guid,
                PagedAndSortedResultRequestDto,
                CreateUpdateSinifDto>
    {
    }
}
