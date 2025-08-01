using Altinay.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Altinay.Blazor;

public abstract class AltinayComponentBase : AbpComponentBase
{
    protected AltinayComponentBase()
    {
        LocalizationResource = typeof(AltinayResource);
    }
}
