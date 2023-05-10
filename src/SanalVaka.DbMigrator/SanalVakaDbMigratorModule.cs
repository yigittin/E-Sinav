using SanalVaka.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace SanalVaka.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(SanalVakaEntityFrameworkCoreModule),
    typeof(SanalVakaApplicationContractsModule)
    )]
public class SanalVakaDbMigratorModule : AbpModule
{

}
