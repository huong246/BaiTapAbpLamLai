using Volo.Abp.Settings;

namespace BaiTapAbp.Settings;

public class BaiTapAbpSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(BaiTapAbpSettings.MySetting1));
    }
}
