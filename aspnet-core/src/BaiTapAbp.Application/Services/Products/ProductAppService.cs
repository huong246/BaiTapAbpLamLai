using System.Linq;
using System.Threading.Tasks;
using BaiTapAbp.Authorization;
using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using BaiTapAbp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace BaiTapAbp.Services;
[Authorize(RolePermissions.Products.Default)]
public class ProductAppService(IProductRepository productRepository, ICategoryRepository categoryRepository, IShopRepository shopRepository)
    : CrudAppService<ProductEntity, ProductDto, int, ProductPagedRequestDto, CreateUpdateProductDto>(productRepository)
{
    protected override async Task<IQueryable<ProductEntity>> CreateFilteredQueryAsync(ProductPagedRequestDto input)
    {
        var query = await base.CreateFilteredQueryAsync(input);
        query = query.WhereIf(
            !string.IsNullOrWhiteSpace(input.Filter), 
            x => x.Name.Contains(input.Filter)
        );
        return query;
    }

    [HttpPost]
    [ActionName("GetPaged")]
    [AllowAnonymous]
    public override async Task<PagedResultDto<ProductDto>> GetListAsync(ProductPagedRequestDto input)
    {
        return await base.GetListAsync(input);  
    }
    //create product
    [HttpPost]
    [ActionName("Create")]
    [Authorize(RolePermissions.Products.Create)]
    public override async Task<ProductDto> CreateAsync(CreateUpdateProductDto input)
    {
        if (!await categoryRepository.CheckCategoryExistInDbAsync(input.CategoryId))
        {
            throw new UserFriendlyException("This category with categoryId not exist");
        }

        if (!CurrentUser.IsInRole(UserRole.Admin))
        {
            if (CurrentUser.IsInRole(UserRole.Seller))
            {
                var currentUserId = CurrentUser.GetId();
                var sellerShopId = await shopRepository.GetShopIdBySellerIdAsync(currentUserId);
                if (sellerShopId == null)
                {
                    throw new UserFriendlyException(
                        "You are a Seller but have not been assigned a shop. Please contact the admin.");
                }
                if (sellerShopId.Value != input.ShopId)
                {
                    throw new UserFriendlyException(
                        "As a Seller, you are only allowed to create products in your own shop.");
                }
            }
        }
        return await base.CreateAsync(input);
    }
    //update product
    [HttpPost]
    [ActionName("Update")]
    [Authorize(RolePermissions.Products.Edit)]
    public override async Task<ProductDto> UpdateAsync(int id, CreateUpdateProductDto input)
    {
        return await base.UpdateAsync(id, input);
    }
    
    //detele product
    [HttpPost]
    [ActionName("Delete")]
    [Authorize(RolePermissions.Products.Delete)]
    public override async Task DeleteAsync(int id)
    {
        await base.DeleteAsync(id);
    }

    /*
    protected override Task<ProductEntity> MapToEntityAsync(CreateUpdateProductDto createInput)
    {
        return base.MapToEntityAsync(createInput);
    }

    protected override Task MapToEntityAsync(CreateUpdateProductDto updateInput, ProductEntity entity)
    {
        return base.MapToEntityAsync(updateInput, entity);
    }
    */
}