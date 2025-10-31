using Volo.Abp.Modularity;

namespace BaiTapAbp;

/* Inherit from this class for your domain layer tests. */
public abstract class BaiTapAbpDomainTestBase<TStartupModule> : BaiTapAbpTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
