using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace SanalVaka.Data;

/* This is used if database provider does't define
 * ISanalVakaDbSchemaMigrator implementation.
 */
public class NullSanalVakaDbSchemaMigrator : ISanalVakaDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
