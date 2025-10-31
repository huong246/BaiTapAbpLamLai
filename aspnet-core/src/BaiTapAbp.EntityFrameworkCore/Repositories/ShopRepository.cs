using System.Threading.Tasks;
using BaiTapAbp.Entities;
using BaiTapAbp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BaiTapAbp.Repositories;

public class ShopRepository(IDbContextProvider<BaiTapAbpDbContext> dbContextProvider)
    : BaseRepository<ShopEntity, int>(dbContextProvider), IShopRepository
{
    public async Task<bool> HasProductsAsync(int shopId)
    {
        var sql = @"
            SELECT EXISTS(
                SELECT 1 
                FROM Product 
                WHERE ShopId = @ShopId AND IsDeleted = 0
            ) AS IsExists;
        ";
        var result = await QueryFirstOrDefaultAsync<bool?>(sql, new { ShopId = shopId });
        return result == true;
    }

    public async Task<ShopEntity?> FindBySellerAsync(int sellerId)
    { 
        var sql = @"
            SELECT * FROM Shop 
            WHERE SellerId = @CreatorId AND IsDeleted = 0 
            LIMIT 1;
        ";
        return await QueryFirstOrDefaultAsync<ShopEntity>(sql, new { SellerId = sellerId });
    }
}