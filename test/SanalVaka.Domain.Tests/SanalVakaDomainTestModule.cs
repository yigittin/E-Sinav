using SanalVaka.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace SanalVaka;

[DependsOn(
    typeof(SanalVakaEntityFrameworkCoreTestModule)
    )]
public class SanalVakaDomainTestModule : AbpModule
{

}
