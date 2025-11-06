using BaiTapAbp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace BaiTapAbp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class BaiTapAbpController : AbpControllerBase
{
    protected BaiTapAbpController()
    {
        LocalizationResource = typeof(BaiTapAbpResource);
    }
}
