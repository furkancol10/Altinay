using Volo.Abp.Settings;

namespace Altinay.Settings;

public class AltinaySettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AltinaySettings.MySetting1));
    }
}
