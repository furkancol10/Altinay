using Altinay.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Altinay.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AltinayEntityFrameworkCoreModule),
    typeof(AltinayApplicationContractsModule)
    )]
public class AltinayDbMigratorModule : AbpModule
{
}
