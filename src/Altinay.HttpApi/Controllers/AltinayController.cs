using Altinay.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Altinay.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AltinayController : AbpControllerBase
{
    protected AltinayController()
    {
        LocalizationResource = typeof(AltinayResource);
    }
}
