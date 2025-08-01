using System.Threading.Tasks;

namespace Altinay.Data;

public interface IAltinayDbSchemaMigrator
{
    Task MigrateAsync();
}
