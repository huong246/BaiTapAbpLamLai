using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace BaiTapAbp.Data;

/* This is used if database provider does't define
 * IBaiTapAbpDbSchemaMigrator implementation.
 */
public class NullBaiTapAbpDbSchemaMigrator : IBaiTapAbpDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
