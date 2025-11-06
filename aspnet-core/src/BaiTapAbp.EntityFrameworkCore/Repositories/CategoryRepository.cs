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

    public async Task<bool> HaveProductInCategoryAsync(int categoryId)
    {
        var sql = @"
        SELECT EXISTS(
            SELECT 1 
            FROM Product p
            JOIN Category c ON p.CategoryId = c.Id
            WHERE p.IsDeleted = 0 AND c.IsDeleted = 0 AND c.Id = @CategoryId
        ) AS IsExist";

        var result = await QueryFirstOrDefaultAsync<bool>(sql, new { CategoryId = categoryId });
        return result;
    }

    public async Task<bool> CheckCategoryExistInDbAsync(int categoryId)
    {
        var sql = @"SELECT EXISTS(
            SELECT 1 
            FROM Category 
            WHERE Id = @Id AND IsDeleted = 0
        )";
        var result = await QueryFirstOrDefaultAsync<bool>(
            sql, 
            new { Id = categoryId });
        return result;
    }
}