using System;
using System.Collections.Generic;
using System.Text;
using Altinay.Localization;
using Volo.Abp.Application.Services;

namespace Altinay;

/* Inherit your application services from this class.
 */
public abstract class AltinayAppService : ApplicationService
{
    protected AltinayAppService()
    {
        LocalizationResource = typeof(AltinayResource);
    }
}
