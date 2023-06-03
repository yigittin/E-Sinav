using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace SanalVaka.SinavDtos
{
    public interface ISoruAppService : ICrudAppService<SoruCrudDto,
                                                        Guid,
                                                        PagedAndSortedResultRequestDto,
                                                        CreateUpdateSoruDto>
    {
    }
}
