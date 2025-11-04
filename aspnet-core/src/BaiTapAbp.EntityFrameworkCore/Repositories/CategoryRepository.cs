using System.Threading.Tasks;
using BaiTapAbp.Entities;
using BaiTapAbp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BaiTapAbp.Repositories;

public class CategoryRepository(IDbContextProvider<BaiTapAbpDbContext> dbContextProvider)
    : BaseRepository<CategoryEntity, int>(dbContextProvider), ICategoryRepository
{
    public async Task<bool> HaveCategoryInDbAsync(string categoryName)
    {
        var sql = @"
    SELECT EXISTS(
        SELECT 1 
        FROM `Category` c
        WHERE c.IsDeleted = 0 
          AND c.Name = @CategoryName
    ) AS IsExist;
    ";
        var result = await QueryFirstOrDefaultAsync<bool?>(sql, new { CategoryName = categoryName });
        return result == true;
    }
}