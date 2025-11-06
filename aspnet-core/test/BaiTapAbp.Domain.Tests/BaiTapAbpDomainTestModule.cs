using Volo.Abp.Modularity;

namespace BaiTapAbp;

[DependsOn(
    typeof(BaiTapAbpDomainModule),
    typeof(BaiTapAbpTestBaseModule)
)]
public class BaiTapAbpDomainTestModule : AbpModule
{

}
