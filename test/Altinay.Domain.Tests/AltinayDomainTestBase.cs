using Volo.Abp.Modularity;

namespace Altinay;

/* Inherit from this class for your domain layer tests. */
public abstract class AltinayDomainTestBase<TStartupModule> : AltinayTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
