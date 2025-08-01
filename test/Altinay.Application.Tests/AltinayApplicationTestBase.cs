using Volo.Abp.Modularity;

namespace Altinay;

public abstract class AltinayApplicationTestBase<TStartupModule> : AltinayTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
