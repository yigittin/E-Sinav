using SanalVaka.SinifDtos;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SanalVaka.SinavDtos
{
    public interface ISinavAppService:ICrudAppService<SinavCrudDto,
                                                        Guid,
                                                        PagedAndSortedResultRequestDto,
                                                        CreateUpdateSinavDto>
    {
    }
}
