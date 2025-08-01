using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Altinay.Data;

/* This is used if database provider does't define
 * IAltinayDbSchemaMigrator implementation.
 */
public class NullAltinayDbSchemaMigrator : IAltinayDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
