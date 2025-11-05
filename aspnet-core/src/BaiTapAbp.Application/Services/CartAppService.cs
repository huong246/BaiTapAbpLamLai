using System.Threading.Tasks;
using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using BaiTapAbp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace BaiTapAbp.Services;

public class CartAppService(ICartRepository cartRepository)
    : CrudAppService<CartEntity, CartDto, int, CartPagedRequestDto, CreateUpdateCartDto>(cartRepository)
{
    [HttpPost]
    [ActionName("CreateOrUpdate")]
    [Authorize(RolePermissions.Carts.Create)]
    public override async Task<CartDto> CreateAsync(CreateUpdateCartDto input)
    {
        if (input.Quantity <= 0)
        {
            throw new UserFriendlyException("The quantity added to the cart must be greater than 0");
        }
        var cartItemExisting = await cartRepository.FindExistingCartItemAsync(input.CustomerId, input.ShopId, input.ProductId);
        if (cartItemExisting != null)
        {
            cartItemExisting.Quantity += input.Quantity;
            if (cartItemExisting.Quantity <= 0)
            {
                await cartRepository.DeleteAsync(cartItemExisting);
                return null;
            }
            await cartRepository.UpdateAsync(cartItemExisting);
            return await MapToGetOutputDtoAsync(cartItemExisting);
        }
        return await base.CreateAsync(input);
    }
//create co the thay the chuc nang cua update va delete luon
    [HttpPost]
    [ActionName("Update")]
    [Authorize(RolePermissions.Carts.Edit)]
    public override async Task<CartDto> UpdateAsync(int id, CreateUpdateCartDto input)
    {
        if (cartRepository.FindExistingCartItemAsync(input.CustomerId, input.ShopId, input.ProductId) == null)
        {
            throw new UserFriendlyException("The cart item does not exist");
        }
        return await base.UpdateAsync(id, input);
    }
    [HttpPost]
    [ActionName("Delete")]
    [Authorize(RolePermissions.Carts.Delete)]
    public override Task DeleteAsync(int id)
    {
        return base.DeleteAsync(id);
    }
}