using System.Threading.Tasks;
using BaiTapAbp.Entities;
using BaiTapAbp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace BaiTapAbp.Repositories;

public class CartRepository(IDbContextProvider<BaiTapAbpDbContext> dbContextProvider)
    : BaseRepository<CartEntity, int>(dbContextProvider), ICartRepository
{
    public async Task<CartEntity?> FindExistingCartItemAsync(int customerId, int shopId, int productId)
    {
        var sql = @"
            SELECT * FROM [Cart] 
            WHERE CustomerId = @CustomerId 
              AND ShopId = @ShopId 
              AND ProductId = @ProductId 
              AND IsDeleted = 0 
            LIMIT 1;
        ";
        var result = await QueryFirstOrDefaultAsync<CartEntity?>(sql, new
        {
            CustomerId = customerId,
            ShopId = shopId,
            ProductId = productId
        });
        return result;
    }
}