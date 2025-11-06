using System.Linq;
using System.Threading.Tasks;
using BaiTapAbp.Dtos;
using BaiTapAbp.Entities;
using BaiTapAbp.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace BaiTapAbp.Services;
public class CategoryAppService(ICategoryRepository categoryRepository)
    : CrudAppService<CategoryEntity, CategoryDto, int, CategoryPagedRequestDto, CreateUpdateCategoryDto>(
        categoryRepository)
{
    protected override async Task<IQueryable<CategoryEntity>> CreateFilteredQueryAsync(CategoryPagedRequestDto input)
    {
        var query = await base.CreateFilteredQueryAsync(input);
        query = query.WhereIf(!string.IsNullOrWhiteSpace(input.Filter), x => x.Name.Contains(input.Filter));
        return query;
    }

    [HttpPost]
    [ActionName("GetPaged")]
    [AllowAnonymous]
    public override async Task<PagedResultDto<CategoryDto>> GetListAsync(CategoryPagedRequestDto input)
    {
        return await base.GetListAsync(input);
    }

   
    [HttpPost]
    [ActionName("Create")]
    [Authorize(RolePermissions.Categories.Create)]
    public override async Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
    {
        if (await categoryRepository.HaveCategoryInDbAsync(input.Name))
        {
            throw new UserFriendlyException($"This category with categoryName existed");
        }
        return await base.CreateAsync(input);
    }

    [HttpPost]
    [ActionName("Update")]
    [Authorize(RolePermissions.Categories.Edit)]
    public override async Task<CategoryDto> UpdateAsync(int id, CreateUpdateCategoryDto input)
    {
        if (await categoryRepository.HaveCategoryInDbAsync(input.Name))
        {
            throw new UserFriendlyException($"This category with categoryName existed");
        }

        return await base.UpdateAsync(id, input);
    }

    [HttpPost]
    [ActionName("Remove")]
    [Authorize(RolePermissions.Categories.Delete)]
    public override async Task DeleteAsync(int id)
    {
        if (await categoryRepository.HaveProductInCategoryAsync(id))
        {
            throw new UserFriendlyException("Cannot delete. This Category is assigned to with products");
        }
        await base.DeleteAsync(id);    
    }
}