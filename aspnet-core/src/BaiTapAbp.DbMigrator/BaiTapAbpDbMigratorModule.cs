using BaiTapAbp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace BaiTapAbp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(BaiTapAbpEntityFrameworkCoreModule),
    typeof(BaiTapAbpApplicationContractsModule)
    )]
public class BaiTapAbpDbMigratorModule : AbpModule
{
}
