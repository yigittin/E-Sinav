using System;
using System.Collections.Generic;
using System.Text;
using SanalVaka.Localization;
using Volo.Abp.Application.Services;

namespace SanalVaka;

/* Inherit your application services from this class.
 */
public abstract class SanalVakaAppService : ApplicationService
{
    protected SanalVakaAppService()
    {
        LocalizationResource = typeof(SanalVakaResource);
    }
}
