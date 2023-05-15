using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SanalVaka.OgrenciDtos
{
    public interface IOgrenciAppService : ICrudAppService<OgrenciDto,
                int,
                PagedAndSortedResultRequestDto,
                CreateUpdateOgrenciDto>
    {
    }

}
