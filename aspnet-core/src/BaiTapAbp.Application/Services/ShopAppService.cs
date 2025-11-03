using System.Linq;
using System.Threading.Tasks;
using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using BaiTapAbp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Volo.Abp.Authorization;

namespace BaiTapAbp.Services;

[Authorize(RolePermissions.Shops.Default)]
public class ShopAppService(IShopRepository shopRepository) 
    : CrudAppService<ShopEntity, ShopDto, int, ShopPagedRequestDto, CreateUpdateShopDto>(shopRepository)
{
    protected override async Task<IQueryable<ShopEntity>> CreateFilteredQueryAsync(ShopPagedRequestDto input)
    {
        var query = await base.CreateFilteredQueryAsync(input);
        query.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x =>
            x.Name.Contains(input.Filter));
        return query;
    }

    [HttpPost]
    [ActionName("GetPaged")]
    [AllowAnonymous]
    public override async Task<PagedResultDto<ShopDto>> GetListAsync(ShopPagedRequestDto input)
    {
        return await base.GetListAsync(input);
    }

    [HttpPost]
    [ActionName("Create")]
    [Authorize(RolePermissions.Shops.Create)]
    public override async Task<ShopDto> CreateAsync(CreateUpdateShopDto input)
    {
        var userId = CurrentUser.Id; 
        if (!userId.HasValue)
        {
            throw new AbpAuthorizationException("Authentication required to perform this action.");
        }
        var actualUserId = userId.Value;
        if (await shopRepository.UserHasAlreadyAShop(actualUserId))
        {
            throw new UserFriendlyException("Shop already exists.");
        }
        var entity = await MapToEntityAsync(input);
        entity.SellerId = actualUserId;
        await Repository.InsertAsync(entity, autoSave: true);
        return await MapToGetOutputDtoAsync(entity);
    }

    [HttpPost]
    [ActionName("Update")]
    [Authorize(RolePermissions.Shops.Edit)]
    public override async Task<ShopDto> UpdateAsync(int id, CreateUpdateShopDto input)
    {
        return await base.UpdateAsync(id, input);
    }
    
    [HttpPost]
    [ActionName("Remove")]
    [Authorize(RolePermissions.Shops.Delete)]
    public override async Task DeleteAsync(int id)
    {
        if (await shopRepository.HaveProductInShop(id))
        {
            throw new UserFriendlyException("Shop have product so can't be deleted.");
        }
        await base.DeleteAsync(id);
    }
    

   
}