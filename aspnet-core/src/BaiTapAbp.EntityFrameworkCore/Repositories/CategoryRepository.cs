using System.Threading.Tasks;
using BaiTapAbp.Entities;
using BaiTapAbp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BaiTapAbp.Repositories;

public class CategoryRepository(IDbContextProvider<BaiTapAbpDbContext> dbContextProvider)
    : BaseRepository<CategoryEntity, int>(dbContextProvider), ICategoryRepository
{
    public Task<bool> HaveCategoryInDbAsync(string categoryName)
    {
        throw new System.NotImplementedException();
    }
}