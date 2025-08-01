using Volo.Abp.Modularity;

namespace Altinay;

[DependsOn(
    typeof(AltinayDomainModule),
    typeof(AltinayTestBaseModule)
)]
public class AltinayDomainTestModule : AbpModule
{

}
