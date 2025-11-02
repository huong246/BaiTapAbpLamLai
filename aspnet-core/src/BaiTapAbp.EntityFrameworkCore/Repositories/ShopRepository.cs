using System.Threading.Tasks;
using BaiTapAbp.Entities;
using BaiTapAbp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BaiTapAbp.Repositories;

public class ShopRepository(IDbContextProvider<BaiTapAbpDbContext> dbContextProvider)
    : BaseRepository<ShopEntity, int>(dbContextProvider), IShopRepository
{
    
    public async Task<bool> HaveProductInShop(int shopId)
    {
        var sql = @"SELECT EXISTS( 
                        SELECT 1 
                        FROM Product p
                        WHERE p.IsDeleted = 0 
                          AND p.ShopId = @ShopId
                    ) AS IsExists";
        var result = await QueryFirstOrDefaultAsync<bool?>(sql, new {ShopId = shopId});
        return result == true;
    }
}