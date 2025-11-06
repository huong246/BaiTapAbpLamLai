using Volo.Abp.Modularity;

namespace BaiTapAbp;

public abstract class BaiTapAbpApplicationTestBase<TStartupModule> : BaiTapAbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
