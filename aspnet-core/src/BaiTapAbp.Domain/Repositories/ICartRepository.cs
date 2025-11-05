using System.Threading.Tasks;
using BaiTapAbp.Entities;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Repositories;

public interface ICartRepository : IRepository<CartEntity, int>,  IScopedDependency
{
    Task<CartEntity?> FindExistingCartItemAsync(int customerId, int shopId, int productId);
}