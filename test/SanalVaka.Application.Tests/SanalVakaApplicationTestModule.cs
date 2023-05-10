using Volo.Abp.Modularity;

namespace SanalVaka;

[DependsOn(
    typeof(SanalVakaApplicationModule),
    typeof(SanalVakaDomainTestModule)
    )]
public class SanalVakaApplicationTestModule : AbpModule
{

}
