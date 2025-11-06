using System.Threading.Tasks;

namespace BaiTapAbp.Data;

public interface IBaiTapAbpDbSchemaMigrator
{
    Task MigrateAsync();
}
