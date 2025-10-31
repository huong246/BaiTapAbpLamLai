using System.Collections.Generic;
using System.Threading.Tasks;
using BaiTapAbp.Entities;
using BaiTapAbp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BaiTapAbp.Repositories;

public class ProductRepository(IDbContextProvider<BaiTapAbpDbContext> dbContextProvider)
    : BaseRepository<ProductEntity, int>(dbContextProvider), IProductRepository
{
    public async Task<IEnumerable<ProductEntity>> GetListByShopIdAsync(int shopId)
    {
        var sql = @"
            SELECT * FROM Product 
            WHERE ShopId = @ShopId AND IsDeleted = 0;
        ";
        return await QueryListAsync<ProductEntity>(sql, new { ShopId = shopId });
    }
}