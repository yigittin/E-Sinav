using System.Threading.Tasks;

namespace SanalVaka.Data;

public interface ISanalVakaDbSchemaMigrator
{
    Task MigrateAsync();
}
