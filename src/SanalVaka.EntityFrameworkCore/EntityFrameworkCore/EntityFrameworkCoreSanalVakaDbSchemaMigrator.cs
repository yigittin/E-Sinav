using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SanalVaka.Data;
using Volo.Abp.DependencyInjection;

namespace SanalVaka.EntityFrameworkCore;

public class EntityFrameworkCoreSanalVakaDbSchemaMigrator
    : ISanalVakaDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreSanalVakaDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the SanalVakaDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<SanalVakaDbContext>()
            .Database
            .MigrateAsync();
    }
}
