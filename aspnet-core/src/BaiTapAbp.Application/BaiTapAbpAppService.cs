using System;
using System.Collections.Generic;
using System.Text;
using BaiTapAbp.Localization;
using Volo.Abp.Application.Services;

namespace BaiTapAbp;

/* Inherit your application services from this class.
 */
public abstract class BaiTapAbpAppService : ApplicationService
{
    protected BaiTapAbpAppService()
    {
        LocalizationResource = typeof(BaiTapAbpResource);
    }
}
