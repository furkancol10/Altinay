using Volo.Abp.Modularity;

namespace Altinay;

[DependsOn(
    typeof(AltinayApplicationModule),
    typeof(AltinayDomainTestModule)
)]
public class AltinayApplicationTestModule : AbpModule
{

}
