using SanalVaka.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace SanalVaka.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class SanalVakaController : AbpControllerBase
{
    protected SanalVakaController()
    {
        LocalizationResource = typeof(SanalVakaResource);
    }
}
