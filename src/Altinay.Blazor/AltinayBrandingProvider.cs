using Microsoft.Extensions.Localization;
using Altinay.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Altinay.Blazor;

[Dependency(ReplaceServices = true)]
public class AltinayBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<AltinayResource> _localizer;

    public AltinayBrandingProvider(IStringLocalizer<AltinayResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
