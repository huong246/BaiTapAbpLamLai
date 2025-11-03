using System;
using System.Threading.Tasks;
using BaiTapAbp.Entities;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Repositories;

public interface IShopRepository : IRepository<ShopEntity, int>, IScopedDependency
{ 
    Task<bool> HaveProductInShop(int shopId);
    Task<bool> UserHasAlreadyAShop(Guid userId);

}