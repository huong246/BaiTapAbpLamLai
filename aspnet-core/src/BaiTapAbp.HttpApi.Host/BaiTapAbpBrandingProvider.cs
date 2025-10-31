using Microsoft.Extensions.Localization;
using BaiTapAbp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace BaiTapAbp;

[Dependency(ReplaceServices = true)]
public class BaiTapAbpBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<BaiTapAbpResource> _localizer;

    public BaiTapAbpBrandingProvider(IStringLocalizer<BaiTapAbpResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
