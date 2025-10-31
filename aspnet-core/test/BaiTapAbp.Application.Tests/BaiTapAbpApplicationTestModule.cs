using Volo.Abp.Modularity;

namespace BaiTapAbp;

[DependsOn(
    typeof(BaiTapAbpApplicationModule),
    typeof(BaiTapAbpDomainTestModule)
)]
public class BaiTapAbpApplicationTestModule : AbpModule
{

}
